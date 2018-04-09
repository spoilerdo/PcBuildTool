using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace KillerApp.Domain
{
    public class Hasher
    {
        public static string Create(string value)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                value,
                Encoding.UTF8.GetBytes(Key()),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        private static string Key()
        {
            return "6vxiw0a0Aah1KTNk4MEQjw==";
        }
    }
}