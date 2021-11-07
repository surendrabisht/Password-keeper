using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordKeeper.BLL;

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
        public ICryptoAlgorithm ConfigEncryptionAlgo { get; private set; }
        public ICryptoAlgorithm CredentialsEncryptionAlgo { get; set; }
        CredentialsEncryption encryptionObj = null;
        static StartupSettings _instance = null;

        public String EncryptionKey { get { return encryptionObj.DecryptKey(ConfigurationManager.AppSettings[StartupSettings.EncryptionKeyConfig], DefaultEncryptionKey); ; } }
        public String AuthenticateKey { get { return encryptionObj.DecryptKey(ConfigurationManager.AppSettings[StartupSettings.AuthenticateKeyConfig], DefaultEncryptionKey); } }

        private StartupSettings()
        {
            ConfigEncryptionAlgo = getAlgoInstance(ConfigurationManager.AppSettings[StartupSettings.ConfigEncryptionAlgoKey]);
            CredentialsEncryptionAlgo = getAlgoInstance(ConfigurationManager.AppSettings[StartupSettings.CredentialsEncryptionAlgoKey]);
            this.encryptionObj = new CredentialsEncryption(ConfigEncryptionAlgo);
        }

        public static StartupSettings GetInstance()
        {
            if (_instance == null)
                _instance = new StartupSettings();
            return _instance;
        }

        private ICryptoAlgorithm getAlgoInstance(String algo_name)
        {
            if (algo_name == "AES128")
                return new AES128Algorithm();
            else if (algo_name == "AES256")
                return new AES256Algorithm();
            else
                return new AES256Algorithm();

        }

        internal void SaveKeys(String encryptionKey, String authenticationKey)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[StartupSettings.EncryptionKeyConfig].Value = encryptionObj.EncryptKey(encryptionKey, DefaultEncryptionKey);
            config.AppSettings.Settings[StartupSettings.AuthenticateKeyConfig].Value = encryptionObj.EncryptKey(authenticationKey, DefaultEncryptionKey);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(StartupSettings.AppSettings);
        }

    }
}
