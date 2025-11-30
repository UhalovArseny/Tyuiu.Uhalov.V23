using Track3.Components;
using VirtualMuseum.Data;
using VirtualMuseum.Services;


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
            builder.Services.AddSingleton<ITourService, TourService>();
            builder.Services.AddSingleton<IFeedbackService, FeedbackService>();
            builder.Services.AddSingleton<IVisitorService, VisitorService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
