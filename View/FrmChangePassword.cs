using System;
using System.Windows.Forms;
using MoneyFlow.Service;

namespace MoneyFlow
{
    public partial class FrmChangePassword : Form
    {
        private readonly int _userId;
        private readonly UserService _userService;

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

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                MessageBox.Show("Current password is required.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChangePasswordCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("New password is required.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChangePasswordNewPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please confirm the new password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChangePasswordConfirmPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChangePasswordConfirmPassword.Focus();
                return;
            }

            if (newPassword == currentPassword)
            {
                MessageBox.Show("The new password must be different from the current password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnChangePasswordCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}