using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MoneyFlow
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        // Main MenuStrip
        private MenuStrip menuStripMain;
        private ToolStripMenuItem menuItemMainFile;
        private ToolStripMenuItem menuItemMainFileImportRecords;
        private ToolStripMenuItem menuItemMainFileExportRecords;
        private ToolStripMenuItem menuItemMainFileExit;
        private ToolStripMenuItem menuItemMainTransaction;
        private ToolStripMenuItem menuItemMainView;
        private ToolStripMenuItem menuItemMainViewShowAll;
        private ToolStripMenuItem menuItemMainViewShowIncome;
        private ToolStripMenuItem menuItemMainViewShowExpense;
        private ToolStripMenuItem menuItemMainViewSummary;
        private ToolStripMenuItem menuItemMainViewGraph;
        private ToolStripMenuItem menuItemMainSettings;
        private ToolStripMenuItem menuItemMainSettingsChangeFont;
        private ToolStripMenuItem menuItemMainSettingsChangeColor;
        private ToolStripMenuItem menuItemMainSettingsChangePassword;
        private ToolStripMenuItem menuItemMainSettingsLogout;

        // Master Grid & Containers
        private Panel workspaceContainerPanel;
        private TableLayoutPanel masterMainGrid;
        private TableLayoutPanel middleLayoutTable;

        // Middle-Left Controls (Financial Overview)
        private GroupBox pnlLeftFields;
        private TableLayoutPanel leftGrid;
        private Label lblInc;
        private Label lblExp;
        private Label lblSav;
        private TextBox txtTotalIncome;
        private TextBox txtTotalExpense;
        private TextBox txtSavings;

        // Middle-Right Controls (Filters)
        private GroupBox pnlRightWorkspace;
        private TableLayoutPanel rightWorkspaceGrid;
        private FlowLayoutPanel filterHeaderPanel;
        private Label lblFilterPrompt;
        private CheckBox chkFilterCategory;
        private CheckBox chkFilterDescription;

        // Input Containers
        private TableLayoutPanel filterInputsGrid;
        private Panel pnlCategoryCheckboxes;
        private FlowLayoutPanel flowCategoryCheckboxes;
        private Panel pnlDescriptionInput;
        private Label lblDescPrompt;
        private TextBox txtDescriptionSearch;
        private List<CheckBox> categoryCheckBoxesList = new List<CheckBox>();

        // Output Controls
        private Label lblListBoxTitle;
        private ListBox lstSelectedFiltersSummary;
        private ListView bottomListView;

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
            this.components = new System.ComponentModel.Container();

            // DPI & Display Autoscaling Configuration
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            this.Text = "MoneyFlow Desktop";
            this.Size = new Size(960, 640);
            this.MinimumSize = new Size(820, 540);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Main MenuStrip
            this.menuStripMain = new MenuStrip
            {
                Dock = DockStyle.Top,
                Name = "menuStripMain"
            };

            // File Menu
            this.menuItemMainFile = new ToolStripMenuItem("File")
            {
                Name = "menuItemMainFile"
            };
            this.menuItemMainFileImportRecords = new ToolStripMenuItem("Import Records")
            {
                Name = "menuItemMainFileImportRecords"
            };
            this.menuItemMainFileExportRecords = new ToolStripMenuItem("Export Records")
            {
                Name = "menuItemMainFileExportRecords"
            };
            this.menuItemMainFileExit = new ToolStripMenuItem("Exit")
            {
                Name = "menuItemMainFileExit"
            };
            this.menuItemMainFile.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.menuItemMainFileImportRecords,
                this.menuItemMainFileExportRecords,
                this.menuItemMainFileExit
            });

            // Transaction Menu
            this.menuItemMainTransaction = new ToolStripMenuItem("Transaction")
            {
                Name = "menuItemMainTransaction"
            };

            // View Menu
            this.menuItemMainView = new ToolStripMenuItem("View")
            {
                Name = "menuItemMainView"
            };
            this.menuItemMainViewShowAll = new ToolStripMenuItem("Show All")
            {
                Name = "menuItemMainViewShowAll"
            };
            this.menuItemMainViewShowIncome = new ToolStripMenuItem("Show Income")
            {
                Name = "menuItemMainViewShowIncome"
            };
            this.menuItemMainViewShowExpense = new ToolStripMenuItem("Show Expense")
            {
                Name = "menuItemMainViewShowExpense"
            };
            this.menuItemMainViewSummary = new ToolStripMenuItem("Summary")
            {
                Name = "menuItemMainViewSummary"
            };
            this.menuItemMainViewGraph = new ToolStripMenuItem("Graph")
            {
                Name = "menuItemMainViewGraph"
            };
            this.menuItemMainView.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.menuItemMainViewShowAll,
                this.menuItemMainViewShowIncome,
                this.menuItemMainViewShowExpense,
                this.menuItemMainViewSummary,
                this.menuItemMainViewGraph
            });

            // Settings Menu
            this.menuItemMainSettings = new ToolStripMenuItem("Settings")
            {
                Name = "menuItemMainSettings"
            };
            this.menuItemMainSettingsChangeFont = new ToolStripMenuItem("Change Font")
            {
                Name = "menuItemMainSettingsChangeFont"
            };
            this.menuItemMainSettingsChangeColor = new ToolStripMenuItem("Change Color")
            {
                Name = "menuItemMainSettingsChangeColor"
            };
            this.menuItemMainSettingsChangePassword = new ToolStripMenuItem("Change Password")
            {
                Name = "menuItemMainSettingsChangePassword"
            };
            this.menuItemMainSettingsLogout = new ToolStripMenuItem("Logout")
            {
                Name = "menuItemMainSettingsLogout"
            };
            this.menuItemMainSettingsChangeFont.Click += new System.EventHandler(this.menuItemMainSettingsChangeFont_Click);
            this.menuItemMainSettingsChangeColor.Click += new System.EventHandler(this.menuItemMainSettingsChangeColor_Click);
            this.menuItemMainSettingsChangePassword.Click += new System.EventHandler(this.menuItemMainSettingsChangePassword_Click);
            this.menuItemMainSettingsLogout.Click += new System.EventHandler(this.menuItemMainSettingsLogout_Click);
            this.menuItemMainSettings.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.menuItemMainSettingsChangeFont,
                this.menuItemMainSettingsChangeColor,
                this.menuItemMainSettingsChangePassword,
                this.menuItemMainSettingsLogout
            });

            this.menuStripMain.Items.AddRange(new ToolStripItem[]
            {
                this.menuItemMainFile,
                this.menuItemMainTransaction,
                this.menuItemMainView,
                this.menuItemMainSettings
            });

            // Main Workspace Panel
            this.workspaceContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            this.Controls.Add(this.workspaceContainerPanel);
            this.Controls.Add(this.menuStripMain);

            InitializeWorkspaceComponents();
        }

        private void InitializeWorkspaceComponents()
        {
            // Master Structure Grid
            this.masterMainGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0)
            };
            this.masterMainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));
            this.masterMainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));

            // Middle Layout Split Screen
            this.middleLayoutTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 6)
            };
            this.middleLayoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            this.middleLayoutTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // --- LEFT KPI SECTION ---
            this.pnlLeftFields = new GroupBox
            {
                Text = "Financial Overview",
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            this.leftGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 110,
                ColumnCount = 2,
                RowCount = 3
            };
            this.leftGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            this.leftGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            this.leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            this.leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

            lblInc = new Label { Text = "Total Income:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtTotalIncome = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, Text = "5000.00" };

            lblExp = new Label { Text = "Total Expense:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtTotalExpense = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, Text = "270.00" };

            lblSav = new Label { Text = "Saving:", Anchor = AnchorStyles.Left, AutoSize = true };
            txtSavings = new TextBox { Dock = DockStyle.Fill, ReadOnly = true, Text = "4730.00" };

            leftGrid.Controls.Add(lblInc, 0, 0); leftGrid.Controls.Add(txtTotalIncome, 1, 0);
            leftGrid.Controls.Add(lblExp, 0, 1); leftGrid.Controls.Add(txtTotalExpense, 1, 1);
            leftGrid.Controls.Add(lblSav, 0, 2); leftGrid.Controls.Add(txtSavings, 1, 2);
            pnlLeftFields.Controls.Add(leftGrid);

            // --- RIGHT WORKSPACE SECTION ---
            this.pnlRightWorkspace = new GroupBox
            {
                Text = "User Filter & Criteria Summary",
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };

            this.rightWorkspaceGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            this.rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // Filter Header
            this.rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));    // Filter Inputs
            this.rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // Summary Label
            this.rightWorkspaceGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));      // Summary ListBox

            // Filter Checkbox Bar
            filterHeaderPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = false,
                Margin = new Padding(0, 0, 0, 6)
            };

            lblFilterPrompt = new Label { Text = "User Filter Mode:", AutoSize = true, Font = new Font(this.Font, FontStyle.Bold), Margin = new Padding(0, 3, 10, 0) };
            chkFilterCategory = new CheckBox { Text = "Category", AutoSize = true, Margin = new Padding(0, 0, 15, 0) };
            chkFilterDescription = new CheckBox { Text = "Description", AutoSize = true };

            chkFilterCategory.CheckedChanged += new System.EventHandler(this.FilterMode_CheckedChanged);
            chkFilterDescription.CheckedChanged += new System.EventHandler(this.FilterMode_CheckedChanged);

            filterHeaderPanel.Controls.AddRange(new Control[] { lblFilterPrompt, chkFilterCategory, chkFilterDescription });

            // Dynamic Inputs Grid
            filterInputsGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 4)
            };
            filterInputsGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            filterInputsGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // Category Checkboxes Box
            pnlCategoryCheckboxes = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            flowCategoryCheckboxes = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(4)
            };

            string[] categoryNames = { "Salary", "Groceries", "Utilities", "Rent", "Entertainment" };
            categoryCheckBoxesList.Clear();
            foreach (string cat in categoryNames)
            {
                CheckBox chkCat = new CheckBox { Text = cat, AutoSize = true, Margin = new Padding(2) };
                chkCat.CheckedChanged += new System.EventHandler(this.DynamicFilter_Changed);
                flowCategoryCheckboxes.Controls.Add(chkCat);
                categoryCheckBoxesList.Add(chkCat);
            }
            pnlCategoryCheckboxes.Controls.Add(flowCategoryCheckboxes);

            // Description Search Box
            pnlDescriptionInput = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Padding = new Padding(6)
            };

            lblDescPrompt = new Label { Text = "Enter Description Text:", Dock = DockStyle.Top, AutoSize = true };
            txtDescriptionSearch = new TextBox { Dock = DockStyle.Top };
            txtDescriptionSearch.TextChanged += new System.EventHandler(this.DynamicFilter_Changed);

            pnlDescriptionInput.Controls.AddRange(new Control[] { txtDescriptionSearch, lblDescPrompt });

            filterInputsGrid.Controls.Add(pnlCategoryCheckboxes, 0, 0);
            filterInputsGrid.Controls.Add(pnlDescriptionInput, 1, 0);

            // Active Summary ListBox
            lblListBoxTitle = new Label { Text = "Active Selected Filters (ListBox):", AutoSize = true, Visible = false, Margin = new Padding(0, 4, 0, 2) };
            lstSelectedFiltersSummary = new ListBox
            {
                Dock = DockStyle.Fill,
                Enabled = false,
                Visible = false
            };

            rightWorkspaceGrid.Controls.Add(filterHeaderPanel, 0, 0);
            rightWorkspaceGrid.Controls.Add(filterInputsGrid, 0, 1);
            rightWorkspaceGrid.Controls.Add(lblListBoxTitle, 0, 2);
            rightWorkspaceGrid.Controls.Add(lstSelectedFiltersSummary, 0, 3);

            pnlRightWorkspace.Controls.Add(rightWorkspaceGrid);

            middleLayoutTable.Controls.Add(pnlLeftFields, 0, 0);
            middleLayoutTable.Controls.Add(pnlRightWorkspace, 1, 0);

            // Bottom Grid View
            this.bottomListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = System.Windows.Forms.View.Details,
                GridLines = true,
                FullRowSelect = true,
                Margin = new Padding(0)
            };

            this.bottomListView.Columns.Add("ID", 50);
            this.bottomListView.Columns.Add("Date", 100);
            this.bottomListView.Columns.Add("Category", 130);
            this.bottomListView.Columns.Add("Description", 280);
            this.bottomListView.Columns.Add("Amount ($)", 110);
            this.bottomListView.Columns.Add("Type", 90);

            // Assemble Master Window Grid
            masterMainGrid.Controls.Add(middleLayoutTable, 0, 0);
            masterMainGrid.Controls.Add(bottomListView, 0, 1);
        }
    }
}