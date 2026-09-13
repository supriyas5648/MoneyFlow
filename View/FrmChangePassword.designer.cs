namespace MoneyFlow
{
      public partial class FrmChangePassword : System.Windows.Forms.Form
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.lblChangePasswordTitle = new System.Windows.Forms.Label();
            this.lblChangePasswordCurrentPassword = new System.Windows.Forms.Label();
            this.txtChangePasswordCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblChangePasswordCurrentPasswordError = new System.Windows.Forms.Label();
            this.lblChangePasswordNewPassword = new System.Windows.Forms.Label();
            this.txtChangePasswordNewPassword = new System.Windows.Forms.TextBox();
            this.lblChangePasswordNewPasswordError = new System.Windows.Forms.Label();
            this.lblChangePasswordConfirmPassword = new System.Windows.Forms.Label();
            this.txtChangePasswordConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblChangePasswordConfirmPasswordError = new System.Windows.Forms.Label();
            this.btnChangePasswordSubmit = new System.Windows.Forms.Button();
            this.btnChangePasswordCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblChangePasswordTitle
            // 
            this.lblChangePasswordTitle.AutoSize = true;
            this.lblChangePasswordTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChangePasswordTitle.Location = new System.Drawing.Point(183, 25);
            this.lblChangePasswordTitle.Name = "lblChangePasswordTitle";
            this.lblChangePasswordTitle.Size = new System.Drawing.Size(177, 28);
            this.lblChangePasswordTitle.TabIndex = 0;
            this.lblChangePasswordTitle.Text = "Change Password";
            // 
            // lblChangePasswordCurrentPassword
            // 
            this.lblChangePasswordCurrentPassword.AutoSize = true;
            this.lblChangePasswordCurrentPassword.Location = new System.Drawing.Point(45, 85);
            this.lblChangePasswordCurrentPassword.Name = "lblChangePasswordCurrentPassword";
            this.lblChangePasswordCurrentPassword.Size = new System.Drawing.Size(112, 16);
            this.lblChangePasswordCurrentPassword.TabIndex = 1;
            this.lblChangePasswordCurrentPassword.Text = "Current Password";
            // 
            // txtChangePasswordCurrentPassword
            // 
            this.txtChangePasswordCurrentPassword.Location = new System.Drawing.Point(215, 82);
            this.txtChangePasswordCurrentPassword.Name = "txtChangePasswordCurrentPassword";
            this.txtChangePasswordCurrentPassword.Size = new System.Drawing.Size(260, 22);
            this.txtChangePasswordCurrentPassword.TabIndex = 2;
            this.txtChangePasswordCurrentPassword.UseSystemPasswordChar = true;
            this.txtChangePasswordCurrentPassword.TextChanged += new System.EventHandler(this.txtChangePasswordCurrentPassword_TextChanged);
            // 
            // lblChangePasswordCurrentPasswordError
            // 
            this.lblChangePasswordCurrentPasswordError.AutoSize = true;
            this.lblChangePasswordCurrentPasswordError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblChangePasswordCurrentPasswordError.Location = new System.Drawing.Point(215, 108);
            this.lblChangePasswordCurrentPasswordError.Name = "lblChangePasswordCurrentPasswordError";
            this.lblChangePasswordCurrentPasswordError.Size = new System.Drawing.Size(0, 16);
            this.lblChangePasswordCurrentPasswordError.TabIndex = 3;
            // 
            // lblChangePasswordNewPassword
            // 
            this.lblChangePasswordNewPassword.AutoSize = true;
            this.lblChangePasswordNewPassword.Location = new System.Drawing.Point(45, 153);
            this.lblChangePasswordNewPassword.Name = "lblChangePasswordNewPassword";
            this.lblChangePasswordNewPassword.Size = new System.Drawing.Size(97, 16);
            this.lblChangePasswordNewPassword.TabIndex = 3;
            this.lblChangePasswordNewPassword.Text = "New Password";
            // 
            // txtChangePasswordNewPassword
            // 
            this.txtChangePasswordNewPassword.Location = new System.Drawing.Point(215, 147);
            this.txtChangePasswordNewPassword.Name = "txtChangePasswordNewPassword";
            this.txtChangePasswordNewPassword.Size = new System.Drawing.Size(260, 22);
            this.txtChangePasswordNewPassword.TabIndex = 4;
            this.txtChangePasswordNewPassword.UseSystemPasswordChar = true;
            this.txtChangePasswordNewPassword.TextChanged += new System.EventHandler(this.txtChangePasswordNewPassword_TextChanged);
            // 
            // lblChangePasswordNewPasswordError
            // 
            this.lblChangePasswordNewPasswordError.AutoSize = true;
            this.lblChangePasswordNewPasswordError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblChangePasswordNewPasswordError.Location = new System.Drawing.Point(215, 175);
            this.lblChangePasswordNewPasswordError.Name = "lblChangePasswordNewPasswordError";
            this.lblChangePasswordNewPasswordError.Size = new System.Drawing.Size(0, 16);
            this.lblChangePasswordNewPasswordError.TabIndex = 5;
            // 
            // lblChangePasswordConfirmPassword
            // 
            this.lblChangePasswordConfirmPassword.AutoSize = true;
            this.lblChangePasswordConfirmPassword.Location = new System.Drawing.Point(45, 258);
            this.lblChangePasswordConfirmPassword.Name = "lblChangePasswordConfirmPassword";
            this.lblChangePasswordConfirmPassword.Size = new System.Drawing.Size(145, 16);
            this.lblChangePasswordConfirmPassword.TabIndex = 5;
            this.lblChangePasswordConfirmPassword.Text = "Confirm New Password";
            // 
            // txtChangePasswordConfirmPassword
            // 
            this.txtChangePasswordConfirmPassword.Location = new System.Drawing.Point(215, 252);
            this.txtChangePasswordConfirmPassword.Name = "txtChangePasswordConfirmPassword";
            this.txtChangePasswordConfirmPassword.Size = new System.Drawing.Size(260, 22);
            this.txtChangePasswordConfirmPassword.TabIndex = 6;
            this.txtChangePasswordConfirmPassword.UseSystemPasswordChar = true;
            this.txtChangePasswordConfirmPassword.TextChanged += new System.EventHandler(this.txtChangePasswordConfirmPassword_TextChanged);
            // 
            // lblChangePasswordConfirmPasswordError
            // 
            this.lblChangePasswordConfirmPasswordError.AutoSize = true;
            this.lblChangePasswordConfirmPasswordError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblChangePasswordConfirmPasswordError.Location = new System.Drawing.Point(215, 280);
            this.lblChangePasswordConfirmPasswordError.Name = "lblChangePasswordConfirmPasswordError";
            this.lblChangePasswordConfirmPasswordError.Size = new System.Drawing.Size(0, 16);
            this.lblChangePasswordConfirmPasswordError.TabIndex = 7;
            // 
            // btnChangePasswordSubmit
            // 
            this.btnChangePasswordSubmit.Location = new System.Drawing.Point(215, 315);
            this.btnChangePasswordSubmit.Name = "btnChangePasswordSubmit";
            this.btnChangePasswordSubmit.Size = new System.Drawing.Size(125, 34);
            this.btnChangePasswordSubmit.TabIndex = 7;
            this.btnChangePasswordSubmit.Text = "Change Password";
            this.btnChangePasswordSubmit.UseVisualStyleBackColor = true;
            this.btnChangePasswordSubmit.Click += new System.EventHandler(this.btnChangePasswordSubmit_Click);
            // 
            // btnChangePasswordCancel
            // 
            this.btnChangePasswordCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnChangePasswordCancel.Location = new System.Drawing.Point(350, 315);
            this.btnChangePasswordCancel.Name = "btnChangePasswordCancel";
            this.btnChangePasswordCancel.Size = new System.Drawing.Size(125, 34);
            this.btnChangePasswordCancel.TabIndex = 8;
            this.btnChangePasswordCancel.Text = "Cancel";
            this.btnChangePasswordCancel.UseVisualStyleBackColor = true;
            this.btnChangePasswordCancel.Click += new System.EventHandler(this.btnChangePasswordCancel_Click);
            // 
            // FrmChangePassword
            // 
            this.AcceptButton = this.btnChangePasswordSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.CancelButton = this.btnChangePasswordCancel;
            this.ClientSize = new System.Drawing.Size(663, 468);
            this.Controls.Add(this.btnChangePasswordCancel);
            this.Controls.Add(this.btnChangePasswordSubmit);
            this.Controls.Add(this.lblChangePasswordConfirmPasswordError);
            this.Controls.Add(this.txtChangePasswordConfirmPassword);
            this.Controls.Add(this.lblChangePasswordConfirmPassword);
            this.Controls.Add(this.lblChangePasswordNewPasswordError);
            this.Controls.Add(this.txtChangePasswordNewPassword);
            this.Controls.Add(this.lblChangePasswordNewPassword);
            this.Controls.Add(this.lblChangePasswordCurrentPasswordError);
            this.Controls.Add(this.txtChangePasswordCurrentPassword);
            this.Controls.Add(this.lblChangePasswordCurrentPassword);
            this.Controls.Add(this.lblChangePasswordTitle);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(49)))), ((int)(((byte)(58)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Password";
            this.Load += new System.EventHandler(this.FrmChangePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblChangePasswordTitle;
        private System.Windows.Forms.Label lblChangePasswordCurrentPassword;
        private System.Windows.Forms.TextBox txtChangePasswordCurrentPassword;
        private System.Windows.Forms.Label lblChangePasswordCurrentPasswordError;
        private System.Windows.Forms.Label lblChangePasswordNewPassword;
        private System.Windows.Forms.TextBox txtChangePasswordNewPassword;
        private System.Windows.Forms.Label lblChangePasswordNewPasswordError;
        private System.Windows.Forms.Label lblChangePasswordConfirmPassword;
        private System.Windows.Forms.TextBox txtChangePasswordConfirmPassword;
        private System.Windows.Forms.Label lblChangePasswordConfirmPasswordError;
        private System.Windows.Forms.Button btnChangePasswordSubmit;
        private System.Windows.Forms.Button btnChangePasswordCancel;
    }
}