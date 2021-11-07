using PasswordKeeper.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordKeeper
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!File.Exists(StartupSettings.CredentialFileName))
                {
                    OneTimeConfigForm keyEnterForm = new OneTimeConfigForm();
                    if (keyEnterForm.ShowDialog() == DialogResult.OK)
                    {
                        StartupSettings.GetInstance().SaveKeys(keyEnterForm.EncryptionKey, keyEnterForm.AuthenticationKey);
                        //Create File And close StreamWriter Object.
                        File.Create(StartupSettings.CredentialFileName).Close();
                    }
                    else
                    {
                        return;
                    }
                }
                if (new AuthenticateWindow().ShowDialog() == DialogResult.OK)
                {
                    // creating request object which will be used in form control clicks to route request to bll logic.
                    new Request(StartupSettings.CredentialFileName, StartupSettings.GetInstance().CredentialsEncryptionAlgo, StartupSettings.GetInstance().EncryptionKey);
                    Application.Run(new Home());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred. For More information, refer below: \n" + ex.InnerException, " Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
        }


    }
}
