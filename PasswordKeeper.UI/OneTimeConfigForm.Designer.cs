namespace PasswordKeeper
{
    partial class OneTimeConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbKeyUsed = new System.Windows.Forms.Label();
            this.lblSafetyKey = new System.Windows.Forms.Label();
            this.txtEncryptionKey = new System.Windows.Forms.TextBox();
            this.txtAuthenticationKey = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblPageInfo.Font = new System.Drawing.Font("Lucida Sans", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPageInfo.Location = new System.Drawing.Point(203, 36);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(486, 32);
            this.lblPageInfo.TabIndex = 0;
            this.lblPageInfo.Text = "One-time Configuration Details:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Chartreuse;
            this.btnSave.Font = new System.Drawing.Font("Lucida Console", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.Location = new System.Drawing.Point(310, 439);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(183, 59);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbKeyUsed
            // 
            this.lbKeyUsed.AutoSize = true;
            this.lbKeyUsed.BackColor = System.Drawing.Color.Transparent;
            this.lbKeyUsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbKeyUsed.Location = new System.Drawing.Point(64, 175);
            this.lbKeyUsed.Name = "lbKeyUsed";
            this.lbKeyUsed.Size = new System.Drawing.Size(179, 29);
            this.lbKeyUsed.TabIndex = 2;
            this.lbKeyUsed.Text = "Encryption Key:";
            // 
            // lblSafetyKey
            // 
            this.lblSafetyKey.AutoSize = true;
            this.lblSafetyKey.BackColor = System.Drawing.Color.Transparent;
            this.lblSafetyKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSafetyKey.Location = new System.Drawing.Point(64, 279);
            this.lblSafetyKey.Name = "lblSafetyKey";
            this.lblSafetyKey.Size = new System.Drawing.Size(322, 29);
            this.lblSafetyKey.TabIndex = 3;
            this.lblSafetyKey.Text = "Safety Key for Authentication:";
            // 
            // txtEncryptionKey
            // 
            this.txtEncryptionKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEncryptionKey.Location = new System.Drawing.Point(488, 175);
            this.txtEncryptionKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtEncryptionKey.Name = "txtEncryptionKey";
            this.txtEncryptionKey.PasswordChar = '*';
            this.txtEncryptionKey.Size = new System.Drawing.Size(337, 35);
            this.txtEncryptionKey.TabIndex = 1;
            // 
            // txtAuthenticationKey
            // 
            this.txtAuthenticationKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAuthenticationKey.Location = new System.Drawing.Point(488, 275);
            this.txtAuthenticationKey.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAuthenticationKey.Name = "txtAuthenticationKey";
            this.txtAuthenticationKey.PasswordChar = '*';
            this.txtAuthenticationKey.Size = new System.Drawing.Size(337, 35);
            this.txtAuthenticationKey.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.Font = new System.Drawing.Font("Lucida Console", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(702, 439);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(148, 59);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OneTimeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::PasswordKeeper.Resource1.BackgroundLocker;
            this.ClientSize = new System.Drawing.Size(879, 541);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtAuthenticationKey);
            this.Controls.Add(this.txtEncryptionKey);
            this.Controls.Add(this.lblSafetyKey);
            this.Controls.Add(this.lbKeyUsed);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblPageInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OneTimeConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "First Time Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbKeyUsed;
        private System.Windows.Forms.Label lblSafetyKey;
        private System.Windows.Forms.TextBox txtEncryptionKey;
        private System.Windows.Forms.TextBox txtAuthenticationKey;
        private System.Windows.Forms.Button btnCancel;
    }
}