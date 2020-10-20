using System;
using System.Security.Cryptography;
using System.Text;
using AssistiveRobot.Web.Service.Utilities;

namespace AssistiveRobot.Web.Service.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetSHA256Hash(this string inputString)
        {
            return SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(inputString));
        }

        public static string GetSHA256HashString(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return inputString;
            }

            return Convert.ToBase64String(GetSHA256Hash(inputString))
                .Split('=')[0]
                .Replace('+', '-')
                .Replace('/', '_');
        }

        public static string Decrypt(this string input)
        {
            try
            {
                string secretKey = Environment.GetEnvironmentVariable("ASPNETCORE_SECRETKEY") ?? "AtlasX";
                return AesEncryption.Decrypt(input, secretKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return input;
            }
        }

        public static string GetSHA256UTF8HashString(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return string.Empty;
            }

            using (SHA256Managed sha = new SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}