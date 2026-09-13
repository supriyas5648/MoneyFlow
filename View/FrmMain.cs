using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;
using MoneyFlow.View;

namespace MoneyFlow
{
    public partial class FrmMain : Form
    {
        private User _currentUser;
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly TransactionService _transactionService = new TransactionService();
        private readonly TransactionCategoryService _transactionCategoryService = new TransactionCategoryService();
        private readonly SummaryService _summaryService = new SummaryService();
        private readonly SettingService _settingService = new SettingService();

        private UserSettings? _currentUserSettings;
        private List<TransactionModel> _allTransactions = new List<TransactionModel>();

        public bool LogoutRequested { get; private set; }

        // Parameterless constructor for VS Code / Visual Studio WinForms Designer
        public FrmMain() : this(new User { UserId = 1, UserFullName = "Administrator", UserUsername = "admin" })
        {
        }

        public FrmMain(User currentUser)
        {
            _currentUser = currentUser ?? new User { UserId = 1, UserFullName = "Administrator", UserUsername = "admin" };
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Safe guard so designer never crashes when opening form in VS Code
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            LoadUserSettings();
            LoadMainWorkspacePage();
        }

        // =========================================================
        // WORKSPACE PAGE SWITCHING & DATA LOADING
        // =========================================================

        private void LoadMainWorkspacePage()
        {
            workspaceContainerPanel.Controls.Clear();
            workspaceContainerPanel.Controls.Add(masterMainGrid);

            LoadFinancialSummary();
            LoadCategoriesFromDatabase();
            LoadTransactionsFromDatabase();
        }

        private void LoadFinancialSummary()
        {
            try
            {
                SummaryModel? summary = _summaryService.GetSummary(_currentUser.UserId);

                if (summary != null)
                {
                    txtTotalIncome.Text = summary.TotalIncome.ToString("N2");
                    txtTotalExpense.Text = summary.TotalExpense.ToString("N2");
                    txtSavings.Text = summary.Savings.ToString("N2");
                }
                else
                {
                    decimal inc = _allTransactions.Where(t => t.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
                    decimal exp = _allTransactions.Where(t => t.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
                    txtTotalIncome.Text = inc.ToString("N2");
                    txtTotalExpense.Text = exp.ToString("N2");
                    txtSavings.Text = (inc - exp).ToString("N2");
                }
            }
            catch
            {
                txtTotalIncome.Text = "0.00";
                txtTotalExpense.Text = "0.00";
                txtSavings.Text = "0.00";
            }
        }

        private void LoadCategoriesFromDatabase()
        {
            try
            {
                List<CategoryModel> categories = _categoryService.GetAllCategories(_currentUser.UserId);

                flowCategoryCheckboxes.Controls.Clear();
                categoryCheckBoxesList.Clear();

                foreach (CategoryModel category in categories)
                {
                    CheckBox chkCat = new CheckBox
                    {
                        Text = category.CategoryName,
                        AutoSize = true,
                        Margin = new Padding(2),
                        Tag = category.CategoryId
                    };
                    chkCat.CheckedChanged += new EventHandler(this.DynamicFilter_Changed);
                    flowCategoryCheckboxes.Controls.Add(chkCat);
                    categoryCheckBoxesList.Add(chkCat);
                }
            }
            catch
            {
            }
        }

        private void LoadTransactionsFromDatabase()
        {
            try
            {
                _allTransactions = _transactionService.GetAllTransactions(_currentUser.UserId);
                PopulateTransactionListView(_allTransactions);
            }
            catch
            {
                _allTransactions = new List<TransactionModel>();
                PopulateTransactionListView(_allTransactions);
            }
        }

        private void PopulateTransactionListView(List<TransactionModel> transactions)
        {
            bottomListView.Items.Clear();

            foreach (TransactionModel transaction in transactions)
            {
                var item = new ListViewItem(new[]
                {
                    transaction.DisplayId.ToString(),
                    transaction.TransactionDate.ToString("yyyy-MM-dd"),
                    transaction.CategoryName,
                    transaction.TransactionDescription ?? string.Empty,
                    transaction.TransactionAmount.ToString("N2"),
                    transaction.TransactionType
                });
                item.Tag = transaction;
                bottomListView.Items.Add(item);
            }
        }

        // =========================================================
        // FILTERING LOGIC
        // =========================================================

        private void ApplyFilters()
        {
            List<TransactionModel> filteredTransactions = _allTransactions;

            if (chkFilterCategory.Checked)
            {
                var selectedCategories = categoryCheckBoxesList
                    .Where(chk => chk.Checked)
                    .Select(chk => chk.Text)
                    .ToList();

                if (selectedCategories.Count > 0)
                {
                    filteredTransactions = filteredTransactions
                        .Where(t => selectedCategories.Contains(t.CategoryName, StringComparer.OrdinalIgnoreCase))
                        .ToList();
                }
            }

            if (chkFilterDescription.Checked && !string.IsNullOrWhiteSpace(txtDescriptionSearch.Text))
            {
                string searchText = txtDescriptionSearch.Text.Trim();
                filteredTransactions = filteredTransactions
                    .Where(t => t.TransactionDescription != null &&
                                t.TransactionDescription.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            PopulateTransactionListView(filteredTransactions);
        }

        private void FilterMode_CheckedChanged(object? sender, EventArgs e)
        {
            pnlCategoryCheckboxes.Visible = chkFilterCategory.Checked;
            pnlDescriptionInput.Visible = chkFilterDescription.Checked;
            UpdateListBoxSummary();
            ApplyFilters();
        }

        private void DynamicFilter_Changed(object? sender, EventArgs e)
        {
            UpdateListBoxSummary();
            ApplyFilters();
        }

        private void UpdateListBoxSummary()
        {
            if (lstSelectedFiltersSummary == null) return;

            lstSelectedFiltersSummary.Items.Clear();

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

            if (chkFilterDescription.Checked && !string.IsNullOrWhiteSpace(txtDescriptionSearch.Text))
            {
                lstSelectedFiltersSummary.Items.Add($"Description Query: {txtDescriptionSearch.Text.Trim()}");
            }

            bool hasSelections = lstSelectedFiltersSummary.Items.Count > 0;
            lstSelectedFiltersSummary.Enabled = hasSelections;
            lstSelectedFiltersSummary.Visible = hasSelections;
            lblListBoxTitle.Visible = hasSelections;

            AdjustFilterLayout(hasSelections);
        }

        private void AdjustFilterLayout(bool isListBoxVisible)
        {
            bool hasAnyFilterOpen = chkFilterCategory.Checked || chkFilterDescription.Checked;

            rightWorkspaceGrid.RowStyles.Clear();
            rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            if (hasAnyFilterOpen && isListBoxVisible)
            {
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            }
            else if (hasAnyFilterOpen)
            {
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            }
            else if (isListBoxVisible)
            {
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            }
            else
            {
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            }

            rightWorkspaceGrid.PerformLayout();
        }

        // =========================================================
        // MENU: FILE (IMPORT, EXPORT, EXIT)
        // =========================================================

        private void menuItemMainFileImportRecords_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Import Transactions from CSV",
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);
                    if (lines.Length == 0)
                    {
                        MessageBox.Show("Selected CSV file is empty.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int importedCount = 0;
                    int startIndex = 0;

                    // Skip header line if detected
                    if (lines[0].Contains("Date", StringComparison.OrdinalIgnoreCase) ||
                        lines[0].Contains("Amount", StringComparison.OrdinalIgnoreCase))
                    {
                        startIndex = 1;
                    }

                    for (int i = startIndex; i < lines.Length; i++)
                    {
                        string line = lines[i].Trim();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        string[] parts = line.Split(',');
                        if (parts.Length < 4) continue;

                        // Expected formats:
                        // 1. Date, Category, Description, Amount, Type
                        // 2. ID, Date, Category, Description, Amount, Type
                        int offset = parts.Length >= 6 ? 1 : 0;

                        if (DateTime.TryParse(parts[offset].Trim(), out DateTime txDate) &&
                            decimal.TryParse(parts[offset + 3].Trim(), out decimal txAmount))
                        {
                            string categoryName = parts[offset + 1].Trim();
                            string description = parts[offset + 2].Trim();
                            string txType = (parts.Length > offset + 4 ? parts[offset + 4].Trim() : "Expense");
                            if (!txType.Equals("Income", StringComparison.OrdinalIgnoreCase))
                            {
                                txType = "Expense";
                            }

                            // Ensure category exists or add it
                            int catId = 1;
                            if (!_transactionCategoryService.CategoryExists(_currentUser.UserId, categoryName, txType))
                            {
                                catId = _transactionCategoryService.AddCategory(categoryName, txType, _currentUser.UserId);
                            }
                            else
                            {
                                var catDt = _transactionCategoryService.GetCategories(_currentUser.UserId, txType);
                                foreach (System.Data.DataRow row in catDt.Rows)
                                {
                                    if (row["c_category_name"].ToString()?.Equals(categoryName, StringComparison.OrdinalIgnoreCase) == true)
                                    {
                                        catId = Convert.ToInt32(row["c_category_id"]);
                                        break;
                                    }
                                }
                            }

                            _transactionService.AddTransaction(txType, catId, txAmount, txDate, _currentUser.UserId, description);
                            importedCount++;
                        }
                    }

                    MessageBox.Show($"Successfully imported {importedCount} transactions!", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesFromDatabase();
                    LoadTransactionsFromDatabase();
                    LoadFinancialSummary();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing file: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void menuItemMainFileExportRecords_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Export Transactions to CSV",
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                FileName = $"MoneyFlow_Transactions_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    using StreamWriter writer = new StreamWriter(saveFileDialog.FileName);
                    writer.WriteLine("TransactionId,Date,Category,Description,Amount,Type");

                    foreach (TransactionModel tx in _allTransactions)
                    {
                        string desc = (tx.TransactionDescription ?? string.Empty).Replace("\"", "\"\"");
                        writer.WriteLine($"{tx.DisplayId},{tx.TransactionDate:yyyy-MM-dd},\"{tx.CategoryName}\",\"{desc}\",{tx.TransactionAmount:F2},{tx.TransactionType}");
                    }

                    MessageBox.Show($"Successfully exported {_allTransactions.Count} records to:\n{saveFileDialog.FileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting file: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void menuItemMainFileExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        // =========================================================
        // MENU: TRANSACTION
        // =========================================================

        private void menuItemMainTransaction_Click(object? sender, EventArgs e)
        {
            using FrmTransaction frmTransaction = new FrmTransaction(_currentUser);
            frmTransaction.ShowDialog(this);

            LoadCategoriesFromDatabase();
            LoadTransactionsFromDatabase();
            LoadFinancialSummary();
        }

        // =========================================================
        // MENU: VIEW
        // =========================================================

        private void menuItemMainViewShowAll_Click(object? sender, EventArgs e)
        {
            workspaceContainerPanel.Controls.Clear();
            workspaceContainerPanel.Controls.Add(masterMainGrid);

            chkFilterCategory.Checked = false;
            chkFilterDescription.Checked = false;
            txtDescriptionSearch.Clear();

            PopulateTransactionListView(_allTransactions);
        }

        private void menuItemMainViewShowIncome_Click(object? sender, EventArgs e)
        {
            workspaceContainerPanel.Controls.Clear();
            workspaceContainerPanel.Controls.Add(masterMainGrid);

            var incomeOnly = _allTransactions.Where(t => t.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase)).ToList();
            PopulateTransactionListView(incomeOnly);
        }

        private void menuItemMainViewShowExpense_Click(object? sender, EventArgs e)
        {
            workspaceContainerPanel.Controls.Clear();
            workspaceContainerPanel.Controls.Add(masterMainGrid);

            var expenseOnly = _allTransactions.Where(t => t.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase)).ToList();
            PopulateTransactionListView(expenseOnly);
        }

        private void menuItemMainViewSummary_Click(object? sender, EventArgs e)
        {
            workspaceContainerPanel.Controls.Clear();
            workspaceContainerPanel.Controls.Add(summaryContainerPanel);
            summaryContainerPanel.Visible = true;

            UpdateMonthWiseSummary();
        }

        private void menuItemMainViewGraph_Click(object? sender, EventArgs e)
        {
            workspaceContainerPanel.Controls.Clear();
            workspaceContainerPanel.Controls.Add(graphContainerPanel);
            graphContainerPanel.Visible = true;

            UpdateMonthWiseGraph();
        }

        // =========================================================
        // MONTH-WISE SUMMARY & GRAPH (DOMAINUPDOWN, PROGRESSBAR, PICTUREBOX)
        // =========================================================

        private void SummaryPeriod_Changed(object? sender, EventArgs e)
        {
            UpdateMonthWiseSummary();
        }

        private void UpdateMonthWiseSummary()
        {
            int month = GetMonthIndex(dudSummaryMonth.SelectedItem?.ToString());
            int year = int.TryParse(dudSummaryYear.SelectedItem?.ToString(), out int y) ? y : DateTime.Now.Year;

            var monthTxs = _allTransactions
                .Where(t => t.TransactionDate.Year == year && t.TransactionDate.Month == month)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();

            decimal income = monthTxs.Where(t => t.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
            decimal expense = monthTxs.Where(t => t.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
            decimal balance = income - expense;

            decimal max = Math.Max(Math.Max(income, expense), Math.Abs(balance));

            lblSummaryIncTitle.Text = $"Total Income:\n₹{income:N2}";
            lblSummaryExpTitle.Text = $"Total Expense:\n₹{expense:N2}";
            lblSummaryBalTitle.Text = $"Net Balance:\n₹{balance:N2}";

            pbSummaryIncome.Value = max <= 0 ? 0 : Math.Min(100, (int)Math.Round(income / max * 100));
            pbSummaryExpense.Value = max <= 0 ? 0 : Math.Min(100, (int)Math.Round(expense / max * 100));
            pbSummaryBalance.Value = max <= 0 ? 0 : Math.Min(100, (int)Math.Round(Math.Abs(balance) / max * 100));

            lvSummaryTransactions.Items.Clear();
            foreach (var tx in monthTxs)
            {
                var item = new ListViewItem(new[]
                {
                    tx.DisplayId.ToString(),
                    tx.TransactionDate.ToString("yyyy-MM-dd"),
                    tx.CategoryName,
                    tx.TransactionDescription ?? string.Empty,
                    tx.TransactionAmount.ToString("N2"),
                    tx.TransactionType
                });
                item.Tag = tx;
                lvSummaryTransactions.Items.Add(item);
            }
        }

        private void GraphPeriod_Changed(object? sender, EventArgs e)
        {
            UpdateMonthWiseGraph();
        }

        private void UpdateMonthWiseGraph()
        {
            int month = GetMonthIndex(dudGraphMonth.SelectedItem?.ToString());
            int year = int.TryParse(dudGraphYear.SelectedItem?.ToString(), out int y) ? y : DateTime.Now.Year;

            var monthTxs = _allTransactions
                .Where(t => t.TransactionDate.Year == year && t.TransactionDate.Month == month)
                .ToList();

            decimal income = monthTxs.Where(t => t.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
            decimal expense = monthTxs.Where(t => t.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
            decimal balance = income - expense;

            lblGraphSummaryInfo.Text = $"Month: {dudGraphMonth.SelectedItem} {year} | Income: ₹{income:N2} | Expense: ₹{expense:N2} | Balance: ₹{balance:N2}";
            picGraph.Invalidate();
        }

        private void picGraph_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            int month = GetMonthIndex(dudGraphMonth.SelectedItem?.ToString());
            int year = int.TryParse(dudGraphYear.SelectedItem?.ToString(), out int y) ? y : DateTime.Now.Year;

            var monthTxs = _allTransactions
                .Where(t => t.TransactionDate.Year == year && t.TransactionDate.Month == month)
                .ToList();

            decimal income = monthTxs.Where(t => t.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
            decimal expense = monthTxs.Where(t => t.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase)).Sum(t => t.TransactionAmount);
            decimal balance = income - expense;

            decimal max = Math.Max(Math.Max(income, expense), Math.Abs(balance));

            using Font titleFont = new Font(Font.FontFamily, 12F, FontStyle.Bold);
            using Font barFont = new Font(Font.FontFamily, 9F, FontStyle.Bold);
            using Font labelFont = new Font(Font.FontFamily, 8.5F);
            using Brush textBrush = new SolidBrush(Color.FromArgb(40, 40, 40));
            using Brush incBrush = new SolidBrush(Color.SeaGreen);
            using Brush expBrush = new SolidBrush(Color.Firebrick);
            using Brush balBrush = new SolidBrush(Color.DarkSlateBlue);
            using Pen gridPen = new Pen(Color.FromArgb(230, 230, 230), 1);

            e.Graphics.DrawString($"Financial Comparison — {dudGraphMonth.SelectedItem} {year}", titleFont, textBrush, 20, 15);

            int chartHeight = Math.Max(60, picGraph.ClientSize.Height - 120);
            int baseline = 45 + chartHeight;
            int barWidth = Math.Max(40, (picGraph.ClientSize.Width - 160) / 4);

            // Baseline guide
            e.Graphics.DrawLine(Pens.Gray, 30, baseline, picGraph.ClientSize.Width - 30, baseline);

            int startX = 60;
            DrawBar(e.Graphics, incBrush, "Income", income, max, startX, baseline, barWidth, chartHeight, barFont, labelFont, textBrush);
            DrawBar(e.Graphics, expBrush, "Expense", expense, max, startX + barWidth + 40, baseline, barWidth, chartHeight, barFont, labelFont, textBrush);
            DrawBar(e.Graphics, balBrush, "Balance", Math.Abs(balance), max, startX + (barWidth + 40) * 2, baseline, barWidth, chartHeight, barFont, labelFont, textBrush);
        }

        private void DrawBar(Graphics g, Brush brush, string label, decimal val, decimal max, int x, int baseline, int width, int chartHeight, Font barFont, Font labelFont, Brush textBrush)
        {
            int h = max <= 0 ? 0 : (int)Math.Round(val / max * chartHeight);
            g.FillRectangle(brush, x, baseline - h, width, h);
            g.DrawRectangle(Pens.DimGray, x, baseline - h, width, h);

            g.DrawString(label, barFont, textBrush, x, baseline + 8);
            g.DrawString($"₹{val:N2}", labelFont, textBrush, x, Math.Max(40, baseline - h - 18));
        }

        private int GetMonthIndex(string? monthName)
        {
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            for (int i = 0; i < months.Length; i++)
            {
                if (months[i].Equals(monthName, StringComparison.OrdinalIgnoreCase))
                {
                    return i + 1;
                }
            }
            return DateTime.Now.Month;
        }

        // =========================================================
        // MENU: SETTINGS (SETTINGS DIALOG, FONT, COLOR, PASSWORD, LOGOUT)
        // =========================================================

        private void menuItemMainSettingsOpen_Click(object? sender, EventArgs e)
        {
            using FrmSetting frmSetting = new FrmSetting(_currentUser.UserId, _currentUserSettings);
            if (frmSetting.ShowDialog(this) == DialogResult.OK)
            {
                if (frmSetting.UpdatedSettings != null)
                {
                    _currentUserSettings = frmSetting.UpdatedSettings;
                    ApplyUserSettings(_currentUserSettings);
                }
            }
        }

        private void menuItemMainSettingsChangeFont_Click(object? sender, EventArgs e)
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
                    MessageBox.Show("Font size must be between 6 and 72.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentUserSettings == null)
                {
                    bottomListView.Font = new Font(fontDialogMain.Font.FontFamily, fontSize);
                    lvSummaryTransactions.Font = bottomListView.Font;
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

        private void menuItemMainSettingsChangeColor_Click(object? sender, EventArgs e)
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
                    lvSummaryTransactions.ForeColor = colorDialogMain.Color;
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

        private void menuItemMainSettingsChangePassword_Click(object? sender, EventArgs e)
        {
            using FrmChangePassword frmChangePassword = new FrmChangePassword(_currentUser.UserId);
            frmChangePassword.ShowDialog(this);
        }

        private void menuItemMainSettingsLogout_Click(object? sender, EventArgs e)
        {
            LogoutRequested = true;
            _currentUserSettings = null;
            Close();
        }

        // =========================================================
        // USER SETTINGS PERSISTENCE & APPLICATION
        // =========================================================

        private void LoadUserSettings()
        {
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
            catch
            {
                _currentUserSettings = CreateDefaultSettings(_currentUser.UserId);
                ApplyUserSettings(_currentUserSettings);
            }
        }

        private bool TrySaveUserSettings(UserSettings settings)
        {
            try
            {
                return _settingService.SaveUserSettings(settings);
            }
            catch
            {
                return false;
            }
        }

        private UserSettings CopyCurrentSettings()
        {
            return new UserSettings
            {
                SettingId = _currentUserSettings?.SettingId ?? 0,
                UserId = _currentUser.UserId,
                FontName = _currentUserSettings?.FontName ?? "Arial",
                FontSize = _currentUserSettings?.FontSize ?? 12,
                TextColor = _currentUserSettings?.TextColor ?? "#000000",
                BackgroundColor = _currentUserSettings?.BackgroundColor ?? "#FFFFFF"
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
                lvSummaryTransactions.Font = bottomListView.Font;
            }
            catch
            {
                bottomListView.Font = new Font(FontFamily.GenericSansSerif, fontSize);
                lvSummaryTransactions.Font = bottomListView.Font;
            }

            Color textColor = ParseColor(settings.TextColor, Color.Black);
            Color backColor = ParseColor(settings.BackgroundColor, Color.White);

            bottomListView.ForeColor = textColor;
            bottomListView.BackColor = backColor;
            lvSummaryTransactions.ForeColor = textColor;
            lvSummaryTransactions.BackColor = backColor;
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
            catch
            {
                return fallback;
            }
        }

        private void BtnAddDashboardCategory_Click(object? sender, EventArgs e)
        {
            using Form prompt = new Form
            {
                Width = 360,
                Height = 225,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Add New Category",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblName = new Label { Left = 20, Top = 15, Text = "Category Name:", AutoSize = true };
            TextBox txtName = new TextBox { Left = 20, Top = 35, Width = 300 };

            Label lblType = new Label { Left = 20, Top = 70, Text = "Category Type:", AutoSize = true };
            RadioButton rbIncome = new RadioButton { Left = 20, Top = 90, Text = "Income", AutoSize = true, Checked = true };
            RadioButton rbExpense = new RadioButton { Left = 120, Top = 90, Text = "Expense", AutoSize = true };

            Button btnSave = new Button { Text = "Save", Left = 140, Width = 85, Top = 135, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Cancel", Left = 235, Width = 85, Top = 135, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lblName);
            prompt.Controls.Add(txtName);
            prompt.Controls.Add(lblType);
            prompt.Controls.Add(rbIncome);
            prompt.Controls.Add(rbExpense);
            prompt.Controls.Add(btnSave);
            prompt.Controls.Add(btnCancel);
            prompt.AcceptButton = btnSave;
            prompt.CancelButton = btnCancel;

            if (prompt.ShowDialog(this) == DialogResult.OK)
            {
                string catName = txtName.Text.Trim();
                string catType = rbIncome.Checked ? "Income" : "Expense";

                if (string.IsNullOrWhiteSpace(catName))
                {
                    MessageBox.Show("Please enter category name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (catName.Length > 100)
                {
                    MessageBox.Show("Category name cannot exceed 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!catName.All(c => char.IsLetter(c) || c == ' '))
                {
                    MessageBox.Show("Category name can contain only alphabets and spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (_transactionCategoryService.CategoryExists(_currentUser.UserId, catName, catType))
                    {
                        MessageBox.Show("This category already exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _transactionCategoryService.AddCategory(catName, catType, _currentUser.UserId);
                    MessageBox.Show("Category added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadCategoriesFromDatabase();

                    if (chkFilterCategory.Checked)
                    {
                        var newChk = categoryCheckBoxesList.FirstOrDefault(c => c.Text.Equals(catName, StringComparison.OrdinalIgnoreCase));
                        if (newChk != null)
                        {
                            newChk.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding category: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BottomListView_DoubleClick(object? sender, EventArgs e)
        {
            if (bottomListView.SelectedItems.Count == 0) return;

            var selectedTx = bottomListView.SelectedItems[0].Tag as TransactionModel;
            if (selectedTx == null) return;

            using FrmTransaction frmTransaction = new FrmTransaction(_currentUser, selectedTx.DisplayId);
            frmTransaction.ShowDialog(this);

            LoadCategoriesFromDatabase();
            LoadTransactionsFromDatabase();
            LoadFinancialSummary();
        }
    }
}