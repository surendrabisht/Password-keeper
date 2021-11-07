using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.BLL
{

    public class CredentialsEncryption
    {
        readonly ICryptoAlgorithm algo = null;
        public CredentialsEncryption(ICryptoAlgorithm algo)
        {
            this.algo = algo;
        }

        public string Encrypt(string plaintext)
        {
            return algo.Encrypt(plaintext, Request.GetInstance().EncryptionKey);
        }

        public string Decrypt(string encryptedstring)
        {
            return algo.Decrypt(encryptedstring, Request.GetInstance().EncryptionKey);
        }


        public string EncryptKey(string key, string defaultEncryptionKey)
        {
            return algo.Encrypt(key, defaultEncryptionKey);
        }

        public string DecryptKey(string key,string defaultEncryptionKey)
        {
            return algo.Decrypt(key, defaultEncryptionKey);
        }
    }
}
