namespace MoneyFlow.View
{
    partial class FrmChangePassword
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
            this.components = new System.ComponentModel.Container();
            this.lblChangePasswordTitle = new System.Windows.Forms.Label();
            this.lblChangePasswordCurrentPassword = new System.Windows.Forms.Label();
            this.txtChangePasswordCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblChangePasswordNewPassword = new System.Windows.Forms.Label();
            this.txtChangePasswordNewPassword = new System.Windows.Forms.TextBox();
            this.lblChangePasswordConfirmPassword = new System.Windows.Forms.Label();
            this.txtChangePasswordConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnChangePasswordSubmit = new System.Windows.Forms.Button();
            this.btnChangePasswordCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblChangePasswordTitle
            // 
            this.lblChangePasswordTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChangePasswordTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChangePasswordTitle.Location = new System.Drawing.Point(20, 20);
            this.lblChangePasswordTitle.Name = "lblChangePasswordTitle";
            this.lblChangePasswordTitle.Size = new System.Drawing.Size(480, 30);
            this.lblChangePasswordTitle.TabIndex = 0;
            this.lblChangePasswordTitle.Text = "Change Password";
            this.lblChangePasswordTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChangePasswordCurrentPassword
            // 
            this.lblChangePasswordCurrentPassword.AutoSize = true;
            this.lblChangePasswordCurrentPassword.Location = new System.Drawing.Point(40, 75);
            this.lblChangePasswordCurrentPassword.Name = "lblChangePasswordCurrentPassword";
            this.lblChangePasswordCurrentPassword.Size = new System.Drawing.Size(108, 15);
            this.lblChangePasswordCurrentPassword.TabIndex = 1;
            this.lblChangePasswordCurrentPassword.Text = "Current Password :";
            // 
            // txtChangePasswordCurrentPassword
            // 
            this.txtChangePasswordCurrentPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChangePasswordCurrentPassword.Location = new System.Drawing.Point(190, 72);
            this.txtChangePasswordCurrentPassword.Name = "txtChangePasswordCurrentPassword";
            this.txtChangePasswordCurrentPassword.Size = new System.Drawing.Size(290, 23);
            this.txtChangePasswordCurrentPassword.TabIndex = 2;
            this.txtChangePasswordCurrentPassword.UseSystemPasswordChar = true;
            // 
            // lblChangePasswordNewPassword
            // 
            this.lblChangePasswordNewPassword.AutoSize = true;
            this.lblChangePasswordNewPassword.Location = new System.Drawing.Point(40, 118);
            this.lblChangePasswordNewPassword.Name = "lblChangePasswordNewPassword";
            this.lblChangePasswordNewPassword.Size = new System.Drawing.Size(90, 15);
            this.lblChangePasswordNewPassword.TabIndex = 3;
            this.lblChangePasswordNewPassword.Text = "New Password :";
            // 
            // txtChangePasswordNewPassword
            // 
            this.txtChangePasswordNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChangePasswordNewPassword.Location = new System.Drawing.Point(190, 115);
            this.txtChangePasswordNewPassword.Name = "txtChangePasswordNewPassword";
            this.txtChangePasswordNewPassword.Size = new System.Drawing.Size(290, 23);
            this.txtChangePasswordNewPassword.TabIndex = 4;
            this.txtChangePasswordNewPassword.UseSystemPasswordChar = true;
            // 
            // lblChangePasswordConfirmPassword
            // 
            this.lblChangePasswordConfirmPassword.AutoSize = true;
            this.lblChangePasswordConfirmPassword.Location = new System.Drawing.Point(40, 161);
            this.lblChangePasswordConfirmPassword.Name = "lblChangePasswordConfirmPassword";
            this.lblChangePasswordConfirmPassword.Size = new System.Drawing.Size(137, 15);
            this.lblChangePasswordConfirmPassword.TabIndex = 5;
            this.lblChangePasswordConfirmPassword.Text = "Confirm New Password :";
            // 
            // txtChangePasswordConfirmPassword
            // 
            this.txtChangePasswordConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChangePasswordConfirmPassword.Location = new System.Drawing.Point(190, 158);
            this.txtChangePasswordConfirmPassword.Name = "txtChangePasswordConfirmPassword";
            this.txtChangePasswordConfirmPassword.Size = new System.Drawing.Size(290, 23);
            this.txtChangePasswordConfirmPassword.TabIndex = 6;
            this.txtChangePasswordConfirmPassword.UseSystemPasswordChar = true;
            // 
            // btnChangePasswordSubmit
            // 
            this.btnChangePasswordSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePasswordSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnChangePasswordSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePasswordSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePasswordSubmit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnChangePasswordSubmit.ForeColor = System.Drawing.Color.White;
            this.btnChangePasswordSubmit.Location = new System.Drawing.Point(220, 218);
            this.btnChangePasswordSubmit.Name = "btnChangePasswordSubmit";
            this.btnChangePasswordSubmit.Size = new System.Drawing.Size(130, 34);
            this.btnChangePasswordSubmit.TabIndex = 7;
            this.btnChangePasswordSubmit.Text = "Change Password";
            this.btnChangePasswordSubmit.UseVisualStyleBackColor = false;
            this.btnChangePasswordSubmit.Click += new System.EventHandler(this.btnChangePasswordSubmit_Click);
            // 
            // btnChangePasswordCancel
            // 
            this.btnChangePasswordCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePasswordCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePasswordCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnChangePasswordCancel.Location = new System.Drawing.Point(360, 218);
            this.btnChangePasswordCancel.Name = "btnChangePasswordCancel";
            this.btnChangePasswordCancel.Size = new System.Drawing.Size(120, 34);
            this.btnChangePasswordCancel.TabIndex = 8;
            this.btnChangePasswordCancel.Text = "Cancel";
            this.btnChangePasswordCancel.UseVisualStyleBackColor = true;
            this.btnChangePasswordCancel.Click += new System.EventHandler(this.btnChangePasswordCancel_Click);
            // 
            // FrmChangePassword
            // 
            this.AcceptButton = this.btnChangePasswordSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btnChangePasswordCancel;
            this.ClientSize = new System.Drawing.Size(520, 280);
            this.Controls.Add(this.btnChangePasswordCancel);
            this.Controls.Add(this.btnChangePasswordSubmit);
            this.Controls.Add(this.txtChangePasswordConfirmPassword);
            this.Controls.Add(this.lblChangePasswordConfirmPassword);
            this.Controls.Add(this.txtChangePasswordNewPassword);
            this.Controls.Add(this.lblChangePasswordNewPassword);
            this.Controls.Add(this.txtChangePasswordCurrentPassword);
            this.Controls.Add(this.lblChangePasswordCurrentPassword);
            this.Controls.Add(this.lblChangePasswordTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(480, 310);
            this.Name = "FrmChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblChangePasswordTitle;
        private System.Windows.Forms.Label lblChangePasswordCurrentPassword;
        private System.Windows.Forms.TextBox txtChangePasswordCurrentPassword;
        private System.Windows.Forms.Label lblChangePasswordNewPassword;
        private System.Windows.Forms.TextBox txtChangePasswordNewPassword;
        private System.Windows.Forms.Label lblChangePasswordConfirmPassword;
        private System.Windows.Forms.TextBox txtChangePasswordConfirmPassword;
        private System.Windows.Forms.Button btnChangePasswordSubmit;
        private System.Windows.Forms.Button btnChangePasswordCancel;
    }
}