using System;
using System.Drawing;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;

namespace MoneyFlow.View
{
    public partial class FrmSetting : Form
    {
        private readonly int _userId;
        private readonly UserService _userService;
        private readonly SettingService _settingService;
        private UserSettings? _settings;

        private Font _selectedFont;
        private Color _selectedColor;

        public UserSettings? UpdatedSettings => _settings;

        // Parameterless constructor for WinForms designer
        public FrmSetting() : this(1, null)
        {
        }

        public FrmSetting(int userId, UserSettings? currentSettings = null)
        {
            InitializeComponent();
            _userId = userId;
            _userService = new UserService();
            _settingService = new SettingService();
            _settings = currentSettings;

            _selectedFont = new Font("Arial", 12F);
            _selectedColor = Color.Black;

            LoadCurrentSettingsUI();
        }

        private void LoadCurrentSettingsUI()
        {
            if (_settings != null)
            {
                int fontSize = _settings.FontSize is >= 6 and <= 72 ? _settings.FontSize : 12;
                string fontName = string.IsNullOrWhiteSpace(_settings.FontName) ? "Arial" : _settings.FontName;

                try
                {
                    _selectedFont = new Font(fontName, fontSize);
                }
                catch
                {
                    _selectedFont = new Font(FontFamily.GenericSansSerif, fontSize);
                }

                if (!string.IsNullOrWhiteSpace(_settings.TextColor))
                {
                    try
                    {
                        _selectedColor = ColorTranslator.FromHtml(_settings.TextColor);
                    }
                    catch
                    {
                        _selectedColor = Color.Black;
                    }
                }
            }

            UpdatePreviewUI();
        }

        private void UpdatePreviewUI()
        {
            lblFontInfo.Text = $"Current: {_selectedFont.FontFamily.Name}, {(int)_selectedFont.Size}pt";
            lblColorInfo.Text = $"Current: ColorTranslator.ToHtml(_selectedColor) ({_selectedColor.Name})";
            lblPreviewSample.Font = _selectedFont;
            lblPreviewSample.ForeColor = _selectedColor;
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                MessageBox.Show("Current password is required.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("New password is required.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please confirm the new password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (newPassword == currentPassword)
            {
                MessageBox.Show("The new password must be different from the current password.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return;
            }

            try
            {
                if (!_userService.ChangePassword(_userId, currentPassword, newPassword))
                {
                    MessageBox.Show("The current password is incorrect.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCurrentPassword.Focus();
                    return;
                }

                MessageBox.Show("Password changed successfully!", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChooseFont_Click(object sender, EventArgs e)
        {
            using FontDialog fontDialog = new FontDialog
            {
                Font = _selectedFont
            };

            if (fontDialog.ShowDialog(this) == DialogResult.OK)
            {
                int fontSize = (int)Math.Round(fontDialog.Font.Size);
                if (fontSize < 6 || fontSize > 72)
                {
                    MessageBox.Show("Font size must be between 6 and 72.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _selectedFont = new Font(fontDialog.Font.FontFamily, fontSize);
                UpdatePreviewUI();
            }
        }

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            using ColorDialog colorDialog = new ColorDialog
            {
                Color = _selectedColor
            };

            if (colorDialog.ShowDialog(this) == DialogResult.OK)
            {
                _selectedColor = colorDialog.Color;
                UpdatePreviewUI();
            }
        }

        private void btnSaveAppearance_Click(object sender, EventArgs e)
        {
            try
            {
                UserSettings settings = _settings ?? new UserSettings
                {
                    UserId = _userId,
                    BackgroundColor = "#FFFFFF"
                };

                settings.FontName = _selectedFont.FontFamily.Name;
                settings.FontSize = (int)_selectedFont.Size;
                settings.TextColor = ColorTranslator.ToHtml(_selectedColor);

                if (_settingService.SaveUserSettings(settings))
                {
                    _settings = settings;
                    MessageBox.Show("Appearance settings saved successfully!", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Appearance settings could not be saved.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}