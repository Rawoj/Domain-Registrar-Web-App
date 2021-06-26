using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Utils
{
    public class PasswordUtils
    {
        private const int interationCount = 50000;
        // https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
        private static byte[] CreateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return salt;
        }
        private static byte[] CreateRfc2898DeriveBytes(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, interationCount);
            return pbkdf2.GetBytes(20);
        }

        private static byte[] CombineSaltAndPassword(byte[] salt, byte[] hash)
        {
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return hashBytes;
        }

        private static string BytesToString(byte[] saltAndHash)
        {
            return Convert.ToBase64String(saltAndHash);
        }

        /// <summary>
        /// Generates password salt and hash from given password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Password Salt+Hash as one string</returns>
        public static string GeneratePasswordHash(string password)
        {
            var salt = CreateSalt();
            var rfc = CreateRfc2898DeriveBytes(password, salt);
            var snp = CombineSaltAndPassword(salt, rfc);
            return BytesToString(snp);
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">Plain password.</param>
        /// <param name="hash">Password hash</param>
        /// <returns>True if passwords match.
        /// False if they don't</returns>
        public static bool ComparePasswords(string password, string hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Compute the hash on the password the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, interationCount);
            byte[] passwordHash = pbkdf2.GetBytes(20);

            // Compare the results 
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != passwordHash[i])
                    return false;

            return true;
        }
    }
}
