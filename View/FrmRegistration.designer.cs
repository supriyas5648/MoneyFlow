namespace MoneyFlow
{
    public partial class FrmRegistration : System.Windows.Forms.Form
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
            this.lblRegistrationTitle = new System.Windows.Forms.Label();
            this.lblRegistrationFullName = new System.Windows.Forms.Label();
            this.txtRegistrationFullName = new System.Windows.Forms.TextBox();
            this.lblRegistrationFullNameError = new System.Windows.Forms.Label();
            this.lblRegistrationUsername = new System.Windows.Forms.Label();
            this.txtRegistrationUsername = new System.Windows.Forms.TextBox();
            this.lblRegistrationUsernameError = new System.Windows.Forms.Label();
            this.lblRegistrationPassword = new System.Windows.Forms.Label();
            this.txtRegistrationPassword = new System.Windows.Forms.TextBox();
            this.chkShowRegistrationPassword = new System.Windows.Forms.CheckBox();
            this.lblRegistrationPasswordError = new System.Windows.Forms.Label();
            this.lblRegistrationConfirmPassword = new System.Windows.Forms.Label();
            this.txtRegistrationConfirmPassword = new System.Windows.Forms.TextBox();
            this.chkShowRegistrationConfirmPassword = new System.Windows.Forms.CheckBox();
            this.lblRegistrationConfirmPasswordError = new System.Windows.Forms.Label();
            this.btnRegistrationRegister = new System.Windows.Forms.Button();
            this.btnRegistrationClear = new System.Windows.Forms.Button();
            this.btnRegistrationLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRegistrationTitle
            // 
            this.lblRegistrationTitle.AutoSize = true;
            this.lblRegistrationTitle.Location = new System.Drawing.Point(284, 28);
            this.lblRegistrationTitle.Name = "lblRegistrationTitle";
            this.lblRegistrationTitle.Size = new System.Drawing.Size(79, 16);
            this.lblRegistrationTitle.TabIndex = 0;
            this.lblRegistrationTitle.Text = "Registration";
            this.lblRegistrationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRegistrationFullName
            // 
            this.lblRegistrationFullName.AutoSize = true;
            this.lblRegistrationFullName.Location = new System.Drawing.Point(151, 92);
            this.lblRegistrationFullName.Name = "lblRegistrationFullName";
            this.lblRegistrationFullName.Size = new System.Drawing.Size(68, 16);
            this.lblRegistrationFullName.TabIndex = 1;
            this.lblRegistrationFullName.Text = "Full Name";
            // 
            // txtRegistrationFullName
            // 
            this.txtRegistrationFullName.Location = new System.Drawing.Point(302, 88);
            this.txtRegistrationFullName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRegistrationFullName.Name = "txtRegistrationFullName";
            this.txtRegistrationFullName.Size = new System.Drawing.Size(223, 22);
            this.txtRegistrationFullName.TabIndex = 2;
            this.txtRegistrationFullName.TextChanged += new System.EventHandler(this.txtRegistrationFullName_TextChanged);
            // 
            // lblRegistrationFullNameError
            // 
            this.lblRegistrationFullNameError.AutoSize = true;
            this.lblRegistrationFullNameError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationFullNameError.Location = new System.Drawing.Point(302, 111);
            this.lblRegistrationFullNameError.MaximumSize = new System.Drawing.Size(223, 0);
            this.lblRegistrationFullNameError.Name = "lblRegistrationFullNameError";
            this.lblRegistrationFullNameError.Size = new System.Drawing.Size(0, 16);
            this.lblRegistrationFullNameError.TabIndex = 12;
            // 
            // lblRegistrationUsername
            // 
            this.lblRegistrationUsername.AutoSize = true;
            this.lblRegistrationUsername.Location = new System.Drawing.Point(151, 144);
            this.lblRegistrationUsername.Name = "lblRegistrationUsername";
            this.lblRegistrationUsername.Size = new System.Drawing.Size(70, 16);
            this.lblRegistrationUsername.TabIndex = 3;
            this.lblRegistrationUsername.Text = "Username";
            // 
            // txtRegistrationUsername
            // 
            this.txtRegistrationUsername.Location = new System.Drawing.Point(302, 140);
            this.txtRegistrationUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRegistrationUsername.Name = "txtRegistrationUsername";
            this.txtRegistrationUsername.Size = new System.Drawing.Size(223, 22);
            this.txtRegistrationUsername.TabIndex = 4;
            this.txtRegistrationUsername.TextChanged += new System.EventHandler(this.txtRegistrationUsername_TextChanged);
            // 
            // lblRegistrationUsernameError
            // 
            this.lblRegistrationUsernameError.AutoSize = true;
            this.lblRegistrationUsernameError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationUsernameError.Location = new System.Drawing.Point(302, 163);
            this.lblRegistrationUsernameError.MaximumSize = new System.Drawing.Size(223, 0);
            this.lblRegistrationUsernameError.Name = "lblRegistrationUsernameError";
            this.lblRegistrationUsernameError.Size = new System.Drawing.Size(0, 16);
            this.lblRegistrationUsernameError.TabIndex = 13;
            // 
            // lblRegistrationPassword
            // 
            this.lblRegistrationPassword.AutoSize = true;
            this.lblRegistrationPassword.Location = new System.Drawing.Point(151, 233);
            this.lblRegistrationPassword.Name = "lblRegistrationPassword";
            this.lblRegistrationPassword.Size = new System.Drawing.Size(67, 16);
            this.lblRegistrationPassword.TabIndex = 5;
            this.lblRegistrationPassword.Text = "Password";
            // 
            // txtRegistrationPassword
            // 
            this.txtRegistrationPassword.Location = new System.Drawing.Point(302, 227);
            this.txtRegistrationPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRegistrationPassword.Name = "txtRegistrationPassword";
            this.txtRegistrationPassword.Size = new System.Drawing.Size(223, 22);
            this.txtRegistrationPassword.TabIndex = 6;
            this.txtRegistrationPassword.UseSystemPasswordChar = true;
            this.txtRegistrationPassword.TextChanged += new System.EventHandler(this.txtRegistrationPassword_TextChanged);
            // 
            // chkShowRegistrationPassword
            // 
            this.chkShowRegistrationPassword.AutoSize = true;
            this.chkShowRegistrationPassword.Location = new System.Drawing.Point(531, 229);
            this.chkShowRegistrationPassword.Name = "chkShowRegistrationPassword";
            this.chkShowRegistrationPassword.Size = new System.Drawing.Size(51, 20);
            this.chkShowRegistrationPassword.TabIndex = 7;
            this.chkShowRegistrationPassword.Text = "Show";
            this.chkShowRegistrationPassword.UseVisualStyleBackColor = true;
            this.chkShowRegistrationPassword.CheckedChanged += new System.EventHandler(this.chkShowRegistrationPassword_CheckedChanged);
            // 
            // lblRegistrationPasswordError
            // 
            this.lblRegistrationPasswordError.AutoSize = true;
            this.lblRegistrationPasswordError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationPasswordError.Location = new System.Drawing.Point(302, 253);
            this.lblRegistrationPasswordError.MaximumSize = new System.Drawing.Size(223, 0);
            this.lblRegistrationPasswordError.Name = "lblRegistrationPasswordError";
            this.lblRegistrationPasswordError.Size = new System.Drawing.Size(0, 16);
            this.lblRegistrationPasswordError.TabIndex = 14;
            // 
            // lblRegistrationConfirmPassword
            // 
            this.lblRegistrationConfirmPassword.AutoSize = true;
            this.lblRegistrationConfirmPassword.Location = new System.Drawing.Point(143, 306);
            this.lblRegistrationConfirmPassword.Name = "lblRegistrationConfirmPassword";
            this.lblRegistrationConfirmPassword.Size = new System.Drawing.Size(115, 16);
            this.lblRegistrationConfirmPassword.TabIndex = 7;
            this.lblRegistrationConfirmPassword.Text = "Confirm Password";
            // 
            // txtRegistrationConfirmPassword
            // 
            this.txtRegistrationConfirmPassword.Location = new System.Drawing.Point(302, 303);
            this.txtRegistrationConfirmPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRegistrationConfirmPassword.Name = "txtRegistrationConfirmPassword";
            this.txtRegistrationConfirmPassword.Size = new System.Drawing.Size(223, 22);
            this.txtRegistrationConfirmPassword.TabIndex = 8;
            this.txtRegistrationConfirmPassword.UseSystemPasswordChar = true;
            this.txtRegistrationConfirmPassword.TextChanged += new System.EventHandler(this.txtRegistrationConfirmPassword_TextChanged);
            // 
            // chkShowRegistrationConfirmPassword
            // 
            this.chkShowRegistrationConfirmPassword.AutoSize = true;
            this.chkShowRegistrationConfirmPassword.Location = new System.Drawing.Point(531, 305);
            this.chkShowRegistrationConfirmPassword.Name = "chkShowRegistrationConfirmPassword";
            this.chkShowRegistrationConfirmPassword.Size = new System.Drawing.Size(51, 20);
            this.chkShowRegistrationConfirmPassword.TabIndex = 9;
            this.chkShowRegistrationConfirmPassword.Text = "Show";
            this.chkShowRegistrationConfirmPassword.UseVisualStyleBackColor = true;
            this.chkShowRegistrationConfirmPassword.CheckedChanged += new System.EventHandler(this.chkShowRegistrationConfirmPassword_CheckedChanged);
            // 
            // lblRegistrationConfirmPasswordError
            // 
            this.lblRegistrationConfirmPasswordError.AutoSize = true;
            this.lblRegistrationConfirmPasswordError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationConfirmPasswordError.Location = new System.Drawing.Point(302, 328);
            this.lblRegistrationConfirmPasswordError.MaximumSize = new System.Drawing.Size(223, 0);
            this.lblRegistrationConfirmPasswordError.Name = "lblRegistrationConfirmPasswordError";
            this.lblRegistrationConfirmPasswordError.Size = new System.Drawing.Size(0, 16);
            this.lblRegistrationConfirmPasswordError.TabIndex = 15;
            // 
            // btnRegistrationRegister
            // 
            this.btnRegistrationRegister.Location = new System.Drawing.Point(151, 384);
            this.btnRegistrationRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegistrationRegister.Name = "btnRegistrationRegister";
            this.btnRegistrationRegister.Size = new System.Drawing.Size(107, 32);
            this.btnRegistrationRegister.TabIndex = 9;
            this.btnRegistrationRegister.Text = "Register";
            this.btnRegistrationRegister.UseVisualStyleBackColor = true;
            this.btnRegistrationRegister.Click += new System.EventHandler(this.btnRegistrationRegister_Click);
            // 
            // btnRegistrationClear
            // 
            this.btnRegistrationClear.Location = new System.Drawing.Point(284, 384);
            this.btnRegistrationClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegistrationClear.Name = "btnRegistrationClear";
            this.btnRegistrationClear.Size = new System.Drawing.Size(107, 32);
            this.btnRegistrationClear.TabIndex = 10;
            this.btnRegistrationClear.Text = "Clear";
            this.btnRegistrationClear.UseVisualStyleBackColor = true;
            this.btnRegistrationClear.Click += new System.EventHandler(this.btnRegistrationClear_Click);
            // 
            // btnRegistrationLogin
            // 
            this.btnRegistrationLogin.Location = new System.Drawing.Point(408, 384);
            this.btnRegistrationLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegistrationLogin.Name = "btnRegistrationLogin";
            this.btnRegistrationLogin.Size = new System.Drawing.Size(107, 32);
            this.btnRegistrationLogin.TabIndex = 11;
            this.btnRegistrationLogin.Text = "Login";
            this.btnRegistrationLogin.UseVisualStyleBackColor = true;
            this.btnRegistrationLogin.Click += new System.EventHandler(this.btnRegistrationLogin_Click);
            // 
            // FrmRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(711, 493);
            this.Controls.Add(this.btnRegistrationLogin);
            this.Controls.Add(this.btnRegistrationClear);
            this.Controls.Add(this.btnRegistrationRegister);
            this.Controls.Add(this.lblRegistrationConfirmPasswordError);
            this.Controls.Add(this.txtRegistrationConfirmPassword);
            this.Controls.Add(this.chkShowRegistrationConfirmPassword);
            this.Controls.Add(this.lblRegistrationConfirmPassword);
            this.Controls.Add(this.lblRegistrationPasswordError);
            this.Controls.Add(this.chkShowRegistrationPassword);
            this.Controls.Add(this.txtRegistrationPassword);
            this.Controls.Add(this.lblRegistrationPassword);
            this.Controls.Add(this.lblRegistrationUsernameError);
            this.Controls.Add(this.txtRegistrationUsername);
            this.Controls.Add(this.lblRegistrationUsername);
            this.Controls.Add(this.lblRegistrationFullNameError);
            this.Controls.Add(this.txtRegistrationFullName);
            this.Controls.Add(this.lblRegistrationFullName);
            this.Controls.Add(this.lblRegistrationTitle);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(49)))), ((int)(((byte)(58)))));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registration";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblRegistrationTitle;
        private System.Windows.Forms.Label lblRegistrationFullName;
        private System.Windows.Forms.TextBox txtRegistrationFullName;
        private System.Windows.Forms.Label lblRegistrationFullNameError;
        private System.Windows.Forms.Label lblRegistrationUsername;
        private System.Windows.Forms.TextBox txtRegistrationUsername;
        private System.Windows.Forms.Label lblRegistrationUsernameError;
        private System.Windows.Forms.Label lblRegistrationPassword;
        private System.Windows.Forms.TextBox txtRegistrationPassword;
        private System.Windows.Forms.CheckBox chkShowRegistrationPassword;
        private System.Windows.Forms.Label lblRegistrationPasswordError;
        private System.Windows.Forms.Label lblRegistrationConfirmPassword;
        private System.Windows.Forms.TextBox txtRegistrationConfirmPassword;
        private System.Windows.Forms.CheckBox chkShowRegistrationConfirmPassword;
        private System.Windows.Forms.Label lblRegistrationConfirmPasswordError;
        private System.Windows.Forms.Button btnRegistrationRegister;
        private System.Windows.Forms.Button btnRegistrationClear;
        private System.Windows.Forms.Button btnRegistrationLogin;
    }
}