namespace MoneyFlow.View
{
    partial class FrmSetting
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpChangePassword;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnUpdatePassword;

        private System.Windows.Forms.GroupBox grpAppearance;
        private System.Windows.Forms.Button btnChooseFont;
        private System.Windows.Forms.Button btnChooseColor;
        private System.Windows.Forms.Label lblFontInfo;
        private System.Windows.Forms.Label lblColorInfo;
        private System.Windows.Forms.Label lblPreviewTitle;
        private System.Windows.Forms.Label lblPreviewSample;
        private System.Windows.Forms.Button btnSaveAppearance;

        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpChangePassword = new System.Windows.Forms.GroupBox();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnUpdatePassword = new System.Windows.Forms.Button();

            this.grpAppearance = new System.Windows.Forms.GroupBox();
            this.btnChooseFont = new System.Windows.Forms.Button();
            this.btnChooseColor = new System.Windows.Forms.Button();
            this.lblFontInfo = new System.Windows.Forms.Label();
            this.lblColorInfo = new System.Windows.Forms.Label();
            this.lblPreviewTitle = new System.Windows.Forms.Label();
            this.lblPreviewSample = new System.Windows.Forms.Label();
            this.btnSaveAppearance = new System.Windows.Forms.Button();

            this.btnClose = new System.Windows.Forms.Button();

            this.grpChangePassword.SuspendLayout();
            this.grpAppearance.SuspendLayout();
            this.SuspendLayout();

            // 
            // grpChangePassword
            // 
            this.grpChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpChangePassword.Controls.Add(this.lblCurrentPassword);
            this.grpChangePassword.Controls.Add(this.txtCurrentPassword);
            this.grpChangePassword.Controls.Add(this.lblNewPassword);
            this.grpChangePassword.Controls.Add(this.txtNewPassword);
            this.grpChangePassword.Controls.Add(this.lblConfirmPassword);
            this.grpChangePassword.Controls.Add(this.txtConfirmPassword);
            this.grpChangePassword.Controls.Add(this.btnUpdatePassword);
            this.grpChangePassword.Location = new System.Drawing.Point(20, 16);
            this.grpChangePassword.Name = "grpChangePassword";
            this.grpChangePassword.Size = new System.Drawing.Size(460, 200);
            this.grpChangePassword.TabIndex = 0;
            this.grpChangePassword.TabStop = false;
            this.grpChangePassword.Text = "Change Password";

            // lblCurrentPassword
            this.lblCurrentPassword.AutoSize = true;
            this.lblCurrentPassword.Location = new System.Drawing.Point(20, 32);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(103, 15);
            this.lblCurrentPassword.TabIndex = 0;
            this.lblCurrentPassword.Text = "Current Password:";

            // txtCurrentPassword
            this.txtCurrentPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentPassword.Location = new System.Drawing.Point(150, 29);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.PasswordChar = '*';
            this.txtCurrentPassword.Size = new System.Drawing.Size(280, 23);
            this.txtCurrentPassword.TabIndex = 1;

            // lblNewPassword
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(20, 68);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(87, 15);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "New Password:";

            // txtNewPassword
            this.txtNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPassword.Location = new System.Drawing.Point(150, 65);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(280, 23);
            this.txtNewPassword.TabIndex = 3;

            // lblConfirmPassword
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(20, 104);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(107, 15);
            this.lblConfirmPassword.TabIndex = 4;
            this.lblConfirmPassword.Text = "Confirm Password:";

            // txtConfirmPassword
            this.txtConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(150, 101);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(280, 23);
            this.txtConfirmPassword.TabIndex = 5;

            // btnUpdatePassword
            this.btnUpdatePassword.Location = new System.Drawing.Point(150, 142);
            this.btnUpdatePassword.Name = "btnUpdatePassword";
            this.btnUpdatePassword.Size = new System.Drawing.Size(140, 32);
            this.btnUpdatePassword.TabIndex = 6;
            this.btnUpdatePassword.Text = "Update Password";
            this.btnUpdatePassword.UseVisualStyleBackColor = true;
            this.btnUpdatePassword.Click += new System.EventHandler(this.btnUpdatePassword_Click);

            // 
            // grpAppearance
            // 
            this.grpAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAppearance.Controls.Add(this.btnChooseFont);
            this.grpAppearance.Controls.Add(this.lblFontInfo);
            this.grpAppearance.Controls.Add(this.btnChooseColor);
            this.grpAppearance.Controls.Add(this.lblColorInfo);
            this.grpAppearance.Controls.Add(this.lblPreviewTitle);
            this.grpAppearance.Controls.Add(this.lblPreviewSample);
            this.grpAppearance.Controls.Add(this.btnSaveAppearance);
            this.grpAppearance.Location = new System.Drawing.Point(20, 230);
            this.grpAppearance.Name = "grpAppearance";
            this.grpAppearance.Size = new System.Drawing.Size(460, 230);
            this.grpAppearance.TabIndex = 1;
            this.grpAppearance.TabStop = false;
            this.grpAppearance.Text = "Display Settings (Font & Color)";

            // btnChooseFont
            this.btnChooseFont.Location = new System.Drawing.Point(20, 28);
            this.btnChooseFont.Name = "btnChooseFont";
            this.btnChooseFont.Size = new System.Drawing.Size(130, 30);
            this.btnChooseFont.TabIndex = 0;
            this.btnChooseFont.Text = "Change Font...";
            this.btnChooseFont.UseVisualStyleBackColor = true;
            this.btnChooseFont.Click += new System.EventHandler(this.btnChooseFont_Click);

            // lblFontInfo
            this.lblFontInfo.AutoSize = true;
            this.lblFontInfo.Location = new System.Drawing.Point(165, 36);
            this.lblFontInfo.Name = "lblFontInfo";
            this.lblFontInfo.Size = new System.Drawing.Size(100, 15);
            this.lblFontInfo.TabIndex = 1;
            this.lblFontInfo.Text = "Current: Arial, 12pt";

            // btnChooseColor
            this.btnChooseColor.Location = new System.Drawing.Point(20, 68);
            this.btnChooseColor.Name = "btnChooseColor";
            this.btnChooseColor.Size = new System.Drawing.Size(130, 30);
            this.btnChooseColor.TabIndex = 2;
            this.btnChooseColor.Text = "Change Color...";
            this.btnChooseColor.UseVisualStyleBackColor = true;
            this.btnChooseColor.Click += new System.EventHandler(this.btnChooseColor_Click);

            // lblColorInfo
            this.lblColorInfo.AutoSize = true;
            this.lblColorInfo.Location = new System.Drawing.Point(165, 76);
            this.lblColorInfo.Name = "lblColorInfo";
            this.lblColorInfo.Size = new System.Drawing.Size(117, 15);
            this.lblColorInfo.TabIndex = 3;
            this.lblColorInfo.Text = "Current: Black (#000)";

            // lblPreviewTitle
            this.lblPreviewTitle.AutoSize = true;
            this.lblPreviewTitle.Location = new System.Drawing.Point(20, 114);
            this.lblPreviewTitle.Name = "lblPreviewTitle";
            this.lblPreviewTitle.Size = new System.Drawing.Size(86, 15);
            this.lblPreviewTitle.TabIndex = 4;
            this.lblPreviewTitle.Text = "Sample Preview:";

            // lblPreviewSample
            this.lblPreviewSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPreviewSample.BackColor = System.Drawing.Color.White;
            this.lblPreviewSample.Location = new System.Drawing.Point(20, 136);
            this.lblPreviewSample.Name = "lblPreviewSample";
            this.lblPreviewSample.Size = new System.Drawing.Size(410, 42);
            this.lblPreviewSample.TabIndex = 5;
            this.lblPreviewSample.Text = "  ₹5,000.00  |  2026-09-01  |  Salary  |  Monthly Income";
            this.lblPreviewSample.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnSaveAppearance
            this.btnSaveAppearance.Location = new System.Drawing.Point(20, 186);
            this.btnSaveAppearance.Name = "btnSaveAppearance";
            this.btnSaveAppearance.Size = new System.Drawing.Size(150, 32);
            this.btnSaveAppearance.TabIndex = 6;
            this.btnSaveAppearance.Text = "Save Appearance";
            this.btnSaveAppearance.UseVisualStyleBackColor = true;
            this.btnSaveAppearance.Click += new System.EventHandler(this.btnSaveAppearance_Click);

            // btnClose
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(380, 474);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(504, 520);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpAppearance);
            this.Controls.Add(this.grpChangePassword);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(520, 560);
            this.Name = "FrmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings & Appearance";
            this.grpChangePassword.ResumeLayout(false);
            this.grpChangePassword.PerformLayout();
            this.grpAppearance.ResumeLayout(false);
            this.grpAppearance.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}