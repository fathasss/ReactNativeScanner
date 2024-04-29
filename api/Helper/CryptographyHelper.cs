using System;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace api.Helper
{
    public class CryptographyHelper
    {
        protected static string HashValue = "Fatih.LogService.2024";

        public static string Encrypt(string value)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(value);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HashValue));
                using (TripleDESCryptoServiceProvider tripDes = new(){ Key = keys, Mode = CipherMode.ECB , Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data,0,data.Length);
                    return Convert.ToBase64String(results,0,results.Length);
                }
            }
        }

        public static string Decrypt(string value)
        {
            byte[] data = Convert.FromBase64String(value);

            using (MD5 md5 = MD5.Create())
            {
                byte[] keys = md5.ComputeHash(Encoding.UTF8.GetBytes(HashValue));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] decryptedData = transform.TransformFinalBlock(data, 0, data.Length);
                    return Encoding.UTF8.GetString(decryptedData);
                }
            }
        }


    }
}

