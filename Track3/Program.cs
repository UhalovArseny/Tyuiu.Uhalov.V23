using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Track3.Components;
using Track3.Components.Data;
using Track3.Components.Services.Implementations;
using VirtualMuseum.Data;
using VirtualMuseum.Services;
using System.IO;
using Microsoft.Data.Sqlite;


namespace Track3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Репозитории

            builder.Services.AddSingleton<IExhibitRepository, InMemoryExhibitRepository>();
            builder.Services.AddSingleton<ITourRepository, InMemoryTourRepository>();
            builder.Services.AddSingleton<IFeedbackRepository, InMemoryFeedbackRepository>();

            //  Сервисы (добавь вот это)
            builder.Services.AddSingleton<IExhibitService, ExhibitService>();
            builder.Services.AddScoped<ITourService, Track3.Components.Services.Implementations.TourServiceDb>();



            builder.Services.AddSingleton<IFeedbackService, FeedbackService>();
            builder.Services.AddSingleton<IVisitorService, VisitorService>();





            // SQLite БД

            // Auth
            builder.Services.AddScoped<AuthService>();
            // и если CurrentVisitorService работает с БД — тоже AddScoped

            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.Cookie.Name = "VM_AUTH";
                    o.LoginPath = "/login";
                    o.AccessDeniedPath = "/login";
                });

            builder.Services.AddAuthorization();
            // <-- добавь
            builder.Services.AddAuthorizationCore();   // <-- добавь (для Blazor компонентов)

            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();



            builder.Services.AddHttpClient("ServerAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7231/");
            });

            builder.Services.AddHttpContextAccessor();

            var dbPath = Path.Combine(builder.Environment.ContentRootPath, "museum.db");

            builder.Services.AddDbContext<AppDbContext>(o =>
                o.UseSqlite($"Data Source={dbPath}"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGet("/auth/debug/user-exists/{name}", async (AppDbContext db, string name) =>
            {
                name = (name ?? "").Trim();
                var exists = await db.Users.AnyAsync(u => u.UserName == name);
                return Results.Ok(new { name, exists });
            }).AllowAnonymous();

            app.MapPost("/auth/login", async (HttpContext http, AuthService auth) =>
            {
                var form = await http.Request.ReadFormAsync();
                var userName = form["userName"].ToString().Trim();
                var password = form["password"].ToString();

                var principal = await auth.LoginAsync(userName, password);
                if (principal == null)
                    return Results.BadRequest("bad");

                await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Results.Redirect("/user");
            }).AllowAnonymous().DisableAntiforgery();


            app.MapPost("/auth/register", async (HttpContext http, AuthService auth) =>
            {
                var form = await http.Request.ReadFormAsync();
                var userName = form["userName"].ToString().Trim();
                var password = form["password"].ToString();

                var (ok, error) = await auth.RegisterAsync(userName, password);
                if (!ok)
                    return Results.BadRequest(error ?? "exists");

                return Results.Ok();
            }).AllowAnonymous().DisableAntiforgery();




            app.MapPost("/auth/logout", async (HttpContext http) =>
            {
                await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Ok();
            });


            app.UseAntiforgery();



            

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AllowAnonymous(); // ← разрешает framework

            app.Run();
        }
    }
}
