using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.BLL
{

    public class Request
    {
        static Request _instance = null;
        public static Request GetInstance()
        {
            if (_instance == null)
                throw new NullReferenceException();
            else
                return _instance;
        }

        public Request(string credentialFileName, ICryptoAlgorithm credentialsEncryptionAlgo, String encryptionKey)
        {
            this.CredentialFileName = credentialFileName;
            this.CredentialsEncryptionAlgo = credentialsEncryptionAlgo;
            this.EncryptionKey = encryptionKey;
            _instance = this;
        }

        public string CredentialFileName { get; }
        public ICryptoAlgorithm CredentialsEncryptionAlgo { get; }
        public string EncryptionKey { get; }

    }
}
