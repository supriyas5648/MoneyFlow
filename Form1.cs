using System;
using System.Windows.Forms;

namespace MoneyFlow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadMainWorkspacePage();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            LoadMainWorkspacePage();
        }

        private void LoadMainWorkspacePage()
        {
            workspaceContainerPanel.Controls.Clear();

            // Populate initial sample items into the ListView
            bottomListView.Items.Clear();
            bottomListView.Items.Add(new ListViewItem(new[] { "1", "2026-09-01", "Salary", "Monthly Salary", "5000.00", "Income" }));
            bottomListView.Items.Add(new ListViewItem(new[] { "2", "2026-09-03", "Groceries", "Supermarket", "150.00", "Expense" }));
            bottomListView.Items.Add(new ListViewItem(new[] { "3", "2026-09-05", "Utilities", "Electric Bill", "120.00", "Expense" }));

            // Attach panels back to workspace container
            workspaceContainerPanel.Controls.Add(middleLayoutTable);
            workspaceContainerPanel.Controls.Add(bottomListView);
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

            // 1. Collect Active Filter Items
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

            // 2. Open/Enable ListBox ONLY when user makes a selection
            bool hasSelections = lstSelectedFiltersSummary.Items.Count > 0;

            lstSelectedFiltersSummary.Enabled = hasSelections;
            lstSelectedFiltersSummary.Visible = hasSelections;
            lblListBoxTitle.Visible = hasSelections;
        }
    }
}