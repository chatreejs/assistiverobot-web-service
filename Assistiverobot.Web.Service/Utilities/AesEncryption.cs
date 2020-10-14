using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AssistiveRobot.Web.Service.Utilities
{
    public static class AesEncryption
    {
        private static readonly byte[] Salt =
        {
            0x67, 0x69, 0x73, 0x63, 0x6f, 0x6d, 0x70, 0x61, 0x6e, 0x79, 0x2d, 0x61, 0x74, 0x6c, 0x61, 0x73, 0x78, 0x2d,
            0x77, 0x65, 0x62, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65
        };
        private static readonly Dictionary<string, SymmetricAlgorithm> AlgorithmPasswords = new Dictionary<string, SymmetricAlgorithm>();

        public static string Encrypt(string plain, string password)
        {
            byte[] plainBytes = Encoding.Unicode.GetBytes(plain ?? "");
            SymmetricAlgorithm algorithm = InitializeAlgorithm(password);
            byte[] cipher = EncryptDecryptBinary(plainBytes, algorithm.CreateEncryptor());
            return Convert.ToBase64String(cipher);
        }

        public static string Decrypt(string cipher, string password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipher);
            SymmetricAlgorithm algorithm = InitializeAlgorithm(password);
            byte[] plain = EncryptDecryptBinary(cipherBytes, algorithm.CreateDecryptor());
            return Encoding.Unicode.GetString(plain);
        }

        private static byte[] EncryptDecryptBinary(byte[] text, ICryptoTransform transform)
        {
            using MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(text, 0, text.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        private static SymmetricAlgorithm InitializeAlgorithm(string password)
        {
            SymmetricAlgorithm algorithm;
            if (AlgorithmPasswords.ContainsKey(password))
            {
                algorithm = AlgorithmPasswords[password];
            }
            else
            {
                algorithm = CreateAlgorithm(password);
                AlgorithmPasswords.Add(password, algorithm);
            }
            return algorithm;
        }

        private static SymmetricAlgorithm CreateAlgorithm(string password)
        {
            SymmetricAlgorithm algorithm;
            algorithm = Aes.Create();
            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, Salt))
            {
                algorithm.Key = pdb.GetBytes(32);
                algorithm.IV = pdb.GetBytes(16);
            }
            return algorithm;
        }
    }
}