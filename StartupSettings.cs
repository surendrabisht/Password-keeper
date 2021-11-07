using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper
{
    public class StartupSettings
    {
        internal const String CredentialsFileConfig = "CredentialsFile";
        internal const string EncryptionKeyConfig = "EncryptionKey";
        internal const string AuthenticateKeyConfig = "AuthenticationKey";
        internal const string ConfigEncryptionAlgoKey = "config_encryption_algo";
        internal const string CredentialsEncryptionAlgoKey = "credentials_encryption_algo";
        internal static string AppSettings = "appSettings";
        public static String DefaultEncryptionKey = "default";
        public static String CredentialFileName => ConfigurationManager.AppSettings[StartupSettings.CredentialsFileConfig];
        public static ICryptoAlgorithm ConfigEncryptionAlgo = null;
        public static ICryptoAlgorithm CredentialsEncryptionAlgo = null;
        CredentialsEncryption encryptionObj = null;
        static StartupSettings _instance = null;
        public String encryptionKey = null;
        public String authenticateKey = null;


        private ICryptoAlgorithm getAlgoInstance(String algo_name)
        {
            if (algo_name == "AES128")
                return new AES128Algorithm();
            else if (algo_name == "AES256")
                return new AES256Algorithm();
            else
                return new AES256Algorithm();

        }


        private StartupSettings()
        {
            StartupSettings.ConfigEncryptionAlgo = getAlgoInstance(ConfigurationManager.AppSettings[StartupSettings.ConfigEncryptionAlgoKey]);
            StartupSettings.CredentialsEncryptionAlgo = getAlgoInstance(ConfigurationManager.AppSettings[StartupSettings.CredentialsEncryptionAlgoKey]);
            this.encryptionObj = new CredentialsEncryption(StartupSettings.ConfigEncryptionAlgo);
            this.encryptionKey = encryptionObj.DecryptKey(ConfigurationManager.AppSettings[StartupSettings.EncryptionKeyConfig]);
            this.authenticateKey = encryptionObj.DecryptKey(ConfigurationManager.AppSettings[StartupSettings.AuthenticateKeyConfig]);
        }


        public static StartupSettings GetInstance()
        {
            if (_instance == null)
                _instance = new StartupSettings();
            return _instance;
        }


        internal void SaveKeys(String encryptionKey, String authenticationKey)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[StartupSettings.EncryptionKeyConfig].Value = encryptionObj.EncryptKey(encryptionKey);
            config.AppSettings.Settings[StartupSettings.AuthenticateKeyConfig].Value = encryptionObj.EncryptKey(authenticationKey);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(StartupSettings.AppSettings);
        }


    }
}
