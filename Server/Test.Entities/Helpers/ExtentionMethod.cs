using System;
using System.Security.Cryptography;
using System.Text;

namespace Test.Entities.Helpers
{
    public static class ExtentionMethod
    {
        public static string Hash(this string password)
        {  
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password)); 
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}