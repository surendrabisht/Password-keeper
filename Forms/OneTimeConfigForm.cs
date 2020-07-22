using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordKeeper
{
    public partial class OneTimeConfigForm : Form
    {
        public string EncryptionKey { get; private set; }
        public string AuthenticationKey { get; private set; }

        public OneTimeConfigForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtEncryptionKey.Text))
            {
                MessageBox.Show("Encryption Key not entered.");
                return;
            }
            if (String.IsNullOrEmpty(txtAuthenticationKey.Text))
            {
                MessageBox.Show("Authentication Key not entered.");
                return;
            }
            EncryptionKey = txtEncryptionKey.Text;
            AuthenticationKey = txtAuthenticationKey.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
