namespace MoneyFlow.View
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
            this.pnlRegistrationCard = new System.Windows.Forms.Panel();
            this.lblRegistrationTitle = new System.Windows.Forms.Label();
            this.lblRegistrationFullName = new System.Windows.Forms.Label();
            this.txtRegistrationFullName = new System.Windows.Forms.TextBox();
            this.lblRegistrationFullNameError = new System.Windows.Forms.Label();
            this.lblRegistrationUsername = new System.Windows.Forms.Label();
            this.txtRegistrationUsername = new System.Windows.Forms.TextBox();
            this.lblRegistrationUsernameError = new System.Windows.Forms.Label();
            this.lblRegistrationPassword = new System.Windows.Forms.Label();
            this.txtRegistrationPassword = new System.Windows.Forms.TextBox();
            this.lblRegistrationPasswordError = new System.Windows.Forms.Label();
            this.lblRegistrationConfirmPassword = new System.Windows.Forms.Label();
            this.txtRegistrationConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblRegistrationConfirmPasswordError = new System.Windows.Forms.Label();
            this.btnRegistrationRegister = new System.Windows.Forms.Button();
            this.btnRegistrationClear = new System.Windows.Forms.Button();
            this.btnRegistrationLogin = new System.Windows.Forms.Button();
            this.pnlRegistrationCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRegistrationCard
            // 
            this.pnlRegistrationCard.BackColor = System.Drawing.SystemColors.Window;
            this.pnlRegistrationCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationTitle);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationFullName);
            this.pnlRegistrationCard.Controls.Add(this.txtRegistrationFullName);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationFullNameError);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationUsername);
            this.pnlRegistrationCard.Controls.Add(this.txtRegistrationUsername);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationUsernameError);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationPassword);
            this.pnlRegistrationCard.Controls.Add(this.txtRegistrationPassword);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationPasswordError);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationConfirmPassword);
            this.pnlRegistrationCard.Controls.Add(this.txtRegistrationConfirmPassword);
            this.pnlRegistrationCard.Controls.Add(this.lblRegistrationConfirmPasswordError);
            this.pnlRegistrationCard.Controls.Add(this.btnRegistrationRegister);
            this.pnlRegistrationCard.Controls.Add(this.btnRegistrationClear);
            this.pnlRegistrationCard.Controls.Add(this.btnRegistrationLogin);
            this.pnlRegistrationCard.Location = new System.Drawing.Point(55, 30);
            this.pnlRegistrationCard.Name = "pnlRegistrationCard";
            this.pnlRegistrationCard.Size = new System.Drawing.Size(650, 440);
            this.pnlRegistrationCard.TabIndex = 0;
            // 
            // lblRegistrationTitle
            // 
            this.lblRegistrationTitle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblRegistrationTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRegistrationTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblRegistrationTitle.Location = new System.Drawing.Point(20, 20);
            this.lblRegistrationTitle.Name = "lblRegistrationTitle";
            this.lblRegistrationTitle.Size = new System.Drawing.Size(610, 36);
            this.lblRegistrationTitle.TabIndex = 0;
            this.lblRegistrationTitle.Text = "CREATE NEW ACCOUNT";
            this.lblRegistrationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRegistrationFullName
            // 
            this.lblRegistrationFullName.AutoSize = true;
            this.lblRegistrationFullName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegistrationFullName.Location = new System.Drawing.Point(35, 78);
            this.lblRegistrationFullName.Name = "lblRegistrationFullName";
            this.lblRegistrationFullName.Size = new System.Drawing.Size(67, 15);
            this.lblRegistrationFullName.TabIndex = 1;
            this.lblRegistrationFullName.Text = "Full Name :";
            // 
            // txtRegistrationFullName
            // 
            this.txtRegistrationFullName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRegistrationFullName.Location = new System.Drawing.Point(180, 75);
            this.txtRegistrationFullName.Name = "txtRegistrationFullName";
            this.txtRegistrationFullName.Size = new System.Drawing.Size(430, 23);
            this.txtRegistrationFullName.TabIndex = 2;
            this.txtRegistrationFullName.TextChanged += new System.EventHandler(this.txtRegistrationFullName_TextChanged);
            // 
            // lblRegistrationFullNameError
            // 
            this.lblRegistrationFullNameError.AutoSize = true;
            this.lblRegistrationFullNameError.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRegistrationFullNameError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationFullNameError.Location = new System.Drawing.Point(180, 101);
            this.lblRegistrationFullNameError.MaximumSize = new System.Drawing.Size(430, 0);
            this.lblRegistrationFullNameError.Name = "lblRegistrationFullNameError";
            this.lblRegistrationFullNameError.Size = new System.Drawing.Size(0, 13);
            this.lblRegistrationFullNameError.TabIndex = 12;
            // 
            // lblRegistrationUsername
            // 
            this.lblRegistrationUsername.AutoSize = true;
            this.lblRegistrationUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegistrationUsername.Location = new System.Drawing.Point(35, 142);
            this.lblRegistrationUsername.Name = "lblRegistrationUsername";
            this.lblRegistrationUsername.Size = new System.Drawing.Size(66, 15);
            this.lblRegistrationUsername.TabIndex = 3;
            this.lblRegistrationUsername.Text = "Username :";
            // 
            // txtRegistrationUsername
            // 
            this.txtRegistrationUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRegistrationUsername.Location = new System.Drawing.Point(180, 139);
            this.txtRegistrationUsername.Name = "txtRegistrationUsername";
            this.txtRegistrationUsername.Size = new System.Drawing.Size(430, 23);
            this.txtRegistrationUsername.TabIndex = 4;
            this.txtRegistrationUsername.TextChanged += new System.EventHandler(this.txtRegistrationUsername_TextChanged);
            // 
            // lblRegistrationUsernameError
            // 
            this.lblRegistrationUsernameError.AutoSize = true;
            this.lblRegistrationUsernameError.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRegistrationUsernameError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationUsernameError.Location = new System.Drawing.Point(180, 165);
            this.lblRegistrationUsernameError.MaximumSize = new System.Drawing.Size(430, 0);
            this.lblRegistrationUsernameError.Name = "lblRegistrationUsernameError";
            this.lblRegistrationUsernameError.Size = new System.Drawing.Size(0, 13);
            this.lblRegistrationUsernameError.TabIndex = 13;
            // 
            // lblRegistrationPassword
            // 
            this.lblRegistrationPassword.AutoSize = true;
            this.lblRegistrationPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegistrationPassword.Location = new System.Drawing.Point(35, 215);
            this.lblRegistrationPassword.Name = "lblRegistrationPassword";
            this.lblRegistrationPassword.Size = new System.Drawing.Size(63, 15);
            this.lblRegistrationPassword.TabIndex = 5;
            this.lblRegistrationPassword.Text = "Password :";
            // 
            // txtRegistrationPassword
            // 
            this.txtRegistrationPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRegistrationPassword.Location = new System.Drawing.Point(180, 212);
            this.txtRegistrationPassword.Name = "txtRegistrationPassword";
            this.txtRegistrationPassword.Size = new System.Drawing.Size(430, 23);
            this.txtRegistrationPassword.TabIndex = 6;
            this.txtRegistrationPassword.UseSystemPasswordChar = true;
            this.txtRegistrationPassword.TextChanged += new System.EventHandler(this.txtRegistrationPassword_TextChanged);
            // 
            // lblRegistrationPasswordError
            // 
            this.lblRegistrationPasswordError.AutoSize = true;
            this.lblRegistrationPasswordError.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRegistrationPasswordError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationPasswordError.Location = new System.Drawing.Point(180, 238);
            this.lblRegistrationPasswordError.MaximumSize = new System.Drawing.Size(430, 0);
            this.lblRegistrationPasswordError.Name = "lblRegistrationPasswordError";
            this.lblRegistrationPasswordError.Size = new System.Drawing.Size(0, 13);
            this.lblRegistrationPasswordError.TabIndex = 14;
            // 
            // lblRegistrationConfirmPassword
            // 
            this.lblRegistrationConfirmPassword.AutoSize = true;
            this.lblRegistrationConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRegistrationConfirmPassword.Location = new System.Drawing.Point(35, 292);
            this.lblRegistrationConfirmPassword.Name = "lblRegistrationConfirmPassword";
            this.lblRegistrationConfirmPassword.Size = new System.Drawing.Size(110, 15);
            this.lblRegistrationConfirmPassword.TabIndex = 7;
            this.lblRegistrationConfirmPassword.Text = "Confirm Password :";
            // 
            // txtRegistrationConfirmPassword
            // 
            this.txtRegistrationConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRegistrationConfirmPassword.Location = new System.Drawing.Point(180, 289);
            this.txtRegistrationConfirmPassword.Name = "txtRegistrationConfirmPassword";
            this.txtRegistrationConfirmPassword.Size = new System.Drawing.Size(430, 23);
            this.txtRegistrationConfirmPassword.TabIndex = 8;
            this.txtRegistrationConfirmPassword.UseSystemPasswordChar = true;
            this.txtRegistrationConfirmPassword.TextChanged += new System.EventHandler(this.txtRegistrationConfirmPassword_TextChanged);
            // 
            // lblRegistrationConfirmPasswordError
            // 
            this.lblRegistrationConfirmPasswordError.AutoSize = true;
            this.lblRegistrationConfirmPasswordError.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRegistrationConfirmPasswordError.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRegistrationConfirmPasswordError.Location = new System.Drawing.Point(180, 315);
            this.lblRegistrationConfirmPasswordError.MaximumSize = new System.Drawing.Size(430, 0);
            this.lblRegistrationConfirmPasswordError.Name = "lblRegistrationConfirmPasswordError";
            this.lblRegistrationConfirmPasswordError.Size = new System.Drawing.Size(0, 13);
            this.lblRegistrationConfirmPasswordError.TabIndex = 15;
            // 
            // btnRegistrationRegister
            // 
            this.btnRegistrationRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btnRegistrationRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrationRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrationRegister.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRegistrationRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegistrationRegister.Location = new System.Drawing.Point(180, 365);
            this.btnRegistrationRegister.Name = "btnRegistrationRegister";
            this.btnRegistrationRegister.Size = new System.Drawing.Size(125, 38);
            this.btnRegistrationRegister.TabIndex = 9;
            this.btnRegistrationRegister.Text = "Register";
            this.btnRegistrationRegister.UseVisualStyleBackColor = false;
            this.btnRegistrationRegister.Click += new System.EventHandler(this.btnRegistrationRegister_Click);
            // 
            // btnRegistrationClear
            // 
            this.btnRegistrationClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrationClear.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnRegistrationClear.Location = new System.Drawing.Point(325, 365);
            this.btnRegistrationClear.Name = "btnRegistrationClear";
            this.btnRegistrationClear.Size = new System.Drawing.Size(125, 38);
            this.btnRegistrationClear.TabIndex = 10;
            this.btnRegistrationClear.Text = "Clear";
            this.btnRegistrationClear.UseVisualStyleBackColor = true;
            this.btnRegistrationClear.Click += new System.EventHandler(this.btnRegistrationClear_Click);
            // 
            // btnRegistrationLogin
            // 
            this.btnRegistrationLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrationLogin.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnRegistrationLogin.Location = new System.Drawing.Point(470, 365);
            this.btnRegistrationLogin.Name = "btnRegistrationLogin";
            this.btnRegistrationLogin.Size = new System.Drawing.Size(125, 38);
            this.btnRegistrationLogin.TabIndex = 11;
            this.btnRegistrationLogin.Text = "Back to Login";
            this.btnRegistrationLogin.UseVisualStyleBackColor = true;
            this.btnRegistrationLogin.Click += new System.EventHandler(this.btnRegistrationLogin_Click);
            // 
            // FrmRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(760, 520);
            this.Controls.Add(this.pnlRegistrationCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(720, 520);
            this.Name = "FrmRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MoneyFlow - Registration";
            this.Resize += new System.EventHandler(this.FrmRegistration_Resize);
            this.pnlRegistrationCard.ResumeLayout(false);
            this.pnlRegistrationCard.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private void FrmRegistration_Resize(object sender, System.EventArgs e)
        {
            if (this.pnlRegistrationCard != null)
            {
                this.pnlRegistrationCard.Left = System.Math.Max(20, (this.ClientSize.Width - this.pnlRegistrationCard.Width) / 2);
                this.pnlRegistrationCard.Top = System.Math.Max(20, (this.ClientSize.Height - this.pnlRegistrationCard.Height) / 2);
            }
        }

        private System.Windows.Forms.Panel pnlRegistrationCard;
        private System.Windows.Forms.Label lblRegistrationTitle;
        private System.Windows.Forms.Label lblRegistrationFullName;
        private System.Windows.Forms.TextBox txtRegistrationFullName;
        private System.Windows.Forms.Label lblRegistrationFullNameError;
        private System.Windows.Forms.Label lblRegistrationUsername;
        private System.Windows.Forms.TextBox txtRegistrationUsername;
        private System.Windows.Forms.Label lblRegistrationUsernameError;
        private System.Windows.Forms.Label lblRegistrationPassword;
        private System.Windows.Forms.TextBox txtRegistrationPassword;
        private System.Windows.Forms.Label lblRegistrationPasswordError;
        private System.Windows.Forms.Label lblRegistrationConfirmPassword;
        private System.Windows.Forms.TextBox txtRegistrationConfirmPassword;
        private System.Windows.Forms.Label lblRegistrationConfirmPasswordError;
        private System.Windows.Forms.Button btnRegistrationRegister;
        private System.Windows.Forms.Button btnRegistrationClear;
        private System.Windows.Forms.Button btnRegistrationLogin;
    }
}