using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;

namespace MoneyFlow.View
{
    public partial class FrmRegistration : Form
    {
        private readonly UserService _userService;
        private const string UsernamePattern = @"^(?=.*[0-9])[A-Z][A-Za-z0-9]{0,49}$";
        private const string PasswordPattern = @"^(?=[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9\s])[^\s]{8,}$";

        public FrmRegistration()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnRegistrationRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtRegistrationFullName.Text.Trim();
            string username = txtRegistrationUsername.Text.Trim();
            string password = txtRegistrationPassword.Text;
            string confirmPassword = txtRegistrationConfirmPassword.Text;

            if (!ValidateFullName(true))
            {
                txtRegistrationFullName.Focus();
                return;
            }

            if (!ValidateUsername(true))
            {
                txtRegistrationUsername.Focus();
                return;
            }

            if (!ValidatePassword(true))
            {
                txtRegistrationPassword.Focus();
                return;
            }

            if (!ValidateConfirmPassword(true))
            {
                txtRegistrationConfirmPassword.Focus();
                return;
            }

            try
            {
                if (_userService.UsernameExists(username))
                {
                    lblRegistrationUsernameError.Text = "That username already exists. Please choose another username.";
                    txtRegistrationUsername.Focus();
                    return;
                }

                // TODO: Hash the password before production use. Plaintext passwords must not be stored.
                User user = new User
                {
                    UserFullName = fullName,
                    UserUsername = username,
                    UserPassword = password
                };

                bool isRegistered = _userService.RegisterUser(user);

                if (isRegistered)
                {
                    MessageBox.Show("Registration successful.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearRegistrationFields();
                }
                else
                {
                    MessageBox.Show(
                        "Registration could not be completed.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Registration could not be completed because the database is unavailable.","Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateFullName(bool showRequiredError = false)
        {
            string fullName = txtRegistrationFullName.Text.Trim();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                lblRegistrationFullNameError.Text = showRequiredError ? "Full name is required." : string.Empty;

                return !showRequiredError;
            }

            if (fullName.Length > 100)
            {
                lblRegistrationFullNameError.Text =
                    "Full name cannot exceed 100 characters.";
                return false;
            }

            lblRegistrationFullNameError.Text = string.Empty;
            return true;
        }

        private bool ValidateUsername(bool showRequiredError = false)
        {
            string username = txtRegistrationUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                lblRegistrationUsernameError.Text = showRequiredError ? "Username is required." : string.Empty;

                return !showRequiredError;
            }

            if (!Regex.IsMatch(username, UsernamePattern, RegexOptions.CultureInvariant))
            {
                lblRegistrationUsernameError.Text =
                    "Username must start with an uppercase letter, contain at least one number, and use only English letters and numbers.";
                return false;
            }

            lblRegistrationUsernameError.Text = string.Empty;
            return true;
        }

        private bool ValidatePassword(bool showRequiredError = false)
        {
            string password = txtRegistrationPassword.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                lblRegistrationPasswordError.Text = showRequiredError? "Password is required." : string.Empty;
                return !showRequiredError;
            }

            if (!Regex.IsMatch(password, PasswordPattern, RegexOptions.CultureInvariant))
            {
                lblRegistrationPasswordError.Text =
                    "Password must start with an uppercase letter, be at least 8 characters, contain a number and special character, and contain no spaces.";
                return false;
            }

            lblRegistrationPasswordError.Text = string.Empty;
            return true;
        }

        private bool ValidateConfirmPassword(bool showRequiredError = false)
        {
            string password = txtRegistrationPassword.Text;
            string confirmPassword = txtRegistrationConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                lblRegistrationConfirmPasswordError.Text = showRequiredError ? "Please confirm your password." : string.Empty;

                return !showRequiredError;
            }

            if (password != confirmPassword)
            {
                lblRegistrationConfirmPasswordError.Text = "Passwords do not match.";
                return false;
            }

            lblRegistrationConfirmPasswordError.Text = string.Empty;
            return true;
        }

        private void btnRegistrationClear_Click(object sender, EventArgs e)
        {
            ClearRegistrationFields();
        }

        private void ClearRegistrationFields()
        {
            txtRegistrationFullName.Clear();
            txtRegistrationUsername.Clear();
            txtRegistrationPassword.Clear();
            txtRegistrationConfirmPassword.Clear();
            lblRegistrationFullNameError.Text = string.Empty;
            lblRegistrationUsernameError.Text = string.Empty;
            lblRegistrationPasswordError.Text = string.Empty;
            lblRegistrationConfirmPasswordError.Text = string.Empty;
            txtRegistrationFullName.Focus();
        }

        private void txtRegistrationFullName_TextChanged(object sender, EventArgs e)
        {
            ValidateFullName();
        }

        private void txtRegistrationUsername_TextChanged(object sender, EventArgs e)
        {
            ValidateUsername();
        }

        private void txtRegistrationPassword_TextChanged(object sender, EventArgs e)
        {
            ValidatePassword();
            ValidateConfirmPassword();
        }

        private void txtRegistrationConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateConfirmPassword();
        }

        private void btnRegistrationLogin_Click(object sender, EventArgs e)
        {
            // TODO: Implement login functionality.
        }
    }
}