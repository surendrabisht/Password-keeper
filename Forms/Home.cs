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
    public partial class Home : Form
    {
        BindingList<Credential> credentialsBindingList;
        public Home()
        {
            InitializeComponent();
        }

        BackgroundWorker refreshGridWorker = new BackgroundWorker();

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by Surendra. \n http://github.com/surendrabisht/");
        }

        private void useToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // Adding Entry for new website
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CredentialEditForm form3 = new CredentialEditForm(new Credential());
            if (form3.ShowDialog() == DialogResult.OK)
            {
                SaveWebsiteCredentials(form3.credential);
            }
        }

        // Deleting entry for websites
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var credential = GetSelectedCredential();
            if (credential != null)
            {
                FileHandling.DeleteOperation(credential);

                refreshDataInGrid();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            refreshGridWorker.DoWork += refreshGridWorkerDoWork;
            refreshGridWorker.RunWorkerCompleted += refreshGridWorkerRunWorkerCompleted;
            refreshDataInGrid();
        }

        private void refreshDataInGrid()
        {
            this.Cursor = Cursors.WaitCursor;
            this.dataGridView1.Cursor = Cursors.WaitCursor;
            refreshGridWorker.RunWorkerAsync();
        }
        private void refreshGridWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            credentialsBindingList = e.Result as BindingList<Credential>;
            dataGridView1.DataSource = credentialsBindingList;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            this.Cursor = Cursors.Arrow;
            this.dataGridView1.Cursor = Cursors.Arrow;
        }

        private void refreshGridWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BindingList<Credential> credentials = GetAllCredentials();
            e.Result = credentials;
        }


        /// <summary>
        /// Modify existing credentials
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var credential = GetSelectedCredential();
            if (credential != null)
            {
                CredentialEditForm form3 = new CredentialEditForm(credential);
                DialogResult dr = form3.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    SaveWebsiteCredentials(form3.credential);
                }
            }
        }

        public Credential GetSelectedCredential()
        {
            Credential credential = null;
            if (dataGridView1 != null && dataGridView1.SelectedRows.Count > 0)
            {
                var dataRow = dataGridView1.SelectedRows[0];

                credential = new Credential();
                credential.id = int.Parse(dataRow.Cells[0].Value.ToString());
                credential.Description = dataRow.Cells[1].Value.ToString();
                credential.UserName = dataRow.Cells[2].Value.ToString();
                credential.Pwd = dataRow.Cells[3].Value.ToString();
            }
            return credential;
        }


        private void SaveWebsiteCredentials(Credential credential)
        {
            if (credential.id > 0)
                FileHandling.ModifyOperation(credential,StartupSettings.CredentialsEncryptionAlgo);
            else
                FileHandling.InsertOperation(credential, StartupSettings.CredentialsEncryptionAlgo);

            refreshDataInGrid();
        }

        private BindingList<Credential> GetAllCredentials()
        {
            var passwords = FileHandling.ViewAll(StartupSettings.CredentialsEncryptionAlgo);
            var bindingList = new BindingList<Credential>(passwords);
            return bindingList;
        }

        // Copy user name to clipboard
        private void usernameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var credential = GetSelectedCredential();
            if (credential != null)
                FileHandling.ReadCredential(CopyValue.UserId, credential.id, StartupSettings.CredentialsEncryptionAlgo);
        }

        //copy password to clipboard
        private void passwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var credential = GetSelectedCredential();
            if (credential != null)
                FileHandling.ReadCredential(CopyValue.Password, credential.id, StartupSettings.CredentialsEncryptionAlgo);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshDataInGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void permissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new AuthenticateWindow().ShowDialog() == DialogResult.OK)
            {
                if (dataGridView1 != null)
                {
                    dataGridView1.Columns[2].Visible = true;
                    dataGridView1.Columns[3].Visible = true;
                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
           dataGridView1.DataSource= credentialsBindingList.Where(credential => credential.Description.ToLower().Contains(txtbxSearch.Text.ToLower())).ToList<Credential>();
        }


        private void to256BlockSize(object sender, EventArgs e)
        {
            foreach (Credential cr in credentialsBindingList)
            {
                FileHandling.ExportOperation(cr, new AES256Algorithm(), StartupSettings.CredentialFileName + "_to256");
            }
        }

        private void to128blockSize(object sender, EventArgs e)
        {
            foreach (Credential cr in credentialsBindingList)
            {
                FileHandling.ExportOperation(cr, new AES128Algorithm(), StartupSettings.CredentialFileName + "_to128");
            }
        }

        private void toPlainText(object sender, EventArgs e)
        {
            foreach (Credential cr in credentialsBindingList)
            {
                FileHandling.ExportOperation(cr, new NoCryptoAlgorithm(), StartupSettings.CredentialFileName + "_toPlainText");
            }
        }
    }
}