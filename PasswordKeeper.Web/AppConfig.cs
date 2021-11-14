using System;
using System.Collections.Generic;
using PasswordKeeper.BLL;


namespace PasswordKeeper.Web
{
    public class AppConfig
    {
        private static AppConfig _instance = null;
        private AppConfig()
        {
        }
        public static AppConfig GetInstance()
        {
            if (_instance == null)
                _instance = new AppConfig();
            return _instance;
        }

        public String CredentialsFile { get; set; }
        public String EncryptionKey { get; set; }
        public String AuthenticationKey { get; set; }
        public String config_encryption_algo { get; set; }
        public String credentials_encryption_algo { get; set; }
        private List<Credential> allCredentials =null;

        public List<Credential> GetAllCredentials()
        {
           if( allCredentials==null)
            {
                RefreshCredentials();
            }
            return allCredentials;
        }

        public  void RefreshCredentials()
        {
            var aES128Algorithm = new PasswordKeeper.BLL.AES128Algorithm();
            Request x = new Request(this.CredentialsFile, aES128Algorithm, this.EncryptionKey);
            allCredentials = FileHandling.ViewAll(aES128Algorithm);
        }
    }
}
