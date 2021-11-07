using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper
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
            return algo.Encrypt(plaintext, StartupSettings.GetInstance().encryptionKey);
        }

        public string Decrypt(string encryptedstring)
        {
            return algo.Decrypt(encryptedstring, StartupSettings.GetInstance().encryptionKey);
        }


        public string EncryptKey(string key)
        {
            return algo.Encrypt(key, StartupSettings.DefaultEncryptionKey);
        }

        public string DecryptKey(string key)
        {
            return algo.Decrypt(key, StartupSettings.DefaultEncryptionKey);
        }
    }
}
