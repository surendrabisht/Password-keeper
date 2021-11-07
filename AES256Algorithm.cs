using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper
{

    /// <summary>
    /// This code has been taken from Cryptography algo.
    /// and Not written by me
    /// </summary>
    public class AES256Algorithm : ICryptoAlgorithm
    {
        private const int keySize = 256;

        private const int DerivationIterations = 1000;

        public string Encrypt(string plainText, string passPhrase)
        {
            byte[] array = Generate256BitsOfRandomEntropy();
            byte[] array2 = Generate256BitsOfRandomEntropy();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            string result;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, array, 1000))
            {
                byte[] bytes2 = rfc2898DeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.BlockSize = keySize;
                    rijndaelManaged.Mode = CipherMode.CBC;
                    rijndaelManaged.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateEncryptor(bytes2, array2))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(bytes, 0, bytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] arg_AF_0 = array.Concat(array2).ToArray<byte>().Concat(memoryStream.ToArray()).ToArray<byte>();
                                memoryStream.Close();
                                cryptoStream.Close();
                                result = Convert.ToBase64String(arg_AF_0);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public string Decrypt(string cipherText, string passPhrase)
        {
            byte[] array = Convert.FromBase64String(cipherText);
            byte[] salt = array.Take(32).ToArray<byte>();
            byte[] rgbIV = array.Skip(32).Take(32).ToArray<byte>();
            byte[] array2 = array.Skip(64).Take(array.Length - 64).ToArray<byte>();
            string @string;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passPhrase, salt, 1000))
            {
                byte[] bytes = rfc2898DeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.BlockSize = keySize;
                    rijndaelManaged.Mode = CipherMode.CBC;
                    rijndaelManaged.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform cryptoTransform = rijndaelManaged.CreateDecryptor(bytes, rgbIV))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(array2))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                            {
                                byte[] array3 = new byte[array2.Length];
                                int count = cryptoStream.Read(array3, 0, array3.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                @string = Encoding.UTF8.GetString(array3, 0, count);
                            }
                        }
                    }
                }
            }
            return @string;
        }

        private byte[] Generate256BitsOfRandomEntropy()
        {
            byte[] array = new byte[32];
            using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rNGCryptoServiceProvider.GetBytes(array);
            }
            return array;
        }

    }
}
