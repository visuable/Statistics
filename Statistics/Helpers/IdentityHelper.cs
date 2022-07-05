using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Statistics.Utils;

namespace Statistics.Helpers;

public static class IdentityHelper
{
    public static IdentityModel GenerateNew()
    {
        var password = FillByRandomLetters(Constants.Identity.PasswordLength);
        var login = FillByRandomLetters(Constants.Identity.LoginLength);

        var passwordSalt = GetSaltBytes(Constants.Identity.SaltLength);
        var passwordHash = GetPasswordHashBytes(password, passwordSalt, Constants.Identity.PasswordLength);

        return new IdentityModel
        {
            Login = login,
            PasswordHash = Convert.ToBase64String(passwordHash),
            PasswordSalt = Convert.ToBase64String(passwordSalt)
        };
    }

    private static string FillByRandomLetters(int length)
    {
        var random = new Random();
        var builder = new StringBuilder(length);

        for (var index = 0; index < length; index++)
        {
            var nextLetterIndex = random.Next(0, Constants.Alphabet.Length);
            builder.Append(Constants.Alphabet[nextLetterIndex]);
        }

        return builder.ToString();
    }

    private static byte[] GetSaltBytes(int length)
    {
        var salt = new byte[length];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }

    private static byte[] GetPasswordHashBytes(string password, byte[] salt, int length)
    {
        return KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 20, length);
    }

    public class IdentityModel
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}