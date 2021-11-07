using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper
{

    internal static class StringDecryptEncryptExtensions
    {

        public static string Encrypt(this string plaintext)
        {
            return StringCipher.Encrypt(plaintext, Constants.EncryptionKey);
        }

        public static string Decrypt(this string encryptedstring)
        {
            return StringCipher.Decrypt(encryptedstring, Constants.EncryptionKey);
        }

        public static string EncryptKey(this string key)
        {
            return StringCipher.Encrypt(key,Constants.DefaultEncryptionKey);
        }

        public static string DecryptKey(this string key)
        {
            return StringCipher.Decrypt(key,Constants.DefaultEncryptionKey);
        }
    }
}
