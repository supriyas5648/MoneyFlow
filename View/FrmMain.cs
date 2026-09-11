using System;
using System.Drawing;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;
using MoneyFlow.View;

namespace MoneyFlow
{
    public partial class FrmMain : Form
    {
        private User? _currentUser;
        private readonly SettingService _settingService;
        private UserSettings? _currentUserSettings;

        public bool LogoutRequested { get; private set; }

        public FrmMain(User? currentUser = null)
        {
            _currentUser = currentUser;
            _settingService = new SettingService();
            InitializeComponent();
            LoadMainWorkspacePage();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadUserSettings();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            LoadMainWorkspacePage();
        }

        private void menuItemMainSettingsChangeFont_Click(object sender, EventArgs e)
        {
            using FontDialog fontDialogMain = new FontDialog
            {
                Font = bottomListView.Font
            };

            if (fontDialogMain.ShowDialog(this) == DialogResult.OK)
            {
                int fontSize = (int)Math.Round(fontDialogMain.Font.Size);
                if (fontSize < 6 || fontSize > 72)
                {
                    MessageBox.Show(
                        "Font size must be between 6 and 72.",
                        "Settings",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (_currentUserSettings == null)
                {
                    bottomListView.Font = new Font(fontDialogMain.Font.FontFamily, fontSize);
                    return;
                }

                UserSettings updatedSettings = CopyCurrentSettings();
                updatedSettings.FontName = fontDialogMain.Font.FontFamily.Name;
                updatedSettings.FontSize = fontSize;

                if (TrySaveUserSettings(updatedSettings))
                {
                    _currentUserSettings = updatedSettings;
                    ApplyUserSettings(updatedSettings);
                }
            }
        }

        private void menuItemMainSettingsChangeColor_Click(object sender, EventArgs e)
        {
            using ColorDialog colorDialogMain = new ColorDialog
            {
                Color = bottomListView.ForeColor
            };

            if (colorDialogMain.ShowDialog(this) == DialogResult.OK)
            {
                if (_currentUserSettings == null)
                {
                    bottomListView.ForeColor = colorDialogMain.Color;
                    return;
                }

                UserSettings updatedSettings = CopyCurrentSettings();
                updatedSettings.TextColor = ColorTranslator.ToHtml(colorDialogMain.Color);

                if (TrySaveUserSettings(updatedSettings))
                {
                    _currentUserSettings = updatedSettings;
                    ApplyUserSettings(updatedSettings);
                }
            }
        }

        private void LoadUserSettings()
        {
            if (_currentUser == null)
            {
                return;
            }

            try
            {
                _currentUserSettings = _settingService.GetUserSettings(_currentUser.UserId)
                    ?? CreateDefaultSettings(_currentUser.UserId);

                if (_currentUserSettings.SettingId == 0)
                {
                    _settingService.SaveUserSettings(_currentUserSettings);
                }

                ApplyUserSettings(_currentUserSettings);
            }
            catch (Exception)
            {
                _currentUserSettings = CreateDefaultSettings(_currentUser.UserId);
                ApplyUserSettings(_currentUserSettings);

                MessageBox.Show(
                    "Unable to load your appearance settings.",
                    "Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private bool TrySaveUserSettings(UserSettings settings)
        {
            try
            {
                if (_settingService.SaveUserSettings(settings))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            MessageBox.Show(
                "Your appearance settings could not be saved.",
                "Settings",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return false;
        }

        private UserSettings CopyCurrentSettings()
        {
            return new UserSettings
            {
                SettingId = _currentUserSettings!.SettingId,
                UserId = _currentUserSettings.UserId,
                FontName = _currentUserSettings.FontName,
                FontSize = _currentUserSettings.FontSize,
                TextColor = _currentUserSettings.TextColor,
                BackgroundColor = _currentUserSettings.BackgroundColor
            };
        }

        private static UserSettings CreateDefaultSettings(int userId)
        {
            return new UserSettings
            {
                UserId = userId,
                FontName = "Arial",
                FontSize = 12,
                TextColor = "#000000",
                BackgroundColor = "#FFFFFF"
            };
        }

        private void ApplyUserSettings(UserSettings settings)
        {
            int fontSize = settings.FontSize is >= 6 and <= 72 ? settings.FontSize : 12;
            string fontName = string.IsNullOrWhiteSpace(settings.FontName) ? "Arial" : settings.FontName;

            try
            {
                bottomListView.Font = new Font(fontName, fontSize);
            }
            catch (ArgumentException)
            {
                bottomListView.Font = new Font(FontFamily.GenericSansSerif, fontSize);
            }

            bottomListView.ForeColor = ParseColor(settings.TextColor, Color.Black);
            bottomListView.BackColor = ParseColor(settings.BackgroundColor, Color.White);
        }

        private static Color ParseColor(string? colorValue, Color fallback)
        {
            if (string.IsNullOrWhiteSpace(colorValue))
            {
                return fallback;
            }

            try
            {
                return ColorTranslator.FromHtml(colorValue);
            }
            catch (ArgumentException)
            {
                return fallback;
            }
        }

        private void menuItemMainSettingsChangePassword_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show(
                    "Change Password is available after a user has logged in.",
                    "Change Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using FrmChangePassword frmChangePassword = new FrmChangePassword(_currentUser.UserId);
            frmChangePassword.ShowDialog(this);
        }

        private void menuItemMainTransaction_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show(
                    "Transaction management is available after a user has logged in.",
                    "Transaction",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using FrmTransaction frmTransaction = new FrmTransaction(_currentUser);
            frmTransaction.ShowDialog(this);
        }

        private void menuItemMainSettingsLogout_Click(object sender, EventArgs e)
        {
            LogoutRequested = true;
            _currentUserSettings = null;
            _currentUser = null;
            Close();
        }

        private void LoadMainWorkspacePage()
        {
            workspaceContainerPanel.Controls.Clear();

            // Populate sample data inside bottom ListView
            bottomListView.Items.Clear();
            bottomListView.Items.Add(new ListViewItem(new[] { "1", "2026-09-01", "Salary", "Monthly Salary", "5000.00", "Income" }));
            bottomListView.Items.Add(new ListViewItem(new[] { "2", "2026-09-03", "Groceries", "Supermarket", "150.00", "Expense" }));
            bottomListView.Items.Add(new ListViewItem(new[] { "3", "2026-09-05", "Utilities", "Electric Bill", "120.00", "Expense" }));

            // Attach master grid to workspace
            workspaceContainerPanel.Controls.Add(masterMainGrid);
        }

        private void FilterMode_CheckedChanged(object sender, EventArgs e)
        {
            pnlCategoryCheckboxes.Visible = chkFilterCategory.Checked;
            pnlDescriptionInput.Visible = chkFilterDescription.Checked;
            UpdateListBoxSummary();
        }

        private void DynamicFilter_Changed(object sender, EventArgs e)
        {
            UpdateListBoxSummary();
        }

        private void UpdateListBoxSummary()
        {
            if (lstSelectedFiltersSummary == null) return;

            lstSelectedFiltersSummary.Items.Clear();

            // 1. Process Category Checkbox Selections
            if (chkFilterCategory.Checked)
            {
                foreach (var chk in categoryCheckBoxesList)
                {
                    if (chk.Checked)
                    {
                        lstSelectedFiltersSummary.Items.Add($"Category Selected: {chk.Text}");
                    }
                }
            }

            // 2. Process Description Search Query
            if (chkFilterDescription.Checked && !string.IsNullOrWhiteSpace(txtDescriptionSearch.Text))
            {
                lstSelectedFiltersSummary.Items.Add($"Description Query: {txtDescriptionSearch.Text.Trim()}");
            }

            // 3. Enable and display ListBox only when active choices exist
            bool hasSelections = lstSelectedFiltersSummary.Items.Count > 0;

            lstSelectedFiltersSummary.Enabled = hasSelections;
            lstSelectedFiltersSummary.Visible = hasSelections;
            lblListBoxTitle.Visible = hasSelections;
        }
    }
}