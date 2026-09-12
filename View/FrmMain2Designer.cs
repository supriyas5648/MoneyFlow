using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MoneyFlow;

public partial class FrmMain2
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

    #region prev code
    //     // Master Grid & Containers
    //     private Panel workspaceContainerPanel;
    //     private TableLayoutPanel masterMainGrid;
    //     private TableLayoutPanel middleLayoutTable;

    //     // Middle-Left Controls (Financial Overview)
    //     private GroupBox pnlLeftFields;
    //     private TableLayoutPanel leftGrid;
    //     private Label lblInc;
    //     private Label lblExp;
    //     private Label lblSav;
    //     private TextBox txtTotalIncome;
    //     private TextBox txtTotalExpense;
    //     private TextBox txtSavings;

    //     // Middle-Right Controls (Filters)
    //     private GroupBox pnlRightWorkspace;
    //     private TableLayoutPanel rightWorkspaceGrid;
    //     private FlowLayoutPanel filterHeaderPanel;
    //     private Label lblFilterPrompt;
    //     private CheckBox chkFilterCategory;
    //     private CheckBox chkFilterDescription;

    //     // Input Containers
    //     private TableLayoutPanel filterInputsGrid;
    //     private Panel pnlCategoryCheckboxes;
    //     private FlowLayoutPanel flowCategoryCheckboxes;
    //     private Panel pnlDescriptionInput;
    //     private Label lblDescPrompt;
    //     private TextBox txtDescriptionSearch;
    //     private List<CheckBox> categoryCheckBoxesList = new List<CheckBox>();

    //     // Output Controls
    //     private Label lblListBoxTitle;
    //     private ListBox lstSelectedFiltersSummary;
    //     private ListView bottomListView;
    #endregion

    private GroupBox grpFinancialOverview;
    private GroupBox grpFilter;

    private Label lblInc;
    private Label lblExp;
    private Label lblSav;

    private TextBox txtTotalIncome;
    private TextBox txtTotalExpense;
    private TextBox txtSavings;

    private Label lblFilterMode;
    private CheckBox chkFilterCategory;
    private CheckBox chkFilterDescription;

    private GroupBox grpCategory;
    private CheckBox chkSalary;
    private CheckBox chkGroceries;
    private CheckBox chkUtilities;
    private CheckBox chkRent;
    private CheckBox chkEntertainment;

    private GroupBox grpDescription;
    private Label lblDescription;
    private TextBox txtDescription;

    private ListView transactionsListView;

    private List<CheckBox> categoryCheckBoxesList = new List<CheckBox>();
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

        // ============================================================
        // FORM
        // ============================================================
        this.AutoScaleMode = AutoScaleMode.Font;
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.Font = new Font("Segoe UI", 10F);
        this.Text = "MoneyFlow Desktop";
        this.WindowState = FormWindowState.Maximized;
        this.MinimumSize = new Size(900, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.FromArgb(245, 247, 250);

        // ============================================================
        // MAIN MENU
        // ============================================================
        this.menuStripMain = new MenuStrip
        {
            Name = "menuStripMain",
            Dock = DockStyle.Top,
            BackColor = Color.FromArgb(30, 41, 59),
            ForeColor = Color.White,
            Padding = new Padding(10, 5, 10, 5),
            Font = new Font("Segoe UI", 10F, FontStyle.Regular)
        };

        this.menuItemMainFile = new ToolStripMenuItem("File");
        this.menuItemMainFileImportRecords = new ToolStripMenuItem("Import Records");
        this.menuItemMainFileExportRecords = new ToolStripMenuItem("Export Records");
        this.menuItemMainFileExit = new ToolStripMenuItem("Exit");

        this.menuItemMainFile.DropDownItems.AddRange(new ToolStripItem[]
        {
                this.menuItemMainFileImportRecords,
                this.menuItemMainFileExportRecords,
                this.menuItemMainFileExit
        });

        this.menuItemMainTransaction = new ToolStripMenuItem("Transaction");
        this.menuItemMainTransaction.Click += new EventHandler(this.menuItemMainTransaction_Click);

        this.menuItemMainView = new ToolStripMenuItem("View");
        this.menuItemMainViewShowAll = new ToolStripMenuItem("Show All");
        this.menuItemMainViewShowIncome = new ToolStripMenuItem("Show Income");
        this.menuItemMainViewShowExpense = new ToolStripMenuItem("Show Expense");
        this.menuItemMainViewSummary = new ToolStripMenuItem("Summary");
        this.menuItemMainViewGraph = new ToolStripMenuItem("Graph");

        this.menuItemMainView.DropDownItems.AddRange(new ToolStripItem[]
        {
                this.menuItemMainViewShowAll,
                this.menuItemMainViewShowIncome,
                this.menuItemMainViewShowExpense,
                this.menuItemMainViewSummary,
                this.menuItemMainViewGraph
        });

        this.menuItemMainSettings = new ToolStripMenuItem("Settings");
        this.menuItemMainSettingsChangeFont = new ToolStripMenuItem("Change Font");
        this.menuItemMainSettingsChangeColor = new ToolStripMenuItem("Change Color");
        this.menuItemMainSettingsChangePassword = new ToolStripMenuItem("Change Password");
        this.menuItemMainSettingsLogout = new ToolStripMenuItem("Logout");

        this.menuItemMainSettingsChangeFont.Click +=
            new EventHandler(this.menuItemMainSettingsChangeFont_Click);
        this.menuItemMainSettingsChangeColor.Click +=
            new EventHandler(this.menuItemMainSettingsChangeColor_Click);
        this.menuItemMainSettingsChangePassword.Click +=
            new EventHandler(this.menuItemMainSettingsChangePassword_Click);
        this.menuItemMainSettingsLogout.Click +=
            new EventHandler(this.menuItemMainSettingsLogout_Click);

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

        // ============================================================
        // RESPONSIVE MAIN LAYOUT
        // Top: financial overview + filters
        // Bottom: transaction table
        // ============================================================
        TableLayoutPanel mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(18),
            ColumnCount = 1,
            RowCount = 2,
            BackColor = Color.FromArgb(245, 247, 250)
        };

        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 43F));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 57F));

        TableLayoutPanel topLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Margin = new Padding(0, 0, 0, 15)
        };

        topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
        topLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));

        // ============================================================
        // FINANCIAL OVERVIEW
        // ============================================================
        this.grpFinancialOverview = new GroupBox
        {
            Name = "grpFinancialOverview",
            Text = "  Financial Overview  ",
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 0, 12, 0),
            Padding = new Padding(18, 28, 18, 18),
            BackColor = Color.White,
            ForeColor = Color.FromArgb(30, 41, 59),
            Font = new Font("Segoe UI", 11F, FontStyle.Bold)
        };

        TableLayoutPanel financialGrid = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 3,
            BackColor = Color.White
        };

        financialGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
        financialGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
        financialGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        financialGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
        financialGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

        this.lblInc = new Label
        {
            Name = "lblInc",
            Text = "Total Income",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = Color.FromArgb(22, 163, 74),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Margin = new Padding(3)
        };

        this.lblExp = new Label
        {
            Name = "lblExp",
            Text = "Total Expense",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = Color.FromArgb(220, 38, 38),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Margin = new Padding(3)
        };

        this.lblSav = new Label
        {
            Name = "lblSav",
            Text = "Saving",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            ForeColor = Color.FromArgb(37, 99, 235),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Margin = new Padding(3)
        };

        this.txtTotalIncome = CreateValueTextBox("5000.00", Color.FromArgb(240, 253, 244));
        this.txtTotalExpense = CreateValueTextBox("270.00", Color.FromArgb(254, 242, 242));
        this.txtSavings = CreateValueTextBox("4730.00", Color.FromArgb(239, 246, 255));

        financialGrid.Controls.Add(this.lblInc, 0, 0);
        financialGrid.Controls.Add(this.txtTotalIncome, 1, 0);
        financialGrid.Controls.Add(this.lblExp, 0, 1);
        financialGrid.Controls.Add(this.txtTotalExpense, 1, 1);
        financialGrid.Controls.Add(this.lblSav, 0, 2);
        financialGrid.Controls.Add(this.txtSavings, 1, 2);

        this.grpFinancialOverview.Controls.Add(financialGrid);
        topLayout.Controls.Add(this.grpFinancialOverview, 0, 0);

        // ============================================================
        // FILTER AREA
        // ============================================================
        this.grpFilter = new GroupBox
        {
            Name = "grpFilter",
            Text = "  User Filter Criteria Summary  ",
            Dock = DockStyle.Fill,
            Padding = new Padding(18, 28, 18, 18),
            BackColor = Color.White,
            ForeColor = Color.FromArgb(30, 41, 59),
            Font = new Font("Segoe UI", 11F, FontStyle.Bold)
        };

        TableLayoutPanel filterLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            BackColor = Color.White
        };

        filterLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
        filterLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        FlowLayoutPanel filterHeader = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            BackColor = Color.FromArgb(248, 250, 252),
            Padding = new Padding(12, 8, 8, 5),
            Margin = new Padding(0, 0, 0, 8)
        };

        this.lblFilterMode = new Label
        {
            Name = "lblFilterMode",
            Text = "Filter Mode:",
            AutoSize = true,
            ForeColor = Color.FromArgb(71, 85, 105),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Margin = new Padding(0, 4, 18, 0)
        };

        this.chkFilterCategory = new CheckBox
        {
            Name = "chkFilterCategory",
            Text = "Category",
            AutoSize = true,
            ForeColor = Color.FromArgb(30, 41, 59),
            Margin = new Padding(0, 1, 22, 0)
        };
        this.chkFilterCategory.CheckedChanged += new EventHandler(this.FilterMode_CheckedChanged);

        this.chkFilterDescription = new CheckBox
        {
            Name = "chkFilterDescription",
            Text = "Description",
            AutoSize = true,
            ForeColor = Color.FromArgb(30, 41, 59),
            Margin = new Padding(0, 1, 0, 0)
        };
        this.chkFilterDescription.CheckedChanged += new EventHandler(this.FilterMode_CheckedChanged);

        filterHeader.Controls.Add(this.lblFilterMode);
        filterHeader.Controls.Add(this.chkFilterCategory);
        filterHeader.Controls.Add(this.chkFilterDescription);

        TableLayoutPanel filterContent = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            BackColor = Color.White
        };

        filterContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        filterContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

        // Category group
        this.grpCategory = new GroupBox
        {
            Name = "grpCategory",
            Text = "  Category  ",
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 0, 8, 0),
            Padding = new Padding(15, 25, 15, 10),
            Visible = false,
            BackColor = Color.FromArgb(248, 250, 252),
            ForeColor = Color.FromArgb(51, 65, 85),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        };

        FlowLayoutPanel categoryFlow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            Padding = new Padding(5),
            BackColor = Color.FromArgb(248, 250, 252)
        };

        this.chkSalary = CreateCategoryCheckBox("Salary");
        this.chkGroceries = CreateCategoryCheckBox("Groceries");
        this.chkUtilities = CreateCategoryCheckBox("Utilities");
        this.chkRent = CreateCategoryCheckBox("Rent");
        this.chkEntertainment = CreateCategoryCheckBox("Entertainment");

        this.categoryCheckBoxesList.Clear();
        this.categoryCheckBoxesList.Add(this.chkSalary);
        this.categoryCheckBoxesList.Add(this.chkGroceries);
        this.categoryCheckBoxesList.Add(this.chkUtilities);
        this.categoryCheckBoxesList.Add(this.chkRent);
        this.categoryCheckBoxesList.Add(this.chkEntertainment);

        foreach (CheckBox checkBox in this.categoryCheckBoxesList)
        {
            checkBox.CheckedChanged += new EventHandler(this.DynamicFilter_Changed);
            categoryFlow.Controls.Add(checkBox);
        }

        this.grpCategory.Controls.Add(categoryFlow);

        // Description group
        this.grpDescription = new GroupBox
        {
            Name = "grpDescription",
            Text = "  Description  ",
            Dock = DockStyle.Fill,
            Margin = new Padding(8, 0, 0, 0),
            Padding = new Padding(15, 25, 15, 15),
            Visible = false,
            BackColor = Color.FromArgb(248, 250, 252),
            ForeColor = Color.FromArgb(51, 65, 85),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        };

        TableLayoutPanel descriptionLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            BackColor = Color.FromArgb(248, 250, 252)
        };
        descriptionLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        descriptionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));

        this.lblDescription = new Label
        {
            Name = "lblDescription",
            Text = "Enter Description Text:",
            Dock = DockStyle.Fill,
            AutoSize = true,
            ForeColor = Color.FromArgb(71, 85, 105),
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            Margin = new Padding(0, 0, 0, 7)
        };

        this.txtDescription = new TextBox
        {
            Name = "txtDescription",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle
        };
        this.txtDescription.TextChanged += new EventHandler(this.DynamicFilter_Changed);

        descriptionLayout.Controls.Add(this.lblDescription, 0, 0);
        descriptionLayout.Controls.Add(this.txtDescription, 0, 1);
        this.grpDescription.Controls.Add(descriptionLayout);

        filterContent.Controls.Add(this.grpCategory, 0, 0);
        filterContent.Controls.Add(this.grpDescription, 1, 0);

        filterLayout.Controls.Add(filterHeader, 0, 0);
        filterLayout.Controls.Add(filterContent, 0, 1);
        this.grpFilter.Controls.Add(filterLayout);
        topLayout.Controls.Add(this.grpFilter, 1, 0);

        // ============================================================
        // Transaction List VIEW
        // ============================================================
        this.transactionsListView = new ListView
        {
            Name = "transactionsListView",
            Dock = DockStyle.Fill,
            Margin = new Padding(0),
            // ReadOnly = true,
            MultiSelect = false,
            FullRowSelect = true,
            GridLines = true,
            HideSelection = false,
            HeaderStyle = ColumnHeaderStyle.Nonclickable,
            View = View.Details,
            BorderStyle = BorderStyle.None,
            // BackgroundColor = Color.White,
            Font = new Font("Segoe UI", 9.5F)
        };

        this.transactionsListView.Columns.Add("ID", 150);
        this.transactionsListView.Columns.Add("Type", 150);
        this.transactionsListView.Columns.Add("Category", 200);
        this.transactionsListView.Columns.Add("Description", 500);
        this.transactionsListView.Columns.Add("Amount ($)", 150);
        this.transactionsListView.Columns.Add("Date", 150);

        // ============================================================
        // BOTTOM TABLE CONTAINER
        // ============================================================
        GroupBox transactionsBox = new GroupBox
        {
            Text = "  Transactions  ",
            Dock = DockStyle.Fill,
            Margin = new Padding(0),
            Padding = new Padding(12, 28, 12, 12),
            BackColor = Color.White,
            ForeColor = Color.FromArgb(30, 41, 59),
            Font = new Font("Segoe UI", 11F, FontStyle.Bold)
        };

        Panel transactionsHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 38,
            BackColor = Color.White
        };

        // transactionsBox.Controls.Add(this.dgv);
        transactionsBox.Controls.Add(this.transactionsListView);
        transactionsBox.Controls.Add(transactionsHeader);

        mainLayout.Controls.Add(topLayout, 0, 0);
        mainLayout.Controls.Add(transactionsBox, 0, 1);

        this.Controls.Add(mainLayout);
        this.Controls.Add(this.menuStripMain);
    }

    // ============================================================
    // UI HELPER METHODS
    // ============================================================
    private TextBox CreateValueTextBox(string value, Color backColor)
    {
        return new TextBox
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            Text = value,
            TextAlign = HorizontalAlignment.Right,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 41, 59),
            BackColor = backColor,
            BorderStyle = BorderStyle.FixedSingle,
            Margin = new Padding(3, 7, 3, 7)
        };
    }

    private CheckBox CreateCategoryCheckBox(string text)
    {
        return new CheckBox
        {
            Text = text,
            AutoSize = true,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular),
            ForeColor = Color.FromArgb(51, 65, 85),
            Margin = new Padding(3, 5, 3, 5),
            Padding = new Padding(3)
        };
    }
}
