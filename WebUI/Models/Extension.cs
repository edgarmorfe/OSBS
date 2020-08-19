using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebUI.Models
{
    public static class Extension
    {
        public static string GetHashString(this string value)
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(value))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        public static byte[] GetHash(this string value)
        {
            var algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
        }
    }
}