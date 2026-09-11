using System;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;
using MoneyFlow.View;

namespace MoneyFlow
{
    public partial class FrmLogin : Form
    {
        private readonly UserService _userService;

        public FrmLogin()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void LoginClick(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            lblUserNameError.Visible = string.IsNullOrWhiteSpace(username);
            lblPasswordError.Visible = string.IsNullOrWhiteSpace(password);

            if (lblUserNameError.Visible || lblPasswordError.Visible)
            {
                return;
            }

            try
            {
                User? authenticatedUser = _userService.AuthenticateUser(username, password);

                if (authenticatedUser != null)
                {
                    Hide();

                    using (FrmMain mainForm = new FrmMain(authenticatedUser))
                    {
                        mainForm.ShowDialog(this);

                        if (mainForm.LogoutRequested)
                        {
                            ResetForNextUser();
                            Show();
                            return;
                        }
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Invalid username or password.",
                        "Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Unable to connect to the database.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RegisterClick(object sender, EventArgs e)
        {
            Hide();

            using (FrmRegistration registrationForm = new FrmRegistration())
            {
                registrationForm.ShowDialog(this);
            }

            Show();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            lblUserNameError.Visible = string.IsNullOrWhiteSpace(txtUserName.Text);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblPasswordError.Visible = string.IsNullOrWhiteSpace(txtPassword.Text);
        }

        private void ResetForNextUser()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            lblUserNameError.Visible = false;
            lblPasswordError.Visible = false;
            txtUserName.Focus();
        }
    }
}
