using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper
{
    public static class Constants
    {
        internal const String CredentialsFileConfig = "CredentialsFile";
        internal const string EncryptionKeyConfig= "EncryptionKey";
        internal const string AuthenticateKeyConfig = "AuthenticationKey";
        internal static string AppSettings= "appSettings";


        public static String DefaultEncryptionKey = "default";
        public static String CredentialFileName => ConfigurationManager.AppSettings[Constants.CredentialsFileConfig];
        public static String EncryptionKey => ConfigurationManager.AppSettings[Constants.EncryptionKeyConfig].DecryptKey();
        public static String AuthenticateKey => ConfigurationManager.AppSettings[Constants.AuthenticateKeyConfig].DecryptKey();


        internal static void SaveKeys(String encryptionKey, String authenticationKey)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[Constants.EncryptionKeyConfig].Value = encryptionKey.EncryptKey();
            config.AppSettings.Settings[Constants.AuthenticateKeyConfig].Value = authenticationKey.EncryptKey();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(Constants.AppSettings);
        }


    }
}
