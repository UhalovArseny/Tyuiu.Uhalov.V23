using System.Security.Cryptography;
using System.Text;

namespace Track3.Components.Services.Implementations;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        // учебный вариант: SHA256
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}
