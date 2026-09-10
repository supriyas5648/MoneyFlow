using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;

namespace MoneyFlow
{
    public partial class FrmMain : Form
    {
        // Default user ID (no login form exists yet — your friend can change this after login is built)
        private readonly int _currentUserId = 1;

        // Service instances for database access
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly TransactionService _transactionService = new TransactionService();
        private readonly SummaryService _summaryService = new SummaryService();

        // Cached data from database
        private List<TransactionModel> _allTransactions = new List<TransactionModel>();

        public FrmMain()
        {
            InitializeComponent();
            LoadMainWorkspacePage();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            LoadMainWorkspacePage();
        }

        /// <summary>
        /// Main entry point — loads all dynamic data from the database into the UI.
        /// </summary>
        private void LoadMainWorkspacePage()
        {
            workspaceContainerPanel.Controls.Clear();

            LoadFinancialSummary();
            LoadCategoriesFromDatabase();
            LoadTransactionsFromDatabase();

            // Attach master grid to workspace
            workspaceContainerPanel.Controls.Add(masterMainGrid);
        }

        /// <summary>
        /// Loads the financial summary (Total Income, Total Expense, Savings) from t_summary.
        /// </summary>
        private void LoadFinancialSummary()
        {
            try
            {
                SummaryModel? summary = _summaryService.GetSummary(_currentUserId);

                if (summary != null)
                {
                    txtTotalIncome.Text = summary.TotalIncome.ToString("N2");
                    txtTotalExpense.Text = summary.TotalExpense.ToString("N2");
                    txtSavings.Text = summary.Savings.ToString("N2");
                }
                else
                {
                    txtTotalIncome.Text = "0.00";
                    txtTotalExpense.Text = "0.00";
                    txtSavings.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading financial summary:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads categories from t_category and dynamically creates filter checkboxes.
        /// </summary>
        private void LoadCategoriesFromDatabase()
        {
            try
            {
                List<CategoryModel> categories = _categoryService.GetAllCategories(_currentUserId);

                // Clear existing category checkboxes
                flowCategoryCheckboxes.Controls.Clear();
                categoryCheckBoxesList.Clear();

                foreach (CategoryModel category in categories)
                {
                    CheckBox chkCat = new CheckBox
                    {
                        Text = category.CategoryName,
                        AutoSize = true,
                        Margin = new Padding(2),
                        Tag = category.CategoryId // Store the ID for filtering
                    };
                    chkCat.CheckedChanged += new EventHandler(this.DynamicFilter_Changed);
                    flowCategoryCheckboxes.Controls.Add(chkCat);
                    categoryCheckBoxesList.Add(chkCat);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads transactions from t_transaction and populates the bottom ListView.
        /// </summary>
        private void LoadTransactionsFromDatabase()
        {
            try
            {
                _allTransactions = _transactionService.GetAllTransactions(_currentUserId);
                PopulateTransactionListView(_allTransactions);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Populates the bottom ListView with the given list of transactions.
        /// </summary>
        private void PopulateTransactionListView(List<TransactionModel> transactions)
        {
            bottomListView.Items.Clear();

            foreach (TransactionModel transaction in transactions)
            {
                var item = new ListViewItem(new[]
                {
                    transaction.TransactionId.ToString(),
                    transaction.TransactionDate.ToString("yyyy-MM-dd"),
                    transaction.CategoryName,
                    transaction.TransactionDescription ?? "",
                    transaction.TransactionAmount.ToString("N2"),
                    transaction.TransactionType
                });

                bottomListView.Items.Add(item);
            }
        }

        /// <summary>
        /// Applies active filters (category and/or description) on the cached transactions.
        /// </summary>
        private void ApplyFilters()
        {
            List<TransactionModel> filteredTransactions = _allTransactions;

            // 1. Filter by selected categories
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

            // 2. Filter by description text
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

        private void FilterMode_CheckedChanged(object sender, EventArgs e)
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

            // 4. Adjust layout to prevent overlap
            AdjustFilterLayout(hasSelections);
        }

        /// <summary>
        /// Dynamically adjusts the rightWorkspaceGrid row heights to prevent overlap
        /// between the filter inputs (row 1) and the summary ListBox (row 3).
        /// </summary>
        private void AdjustFilterLayout(bool isListBoxVisible)
        {
            bool hasAnyFilterOpen = chkFilterCategory.Checked || chkFilterDescription.Checked;

            rightWorkspaceGrid.RowStyles.Clear();
            rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // Row 0: Filter Header

            if (hasAnyFilterOpen && isListBoxVisible)
            {
                // Both filter inputs and ListBox visible — split space
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));  // Row 1: Filter Inputs
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Row 2: Label
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));  // Row 3: ListBox
            }
            else if (hasAnyFilterOpen)
            {
                // Only filter inputs visible — give them all the space
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 1: Filter Inputs
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Row 2: Label
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));  // Row 3: Hidden
            }
            else if (isListBoxVisible)
            {
                // Only ListBox visible (edge case)
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));  // Row 1: Hidden
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Row 2: Label
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 3: ListBox
            }
            else
            {
                // Nothing visible — original defaults
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            }

            rightWorkspaceGrid.PerformLayout();
        }
    }
}