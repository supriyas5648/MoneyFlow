using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MoneyFlow.Service;

namespace MoneyFlow
{
    public partial class FrmChangePassword : Form
    {
        private readonly int _userId;
        private readonly UserService _userService;
        
        private const string PasswordPattern =@"^(?=.*\d)(?=.*[^A-Za-z0-9\s])[^\s]{8,}$";

        public FrmChangePassword(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _userService = new UserService();
        }

        private void btnChangePasswordSubmit_Click(object sender, EventArgs e)
        {
            string currentPassword = txtChangePasswordCurrentPassword.Text;
            string newPassword = txtChangePasswordNewPassword.Text;
            string confirmPassword = txtChangePasswordConfirmPassword.Text;

            if (!ValidateCurrentPassword(true))
            {
                txtChangePasswordCurrentPassword.Focus();
                return;
            }

            if (!ValidateNewPassword(true))
            {
                txtChangePasswordNewPassword.Focus();
                return;
            }

            if (!ValidateConfirmPassword(true))
            {
                txtChangePasswordConfirmPassword.Focus();
                return;
            }

            if (newPassword == currentPassword)
            {
                lblChangePasswordNewPasswordError.Text = "The new password must be different from the current password.";
                txtChangePasswordNewPassword.Focus();
                return;
            }

            try
            {
                if (!_userService.ChangePassword(_userId, currentPassword, newPassword))
                {
                    MessageBox.Show("The current password is incorrect.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtChangePasswordCurrentPassword.Focus();
                    return;
                }

                MessageBox.Show("Password changed successfully.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearChangePasswordFields();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "The password could not be changed because the database is unavailable.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool ValidateCurrentPassword(bool showRequiredError = false)
        {
            if (string.IsNullOrWhiteSpace(txtChangePasswordCurrentPassword.Text))
            {
                lblChangePasswordCurrentPasswordError.Text = showRequiredError ? "Current password is required." : string.Empty;
                return !showRequiredError;
            }

            lblChangePasswordCurrentPasswordError.Text = string.Empty;
            return true;
        }

        private bool ValidateNewPassword(bool showRequiredError = false)
        {
            string newPassword = txtChangePasswordNewPassword.Text;
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                lblChangePasswordNewPasswordError.Text = showRequiredError ? "New password is required." : string.Empty;
                return !showRequiredError;
            }

            if (!Regex.IsMatch(newPassword, PasswordPattern, RegexOptions.CultureInvariant))
            {
                lblChangePasswordNewPasswordError.Text =
                    "Password must start with an letter,\n be at least 8 characters,\n contain a number and special character,\n and contain no spaces.";
                return false;
            }

            lblChangePasswordNewPasswordError.Text = string.Empty;
            return true;
        }

        private bool ValidateConfirmPassword(bool showRequiredError = false)
        {
            string confirmPassword = txtChangePasswordConfirmPassword.Text;
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                lblChangePasswordConfirmPasswordError.Text = showRequiredError ? "Please confirm the new password." : string.Empty;
                return !showRequiredError;
            }

            if (txtChangePasswordNewPassword.Text != confirmPassword)
            {
                lblChangePasswordConfirmPasswordError.Text = "New passwords do not match.";
                return false;
            }

            lblChangePasswordConfirmPasswordError.Text = string.Empty;
            return true;
        }

        private void ClearChangePasswordFields()
        {
            txtChangePasswordCurrentPassword.Clear();
            txtChangePasswordNewPassword.Clear();
            txtChangePasswordConfirmPassword.Clear();
            lblChangePasswordCurrentPasswordError.Text = string.Empty;
            lblChangePasswordNewPasswordError.Text = string.Empty;
            lblChangePasswordConfirmPasswordError.Text = string.Empty;
            txtChangePasswordCurrentPassword.Focus();
        }

        private void txtChangePasswordCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateCurrentPassword();
        }

        private void txtChangePasswordNewPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateNewPassword();
            ValidateConfirmPassword();
        }

        private void txtChangePasswordConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateConfirmPassword();
        }

        private void chkShowChangePasswordCurrent_CheckedChanged(object sender, EventArgs e)
        {
            txtChangePasswordCurrentPassword.UseSystemPasswordChar = !chkShowChangePasswordCurrent.Checked;
        }

        private void chkShowChangePasswordNew_CheckedChanged(object sender, EventArgs e)
        {
            txtChangePasswordNewPassword.UseSystemPasswordChar = !chkShowChangePasswordNew.Checked;
        }

        private void chkShowChangePasswordConfirm_CheckedChanged(object sender, EventArgs e)
        {
            txtChangePasswordConfirmPassword.UseSystemPasswordChar = !chkShowChangePasswordConfirm.Checked;
        }

        private void btnChangePasswordCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmChangePassword_Load(object sender, System.EventArgs e)
        {

        }
    }
}