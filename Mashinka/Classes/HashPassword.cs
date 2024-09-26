using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Classes
{
    public static class HashPassword
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static string CreateHash(string password)
        {
            using var rng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);

            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);
            var salt = new byte[SaltSize];
            var storedHash = new byte[HashSize];

            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);

            return hash.SequenceEqual(storedHash);
        }
    }
}
