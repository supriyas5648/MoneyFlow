#region prevregion
// using System;
// using System.Drawing;
// using System.Windows.Forms;
// using MoneyFlow.Model;
// using Npgsql;
// using MoneyFlow.Service;
// using System.Data;

// namespace MoneyFlow
// {
//     public partial class FrmMain : Form
//     {
//         private User? _currentUser;
//         private readonly SettingService _settingService;
//         private UserSettings? _currentUserSettings;

//         public bool LogoutRequested { get; private set; }

//         public FrmMain(User? currentUser = null)
//         {
//             _currentUser = currentUser;
//             _settingService = new SettingService();
//             InitializeComponent();
//             LoadDataInListView();
//         }

//         protected override void OnLoad(EventArgs e)
//         {
//             base.OnLoad(e);
//             LoadUserSettings();
//         }

//         // private void btnFile_Click(object sender, EventArgs e)
//         // {
//         //     LoadMainWorkspacePage();
//         // }

//         private void menuItemMainSettingsChangeFont_Click(object sender, EventArgs e)
//         {
//             using FontDialog fontDialogMain = new FontDialog
//             {
//                 Font = bottomListView.Font
//             };

//             if (fontDialogMain.ShowDialog(this) == DialogResult.OK)
//             {
//                 int fontSize = (int)Math.Round(fontDialogMain.Font.Size);
//                 if (fontSize < 6 || fontSize > 72)
//                 {
//                     MessageBox.Show(
//                         "Font size must be between 6 and 72.",
//                         "Settings",
//                         MessageBoxButtons.OK,
//                         MessageBoxIcon.Warning);
//                     return;
//                 }

//                 if (_currentUserSettings == null)
//                 {
//                     bottomListView.Font = new Font(fontDialogMain.Font.FontFamily, fontSize);
//                     return;
//                 }

//                 UserSettings updatedSettings = CopyCurrentSettings();
//                 updatedSettings.FontName = fontDialogMain.Font.FontFamily.Name;
//                 updatedSettings.FontSize = fontSize;

//                 if (TrySaveUserSettings(updatedSettings))
//                 {
//                     _currentUserSettings = updatedSettings;
//                     ApplyUserSettings(updatedSettings);
//                 }
//             }
//         }

//         private void menuItemMainSettingsChangeColor_Click(object sender, EventArgs e)
//         {
//             using ColorDialog colorDialogMain = new ColorDialog
//             {
//                 Color = bottomListView.ForeColor
//             };

//             if (colorDialogMain.ShowDialog(this) == DialogResult.OK)
//             {
//                 if (_currentUserSettings == null)
//                 {
//                     bottomListView.ForeColor = colorDialogMain.Color;
//                     return;
//                 }

//                 UserSettings updatedSettings = CopyCurrentSettings();
//                 updatedSettings.TextColor = ColorTranslator.ToHtml(colorDialogMain.Color);

//                 if (TrySaveUserSettings(updatedSettings))
//                 {
//                     _currentUserSettings = updatedSettings;
//                     ApplyUserSettings(updatedSettings);
//                 }
//             }
//         }

//         private void LoadUserSettings()
//         {
//             if (_currentUser == null)
//             {
//                 return;
//             }

//             try
//             {
//                 _currentUserSettings = _settingService.GetUserSettings(_currentUser.UserId)
//                     ?? CreateDefaultSettings(_currentUser.UserId);

//                 if (_currentUserSettings.SettingId == 0)
//                 {
//                     _settingService.SaveUserSettings(_currentUserSettings);
//                 }

//                 ApplyUserSettings(_currentUserSettings);
//             }
//             catch (Exception)
//             {
//                 _currentUserSettings = CreateDefaultSettings(_currentUser.UserId);
//                 ApplyUserSettings(_currentUserSettings);

//                 MessageBox.Show(
//                     "Unable to load your appearance settings.",
//                     "Settings",
//                     MessageBoxButtons.OK,
//                     MessageBoxIcon.Warning);
//             }
//         }

//         private bool TrySaveUserSettings(UserSettings settings)
//         {
//             try
//             {
//                 if (_settingService.SaveUserSettings(settings))
//                 {
//                     return true;
//                 }
//             }
//             catch (Exception)
//             {
//             }

//             MessageBox.Show(
//                 "Your appearance settings could not be saved.",
//                 "Settings",
//                 MessageBoxButtons.OK,
//                 MessageBoxIcon.Error);
//             return false;
//         }

//         private UserSettings CopyCurrentSettings()
//         {
//             return new UserSettings
//             {
//                 SettingId = _currentUserSettings!.SettingId,
//                 UserId = _currentUserSettings.UserId,
//                 FontName = _currentUserSettings.FontName,
//                 FontSize = _currentUserSettings.FontSize,
//                 TextColor = _currentUserSettings.TextColor,
//                 BackgroundColor = _currentUserSettings.BackgroundColor
//             };
//         }

//         private static UserSettings CreateDefaultSettings(int userId)
//         {
//             return new UserSettings
//             {
//                 UserId = userId,
//                 FontName = "Arial",
//                 FontSize = 12,
//                 TextColor = "#000000",
//                 BackgroundColor = "#FFFFFF"
//             };
//         }

//         private void ApplyUserSettings(UserSettings settings)
//         {
//             int fontSize = settings.FontSize is >= 6 and <= 72 ? settings.FontSize : 12;
//             string fontName = string.IsNullOrWhiteSpace(settings.FontName) ? "Arial" : settings.FontName;

//             try
//             {
//                 bottomListView.Font = new Font(fontName, fontSize);
//             }
//             catch (ArgumentException)
//             {
//                 bottomListView.Font = new Font(FontFamily.GenericSansSerif, fontSize);
//             }

//             bottomListView.ForeColor = ParseColor(settings.TextColor, Color.Black);
//             bottomListView.BackColor = ParseColor(settings.BackgroundColor, Color.White);
//         }

//         private static Color ParseColor(string? colorValue, Color fallback)
//         {
//             if (string.IsNullOrWhiteSpace(colorValue))
//             {
//                 return fallback;
//             }

//             try
//             {
//                 return ColorTranslator.FromHtml(colorValue);
//             }
//             catch (ArgumentException)
//             {
//                 return fallback;
//             }
//         }

//         private void menuItemMainSettingsChangePassword_Click(object sender, EventArgs e)
//         {
//             if (_currentUser == null)
//             {
//                 MessageBox.Show(
//                     "Change Password is available after a user has logged in.",
//                     "Change Password",
//                     MessageBoxButtons.OK,
//                     MessageBoxIcon.Information);
//                 return;
//             }

//             using FrmChangePassword frmChangePassword = new FrmChangePassword(_currentUser.UserId);
//             frmChangePassword.ShowDialog(this);
//         }

//         private void menuItemMainTransaction_Click(object sender, EventArgs e)
//         {
//             if (_currentUser == null)
//             {
//                 MessageBox.Show(
//                     "Transaction management is available after a user has logged in.",
//                     "Transaction",
//                     MessageBoxButtons.OK,
//                     MessageBoxIcon.Information);
//                 return;
//             }

//             using FrmTransaction frmTransaction = new FrmTransaction(_currentUser);
//             frmTransaction.ShowDialog(this);
//         }

//         private void menuItemMainSettingsLogout_Click(object sender, EventArgs e)
//         {
//             LogoutRequested = true;
//             _currentUserSettings = null;
//             _currentUser = null;
//             Close();
//         }

//         private void LoadDataInListView()
//         {
//             try
//             {
//                 using NpgsqlConnection conn = new NpgsqlConnection(env.ConnectionString);

//                 conn.Open();

//                 string query = @"
//             SELECT
//                 t.c_transaction_id AS ""Transaction ID"",
//                 t.c_transaction_type AS ""Type"",
//                 c.c_category_name AS ""Category"",
//                 t.c_transaction_amount AS ""Amount"",
//                 t.c_transaction_description AS ""Description"",
//                 t.c_transaction_date AS ""Date""
//             FROM t_transaction t
//             INNER JOIN t_category c
//                 ON t.c_transaction_category_id = c.c_category_id
//             WHERE t.c_user_id = @user_id
//             ORDER BY t.c_transaction_date DESC,
//                      t.c_transaction_id DESC;
//         ";

//                 using NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

//                 cmd.Parameters.AddWithValue("@user_id", _currentUser.UserId);

//                 using NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);

//                 DataTable dt = new DataTable();
//                 adapter.Fill(dt);

//                 // dgvTransactions.DataSource = dt;
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show(
//                     "Error loading transactions: " + ex.Message,
//                     "Error",
//                     MessageBoxButtons.OK,
//                     MessageBoxIcon.Error
//                 );
//             }
//         }

//         private void FilterMode_CheckedChanged(object sender, EventArgs e)
//         {
//             pnlCategoryCheckboxes.Visible = chkFilterCategory.Checked;
//             pnlDescriptionInput.Visible = chkFilterDescription.Checked;
//             UpdateListBoxSummary();
//         }

//         private void DynamicFilter_Changed(object sender, EventArgs e)
//         {
//             UpdateListBoxSummary();
//         }

//         private void UpdateListBoxSummary()
//         {
//             if (lstSelectedFiltersSummary == null) return;

//             lstSelectedFiltersSummary.Items.Clear();

//             // 1. Process Category Checkbox Selections
//             if (chkFilterCategory.Checked)
//             {
//                 foreach (var chk in categoryCheckBoxesList)
//                 {
//                     if (chk.Checked)
//                     {
//                         lstSelectedFiltersSummary.Items.Add($"Category Selected: {chk.Text}");
//                     }
//                 }
//             }

//             // 2. Process Description Search Query
//             if (chkFilterDescription.Checked && !string.IsNullOrWhiteSpace(txtDescriptionSearch.Text))
//             {
//                 lstSelectedFiltersSummary.Items.Add($"Description Query: {txtDescriptionSearch.Text.Trim()}");
//             }

//             // 3. Enable and display ListBox only when active choices exist
//             bool hasSelections = lstSelectedFiltersSummary.Items.Count > 0;

//             lstSelectedFiltersSummary.Enabled = hasSelections;
//             lstSelectedFiltersSummary.Visible = hasSelections;
//             lblListBoxTitle.Visible = hasSelections;
//         }
//     }
// }
#endregion
using System;
using System.Drawing;
using System.Windows.Forms;
using MoneyFlow.Model;
using Npgsql;
using MoneyFlow.Service;
using System.Data;

namespace MoneyFlow
{
    public partial class FrmMain2 : Form
    {
        // CURRENT USER
        // ============================================================

        private User? _currentUser;
        private readonly SettingService _settingService;
        private UserSettings? _currentUserSettings;
        private DataTable? _transactionsTable;
        private readonly FileService _fileService = new FileService();
        public bool LogoutRequested { get; private set; }


        // ============================================================
        // CONSTRUCTOR
        // ============================================================

        public FrmMain2(User? currentUser = null)
        {
            _currentUser = currentUser;
            _settingService = new SettingService();
            InitializeComponent();
            LoadCategoryFilter();
            LoadDataInListView();
        }


        // ============================================================
        // FORM LOAD
        // ============================================================

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadUserSettings();
        }


        // ============================================================
        // SETTINGS - CHANGE FONT
        // ============================================================

        private void menuItemMainSettingsChangeFont_Click(object sender, EventArgs e)
        {
            using FontDialog fontDialogMain = new FontDialog
            {
                Font = transactionsListView.Font
            };

            if (fontDialogMain.ShowDialog(this) == DialogResult.OK)
            {
                int fontSize =
                    (int)Math.Round(fontDialogMain.Font.Size);

                if (fontSize < 6 || fontSize > 72)
                {
                    MessageBox.Show(
                        "Font size must be between 6 and 72.",
                        "Settings",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                // ----------------------------------------------------
                // If no settings exist, apply font directly
                // ----------------------------------------------------

                if (_currentUserSettings == null)
                {
                    transactionsListView.Font = new Font(
                        fontDialogMain.Font.FontFamily,
                        fontSize);

                    return;
                }


                // ----------------------------------------------------
                // Update saved settings
                // ----------------------------------------------------

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

        // SETTINGS - CHANGE COLOR

        private void menuItemMainSettingsChangeColor_Click(object sender, EventArgs e)
        {
            using ColorDialog colorDialogMain = new ColorDialog
            {
                Color = transactionsListView.ForeColor
            };

            if (colorDialogMain.ShowDialog(this) == DialogResult.OK)
            {
                // ----------------------------------------------------
                // If no settings exist, apply color directly
                // ----------------------------------------------------

                if (_currentUserSettings == null)
                {
                    transactionsListView.ForeColor = colorDialogMain.Color;

                    return;
                }


                // ----------------------------------------------------
                // Update saved settings
                // ----------------------------------------------------

                UserSettings updatedSettings = CopyCurrentSettings();

                updatedSettings.TextColor =
                    ColorTranslator.ToHtml(
                        colorDialogMain.Color);


                if (TrySaveUserSettings(updatedSettings))
                {
                    _currentUserSettings = updatedSettings;

                    ApplyUserSettings(updatedSettings);
                }
            }
        }


        // ============================================================
        // LOAD USER SETTINGS
        // ============================================================

        private void LoadUserSettings()
        {
            if (_currentUser == null)
            {
                return;
            }

            try
            {
                _currentUserSettings =
                    _settingService.GetUserSettings(
                        _currentUser.UserId)
                    ?? CreateDefaultSettings(
                        _currentUser.UserId);


                // ----------------------------------------------------
                // Save default settings if they don't exist
                // ----------------------------------------------------

                if (_currentUserSettings.SettingId == 0)
                {
                    _settingService.SaveUserSettings(
                        _currentUserSettings);
                }


                ApplyUserSettings(_currentUserSettings);
            }
            catch (Exception)
            {
                _currentUserSettings =
                    CreateDefaultSettings(
                        _currentUser.UserId);

                ApplyUserSettings(
                    _currentUserSettings);

                MessageBox.Show(
                    "Unable to load your appearance settings.",
                    "Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }


        // ============================================================
        // SAVE USER SETTINGS
        // ============================================================

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


        // ============================================================
        // COPY CURRENT SETTINGS
        // ============================================================

        private UserSettings CopyCurrentSettings()
        {
            return new UserSettings
            {
                SettingId = _currentUserSettings!.SettingId,

                UserId = _currentUserSettings.UserId,

                FontName = _currentUserSettings.FontName,

                FontSize = _currentUserSettings.FontSize,

                TextColor = _currentUserSettings.TextColor,

                BackgroundColor =
                    _currentUserSettings.BackgroundColor
            };
        }


        // ============================================================
        // CREATE DEFAULT SETTINGS
        // ============================================================

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


        // ============================================================
        // APPLY USER SETTINGS
        // ============================================================

        private void ApplyUserSettings(
            UserSettings settings)
        {
            int fontSize =
                settings.FontSize is >= 6 and <= 72
                ? settings.FontSize
                : 12;

            string fontName =
                string.IsNullOrWhiteSpace(settings.FontName)
                ? "Arial"
                : settings.FontName;


            try
            {
                transactionsListView.Font =
                    new Font(fontName, fontSize);
            }
            catch (ArgumentException)
            {
                transactionsListView.Font =
                    new Font(
                        FontFamily.GenericSansSerif,
                        fontSize);
            }


            transactionsListView.ForeColor =
                ParseColor(
                    settings.TextColor,
                    Color.Black);

            transactionsListView.BackColor =
                ParseColor(
                    settings.BackgroundColor,
                    Color.White);
        }


        // ============================================================
        // CONVERT HTML COLOR TO COLOR
        // ============================================================

        private static Color ParseColor(
            string? colorValue,
            Color fallback)
        {
            if (string.IsNullOrWhiteSpace(colorValue))
            {
                return fallback;
            }

            try
            {
                return ColorTranslator.FromHtml(
                    colorValue);
            }
            catch (ArgumentException)
            {
                return fallback;
            }
        }


        // ============================================================
        // CHANGE PASSWORD
        // ============================================================

        private void menuItemMainSettingsChangePassword_Click(
            object sender,
            EventArgs e)
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


            using FrmChangePassword frmChangePassword =
                new FrmChangePassword(
                    _currentUser.UserId);

            frmChangePassword.ShowDialog(this);
        }


        // ============================================================
        // TRANSACTION MENU
        // ============================================================

        private void menuItemMainTransaction_Click(
            object sender,
            EventArgs e)
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


            using FrmTransaction frmTransaction =
                new FrmTransaction(_currentUser);

            frmTransaction.ShowDialog(this);


            // --------------------------------------------------------
            // Reload transactions after returning from transaction
            // form.
            // --------------------------------------------------------

            LoadCategoryFilter();
            LoadDataInListView();
        }


        // ============================================================
        // LOGOUT
        // ============================================================

        private void menuItemMainSettingsLogout_Click(
            object sender,
            EventArgs e)
        {
            LogoutRequested = true;

            _currentUserSettings = null;

            _currentUser = null;

            Close();
        }


        // ============================================================
        // LOAD TRANSACTIONS INTO LISTVIEW
        // ============================================================

        private void LoadDataInListView()
        {
            // --------------------------------------------------------
            // No user = no transaction data
            // --------------------------------------------------------

            if (_currentUser == null)
            {
                return;
            }


            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(env.ConnectionString);

                conn.Open();

                string query = @"
                    SELECT
                        t.c_transaction_id AS ""Transaction ID"",
                        t.c_transaction_type AS ""Type"",
                        c.c_category_name AS ""Category"",
                        t.c_transaction_amount AS ""Amount"",
                        t.c_transaction_description AS ""Description"",
                        t.c_transaction_date AS ""Date"",
                        t.c_user_id AS ""User ID""
                    FROM t_transaction t
                    INNER JOIN t_category c
                        ON t.c_transaction_category_id =
                           c.c_category_id
                    WHERE t.c_user_id = @user_id
                    ORDER BY
                        t.c_transaction_date DESC,
                        t.c_transaction_id DESC;
                ";


                using NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", _currentUser.UserId);


                using NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);


                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Store the table for filtering and render rows in the ListView.
                _transactionsTable = dt;
                UpdateFinancialTotals(dt);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading transactions: "
                    + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error);
            }
        }

         private void UpdateFinancialTotals(DataTable transactions)
        {
            decimal totalIncome = 0m;
            decimal totalExpense = 0m;

            foreach (DataRow transaction in transactions.Rows)
            {
                decimal amount = transaction["Amount"] == DBNull.Value
                    ? 0m
                    : Convert.ToDecimal(transaction["Amount"]);

                string transactionType = transaction["Type"]?.ToString() ?? string.Empty;

                if (transactionType.Equals("Income", StringComparison.OrdinalIgnoreCase))
                {
                    totalIncome += amount;
                }
                else if (transactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase))
                {
                    totalExpense += amount;
                }
            }

            txtTotalIncome.Text = totalIncome.ToString("0.00");
            txtTotalExpense.Text = totalExpense.ToString("0.00");
            txtSavings.Text = (totalIncome - totalExpense).ToString("0.00");
        }


        // ============================================================
        // FILTER MODE CHANGED
        // ============================================================

        private void FilterMode_CheckedChanged(object sender, EventArgs e)
        {
            // Show category section only when Category is checked
            grpCategory.Visible = chkFilterCategory.Checked;

            // Show description section only when Description is checked
            grpDescription.Visible = chkFilterDescription.Checked;
            ApplyFilters();
        }


        // ============================================================
        // CATEGORY / DESCRIPTION VALUE CHANGED
        // ============================================================

        private void DynamicFilter_Changed(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void CategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategoryFilter();
        }

        private void LoadCategoryFilter()
        {
            if (_currentUser == null || categoryFlow == null || cmbCategoryType == null)
            {
                return;
            }

            string categoryType = cmbCategoryType.SelectedItem?.ToString() ?? "Income";
            categoryFlow.SuspendLayout();
            categoryFlow.Controls.Clear();
            categoryCheckBoxesList.Clear();

            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(env.ConnectionString);
                using NpgsqlCommand command = new NpgsqlCommand(@"
                    SELECT c_category_name
                    FROM t_category
                    WHERE c_created_by_user_id = @UserId
                      AND c_category_type = @CategoryType
                    ORDER BY c_category_name;", connection);
                command.Parameters.AddWithValue("@UserId", _currentUser.UserId);
                command.Parameters.AddWithValue("@CategoryType", categoryType);

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CheckBox checkBox = CreateCategoryCheckBox(reader.GetString(0));
                    checkBox.CheckedChanged += DynamicFilter_Changed;
                    categoryCheckBoxesList.Add(checkBox);
                    categoryFlow.Controls.Add(checkBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading categories: " + ex.Message,
                    "Category Filter",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                categoryFlow.ResumeLayout();
                ApplyFilters();
            }
        }

        // ============================================================
        // APPLY FILTERS TO LISTVIEW
        // ============================================================

        private void ApplyFilters()
        {
            // --------------------------------------------------------
            // If no data has been loaded, there is nothing to filter
            // --------------------------------------------------------

            if (_transactionsTable == null)
            {
                return;
            }

            DataTable dt = _transactionsTable;


            // --------------------------------------------------------
            // Build filter conditions
            // --------------------------------------------------------

            List<string> filters = new List<string>();


            // ========================================================
            // CATEGORY FILTER
            // ========================================================

            if (chkFilterCategory.Checked)
            {
                List<string> selectedCategories =
                    new List<string>();


                foreach (CheckBox chk
                    in categoryCheckBoxesList)
                {
                    if (chk.Checked)
                    {
                        selectedCategories.Add(
                            $"[Category] = '{chk.Text.Replace("'", "''")}'");
                    }
                }


                // ----------------------------------------------------
                // If categories are selected, add OR condition
                // ----------------------------------------------------

                if (selectedCategories.Count > 0)
                {
                    filters.Add(
                        "(" +
                        string.Join(
                            " OR ",
                            selectedCategories) +
                        ")");
                }
            }


            // ========================================================
            // DESCRIPTION FILTER
            // ========================================================

            if (chkFilterDescription.Checked && !string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                string description =
                    txtDescription.Text
                        .Trim()
                        .Replace("'", "''");


                filters.Add(
                    $"[Description] LIKE '%{description}%'");
            }


            // ========================================================
            // APPLY FILTER
            // ========================================================

            if (filters.Count > 0)
            {
                dt.DefaultView.RowFilter =
                    string.Join(
                        " AND ",
                        filters);
            }
            else
            {
                dt.DefaultView.RowFilter = "";
            }

            RenderTransactions(dt.DefaultView);
        }

        private void RenderTransactions(DataView transactions)
        {
            transactionsListView.BeginUpdate();
            transactionsListView.Items.Clear();

            foreach (DataRowView transaction in transactions)
            {
                ListViewItem item = new ListViewItem(transaction["Transaction ID"].ToString());
                item.SubItems.Add(transaction["Type"].ToString());
                item.SubItems.Add(transaction["Category"].ToString());
                item.SubItems.Add(transaction["Description"].ToString());

                if (decimal.TryParse(transaction["Amount"].ToString(), out decimal amount))
                {
                    item.SubItems.Add(amount.ToString("0.00"));
                }
                else
                {
                    item.SubItems.Add(transaction["Amount"].ToString());
                }

                if (DateTime.TryParse(transaction["Date"].ToString(), out DateTime date))
                {
                    item.SubItems.Add(date.ToString("yyyy-MM-dd"));
                }
                else
                {
                    item.SubItems.Add(transaction["Date"].ToString());
                }

                transactionsListView.Items.Add(item);
            }

            transactionsListView.EndUpdate();
        }

    private void Graph_Click(object sender, EventArgs e)
        {
            //    string query = "SELECT "
                FrmReport frmReport= new FrmReport(_currentUser.UserId);
                frmReport.Show();
        }

    private void Summary_Click(object sender, EventArgs e)
        {
            // FrmSummary frmSummary = new FrmSummary();
            // frmSummary.Show();
        }

      private void menuItemMainFileImportRecords_Click(
            object sender,
            EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show(
                    "Please log in before importing records.",
                    "Import Records",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Select transaction CSV file",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                DataTable records = _fileService.ReadCsv(dialog.FileName);
                List<string> validationErrors = _fileService.ValidateRecords(records, _currentUser.UserId);

                if (validationErrors.Count > 0)
                {
                    string details = string.Join(
                        Environment.NewLine,
                        validationErrors.Take(10));

                    if (validationErrors.Count > 10)
                    {
                        details += Environment.NewLine + "More validation errors were found.";
                    }

                    MessageBox.Show(
                        details,
                        "Import Validation Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                int savedCount = _fileService.SaveToDatabase(
                    records,
                    _currentUser.UserId);

                LoadDataInListView();

                MessageBox.Show(
                    savedCount == 0
                        ? "The CSV contained no new records."
                        : $"Imported {savedCount} new record(s) successfully.",
                    "Import Records",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The records could not be imported.\n\n" + ex.Message,
                    "Import Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void menuItemMainFileExportRecords_Click(
            object sender,
            EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show(
                    "Please log in before exporting records.",
                    "Export Records",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "Save transaction CSV file",
                DefaultExt = "csv",
                AddExtension = true,
                OverwritePrompt = true,
                FileName = "transactions.csv"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                DataTable records = _transactionsTable?.DefaultView.ToTable()
                    ?? new DataTable();

                _fileService.ExportToCsv(records, dialog.FileName);

                MessageBox.Show(
                    $"Exported {records.Rows.Count} record(s) successfully.",
                    "Export Records",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "The records could not be exported.\n\n" + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}