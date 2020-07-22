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
    public partial class AuthenticateWindow : Form
    {
        public AuthenticateWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            if (!String.IsNullOrEmpty( textBox1.Text) && textBox1.Text.Equals(Constants.AuthenticateKey))
            this.DialogResult= DialogResult.OK;         
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender,null);
            }
        }

    }
}
