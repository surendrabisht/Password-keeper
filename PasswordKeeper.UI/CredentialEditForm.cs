using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PasswordKeeper.BLL;

namespace PasswordKeeper
{
    public partial class CredentialEditForm : Form
    {

        //public string SiteName { get; set; }
        //public string UserID { get; set; }
        //public string Password { get; set; }
        public Credential credential { get; }

        public CredentialEditForm(Credential credential)
        {
            this.credential = credential;
            InitializeComponent();
        }

        /// <summary>
        /// save function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            credential.Description = txtSite.Text;
            credential.UserName = txtUserId.Text;
            credential.Pwd = txtPwd.Text;

           this.DialogResult= DialogResult.OK;
        }

        /// <summary>
        /// form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form3_Load(object sender, EventArgs e)
        {
            txtSite.Text = credential.Description;
            txtUserId.Text = credential.UserName;
            txtPwd.Text = credential.Pwd;
        }

        /// <summary>
        /// Cancel function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
