using PasswordKeeper.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordKeeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!File.Exists(Constants.CredentialFileName))
                {
                    OneTimeConfigForm keyEnterForm = new OneTimeConfigForm();
                    if (keyEnterForm.ShowDialog() == DialogResult.OK)
                    {
                        Constants.SaveKeys(keyEnterForm.EncryptionKey, keyEnterForm.AuthenticationKey);
                        //Create File And close StreamWriter Object.
                        File.Create(Constants.CredentialFileName).Close();
                    }
                    else
                    {
                        return;
                    }
                }
                if (new AuthenticateWindow().ShowDialog() == DialogResult.OK)
                    Application.Run(new Home());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Occurred. For More information, refer below: \n"+ex.InnerException," Error! ",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
