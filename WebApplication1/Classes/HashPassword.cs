using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Classes
{
    public static class HashPassword
    {
        private const int SaltSize = 16;

        public static string CreateHash(string password)
        {
            using var rng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var sha256 = SHA256.Create();
            var passwordWithSaltBytes = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));
            var hash = sha256.ComputeHash(passwordWithSaltBytes);

            var hashWithSalt = new byte[SaltSize + hash.Length];
            Array.Copy(salt, 0, hashWithSalt, 0, SaltSize);
            Array.Copy(hash, 0, hashWithSalt, SaltSize, hash.Length);

            return Convert.ToBase64String(hashWithSalt);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                var hashWithSalt = Convert.FromBase64String(hashedPassword);
                var salt = new byte[SaltSize];
                Array.Copy(hashWithSalt, 0, salt, 0, SaltSize);

                using var sha256 = SHA256.Create();
                var passwordWithSaltBytes = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));
                var hash = sha256.ComputeHash(passwordWithSaltBytes);

                var hashWithoutSalt = new byte[hash.Length];
                Array.Copy(hashWithSalt, SaltSize, hashWithoutSalt, 0, hash.Length);

                return hashWithoutSalt.SequenceEqual(hash);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
