using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Track3.Components.Data;
using Track3.Components.Models;
using Track3.Components.Services.Implementations;
using VirtualMuseum.Models;

namespace VirtualMuseum.Services
{
    public class AuthService
    {
        private readonly AppDbContext _db;

        public AuthService(AppDbContext db)
        {
            _db = db;
        }

        // 🔹 Регистрация
        public async Task<(bool ok, string? error)> RegisterAsync(string userName, string password)
        {
            userName = (userName ?? "").Trim();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return (false, "Введите логин и пароль.");

            var exists = await _db.Users.AnyAsync(u => u.UserName == userName);
            if (exists)
                return (false, "Логин уже занят.");

            var user = new AppUser
            {
                UserName = userName,
                PasswordHash = PasswordHasher.Hash(password),
                CreatedAtUtc = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return (true, null);
        }

        // 🔹 Логин (ВАЖНО)
        public async Task<ClaimsPrincipal?> LoginAsync(string userName, string password)
        {
            userName = (userName ?? "").Trim();

            var user = await _db.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                return null;

            // ✅ проверка пароля — СРАВНЕНИЕ ХЭШЕЙ
            var inputHash = PasswordHasher.Hash(password);
            if (user.PasswordHash != inputHash)
                return null;

            user.LastLoginUtc = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new("uid", user.Id.ToString())
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return new ClaimsPrincipal(identity);
        }

        // 🔹 если где-то ещё используется
        public async Task<AppUser?> ValidateLoginAsync(string userName, string password)
        {
            var hash = PasswordHasher.Hash(password);

            return await _db.Users.SingleOrDefaultAsync(u =>
                u.UserName == userName && u.PasswordHash == hash
            );
        }
    }
}
