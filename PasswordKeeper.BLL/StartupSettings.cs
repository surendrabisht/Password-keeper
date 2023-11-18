using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordKeeper.BLL
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
    }
}
