using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MoneyFlow
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Form & Core Containers
        private Panel headerPanel;
        private Panel workspaceContainerPanel;
        private TableLayoutPanel middleLayoutTable;

        // Middle-Left Controls
        private GroupBox pnlLeftFields;
        private Label lblInc;
        private Label lblExp;
        private Label lblSav;
        private TextBox txtTotalIncome;
        private TextBox txtTotalExpense;
        private TextBox txtSavings;

        // Middle-Right Controls
        private GroupBox pnlRightWorkspace;
        private Label lblFilterPrompt;
        private CheckBox chkFilterCategory;
        private CheckBox chkFilterDescription;
        private Panel pnlCategoryCheckboxes;
        private Panel pnlDescriptionInput;
        private Label lblDescPrompt;
        private TextBox txtDescriptionSearch;
        private List<CheckBox> categoryCheckBoxesList = new List<CheckBox>();

        // Output Controls
        private Label lblListBoxTitle;
        private ListBox lstSelectedFiltersSummary;
        private ListView bottomListView;

        // Header Buttons
        private Button btnFile;
        private Button btnTrans;
        private Button btnView;
        private Button btnSetting;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerPanel = new Panel();
            this.workspaceContainerPanel = new Panel();
            this.middleLayoutTable = new TableLayoutPanel();

            // 1. Form Setup (Sized for laptop displays)
            this.Text = "MoneyFlow Desktop";
            this.Size = new Size(950, 620);
            this.MinimumSize = new Size(800, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. Header Setup
            this.headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 35,
                BorderStyle = BorderStyle.FixedSingle
            };

            TableLayoutPanel headerGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };
            headerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            headerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            headerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            headerGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            btnFile = new Button { Text = "File", Dock = DockStyle.Fill };
            btnTrans = new Button { Text = "Trans", Dock = DockStyle.Fill };
            btnView = new Button { Text = "View", Dock = DockStyle.Fill };
            btnSetting = new Button { Text = "Setting", Dock = DockStyle.Fill };

            btnFile.Click += new System.EventHandler(this.btnFile_Click);

            headerGrid.Controls.Add(btnFile, 0, 0);
            headerGrid.Controls.Add(btnTrans, 1, 0);
            headerGrid.Controls.Add(btnView, 2, 0);
            headerGrid.Controls.Add(btnSetting, 3, 0);
            this.headerPanel.Controls.Add(headerGrid);

            // 3. Workspace Panel Setup
            this.workspaceContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            // 4. Form Controls Assembly
            this.Controls.Add(this.workspaceContainerPanel);
            this.Controls.Add(this.headerPanel);

            // 5. Build Workspace Components
            InitializeWorkspaceComponents();
        }

        private void InitializeWorkspaceComponents()
        {
            // Bottom ListView (Sized compactly for laptops)
            this.bottomListView = new ListView
            {
                Dock = DockStyle.Bottom,
                Height = 180,
                View = View.Details,
                GridLines = true,
                FullRowSelect = true
            };

            this.bottomListView.Columns.Add("ID", 50);
            this.bottomListView.Columns.Add("Date", 100);
            this.bottomListView.Columns.Add("Category", 130);
            this.bottomListView.Columns.Add("Description", 240);
            this.bottomListView.Columns.Add("Amount ($)", 100);
            this.bottomListView.Columns.Add("Type", 90);

            // Middle Layout Table
            this.middleLayoutTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 0, 0, 8)
            };
            this.middleLayoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            this.middleLayoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // Left KPI GroupBox
            this.pnlLeftFields = new GroupBox
            {
                Text = "Financial Overview",
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            lblInc = new Label { Text = "Total Income:", Location = new Point(12, 25), AutoSize = true };
            txtTotalIncome = new TextBox { Location = new Point(110, 22), Width = 130, ReadOnly = true, Text = "5000.00" };

            lblExp = new Label { Text = "Total Expense:", Location = new Point(12, 60), AutoSize = true };
            txtTotalExpense = new TextBox { Location = new Point(110, 57), Width = 130, ReadOnly = true, Text = "270.00" };

            lblSav = new Label { Text = "Saving:", Location = new Point(12, 95), AutoSize = true };
            txtSavings = new TextBox { Location = new Point(110, 92), Width = 130, ReadOnly = true, Text = "4730.00" };

            pnlLeftFields.Controls.AddRange(new Control[] { lblInc, txtTotalIncome, lblExp, txtTotalExpense, lblSav, txtSavings });

            // Right Workspace GroupBox
            this.pnlRightWorkspace = new GroupBox
            {
                Text = "User Filter & Criteria Summary",
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            lblFilterPrompt = new Label { Text = "User Filter Mode:", Location = new Point(12, 22), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            chkFilterCategory = new CheckBox { Text = "Category", Location = new Point(130, 21), AutoSize = true };
            chkFilterDescription = new CheckBox { Text = "Description", Location = new Point(215, 21), AutoSize = true };

            chkFilterCategory.CheckedChanged += new System.EventHandler(this.FilterMode_CheckedChanged);
            chkFilterDescription.CheckedChanged += new System.EventHandler(this.FilterMode_CheckedChanged);

            // Category Checkboxes Panel
            pnlCategoryCheckboxes = new Panel
            {
                Location = new Point(12, 48),
                Size = new Size(270, 80),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                Visible = false
            };

            string[] categoryNames = { "Salary", "Groceries", "Utilities", "Rent", "Entertainment" };
            int yPos = 4;
            categoryCheckBoxesList.Clear();
            foreach (string cat in categoryNames)
            {
                CheckBox chkCat = new CheckBox
                {
                    Text = cat,
                    Location = new Point(6, yPos),
                    AutoSize = true
                };
                chkCat.CheckedChanged += new System.EventHandler(this.DynamicFilter_Changed);
                pnlCategoryCheckboxes.Controls.Add(chkCat);
                categoryCheckBoxesList.Add(chkCat);
                yPos += 20;
            }

            // Description Search Panel
            pnlDescriptionInput = new Panel
            {
                Location = new Point(290, 48),
                Size = new Size(240, 80),
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            lblDescPrompt = new Label { Text = "Enter Description Text:", Location = new Point(6, 8), AutoSize = true };
            txtDescriptionSearch = new TextBox { Location = new Point(6, 28), Width = 215 };
            txtDescriptionSearch.TextChanged += new System.EventHandler(this.DynamicFilter_Changed);
            pnlDescriptionInput.Controls.AddRange(new Control[] { lblDescPrompt, txtDescriptionSearch });

            // Summary ListBox Title & Box (Disabled by default, compact laptop height)
            lblListBoxTitle = new Label 
            { 
                Text = "Active Selected Filters (ListBox):", 
                Location = new Point(12, 135), 
                AutoSize = true,
                Visible = false 
            };

            lstSelectedFiltersSummary = new ListBox
            {
                Location = new Point(12, 155),
                Size = new Size(520, 55),
                Enabled = false,
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            pnlRightWorkspace.Controls.AddRange(new Control[] {
                lblFilterPrompt, chkFilterCategory, chkFilterDescription,
                pnlCategoryCheckboxes, pnlDescriptionInput,
                lblListBoxTitle, lstSelectedFiltersSummary
            });

            // Grid Assembly
            middleLayoutTable.Controls.Add(pnlLeftFields, 0, 0);
            middleLayoutTable.Controls.Add(pnlRightWorkspace, 1, 0);
        }
    }
}