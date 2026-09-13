using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MoneyFlow
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        // MenuStrip and Items
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainFileImportRecords;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainFileExportRecords;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainTransaction;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainView;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainViewShowAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainViewShowIncome;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainViewShowExpense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorView;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainViewSummary;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainViewGraph;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainSettingsOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSettings1;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainSettingsChangeFont;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainSettingsChangeColor;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainSettingsChangePassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSettings2;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainSettingsLogout;

        // Workspace Container
        private System.Windows.Forms.Panel workspaceContainerPanel;

        // Master Grid & Original Main Screen Controls
        private System.Windows.Forms.TableLayoutPanel masterMainGrid;
        private System.Windows.Forms.TableLayoutPanel middleLayoutTable;

        // Middle-Left Controls (Financial Overview)
        private System.Windows.Forms.GroupBox pnlLeftFields;
        private System.Windows.Forms.TableLayoutPanel leftGrid;
        private System.Windows.Forms.Label lblInc;
        private System.Windows.Forms.Label lblExp;
        private System.Windows.Forms.Label lblSav;
        private System.Windows.Forms.TextBox txtTotalIncome;
        private System.Windows.Forms.TextBox txtTotalExpense;
        private System.Windows.Forms.TextBox txtSavings;

        // Middle-Right Controls (Filters)
        private System.Windows.Forms.GroupBox pnlRightWorkspace;
        private System.Windows.Forms.TableLayoutPanel rightWorkspaceGrid;
        private System.Windows.Forms.FlowLayoutPanel filterHeaderPanel;
        private System.Windows.Forms.Label lblFilterPrompt;
        private System.Windows.Forms.CheckBox chkFilterCategory;
        private System.Windows.Forms.CheckBox chkFilterDescription;

        // Input Containers
        private System.Windows.Forms.TableLayoutPanel filterInputsGrid;
        private System.Windows.Forms.Panel pnlCategoryCheckboxes;
        private System.Windows.Forms.FlowLayoutPanel flowCategoryCheckboxes;
        private System.Windows.Forms.Panel pnlDescriptionInput;
        private System.Windows.Forms.Label lblDescPrompt;
        private System.Windows.Forms.TextBox txtDescriptionSearch;
        private System.Collections.Generic.List<System.Windows.Forms.CheckBox> categoryCheckBoxesList = new System.Collections.Generic.List<System.Windows.Forms.CheckBox>();

        // Output Controls
        private System.Windows.Forms.Label lblListBoxTitle;
        private System.Windows.Forms.ListBox lstSelectedFiltersSummary;
        private System.Windows.Forms.ListView bottomListView;

        // Month-Wise Summary Controls (ProgressBar View)
        private System.Windows.Forms.Panel summaryContainerPanel;
        private System.Windows.Forms.GroupBox grpSummaryFilter;
        private System.Windows.Forms.Label lblSummaryMonth;
        private System.Windows.Forms.DomainUpDown dudSummaryMonth;
        private System.Windows.Forms.Label lblSummaryYear;
        private System.Windows.Forms.DomainUpDown dudSummaryYear;
        private System.Windows.Forms.TableLayoutPanel summaryCardsGrid;
        private System.Windows.Forms.Label lblSummaryIncTitle;
        private System.Windows.Forms.Label lblSummaryExpTitle;
        private System.Windows.Forms.Label lblSummaryBalTitle;
        private System.Windows.Forms.ProgressBar pbSummaryIncome;
        private System.Windows.Forms.ProgressBar pbSummaryExpense;
        private System.Windows.Forms.ProgressBar pbSummaryBalance;
        private System.Windows.Forms.ListView lvSummaryTransactions;

        // Month-Wise Graph Controls (PictureBox View)
        private System.Windows.Forms.Panel graphContainerPanel;
        private System.Windows.Forms.GroupBox grpGraphFilter;
        private System.Windows.Forms.Label lblGraphMonth;
        private System.Windows.Forms.DomainUpDown dudGraphMonth;
        private System.Windows.Forms.Label lblGraphYear;
        private System.Windows.Forms.DomainUpDown dudGraphYear;
        private System.Windows.Forms.Label lblGraphSummaryInfo;
        private System.Windows.Forms.PictureBox picGraph;

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

            // MenuStrip and Items
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemMainFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainFileImportRecords = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainFileExportRecords = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemMainFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainViewShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainViewShowIncome = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainViewShowExpense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorView = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemMainViewSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainViewGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainSettingsOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSettings1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemMainSettingsChangeFont = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainSettingsChangeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemMainSettingsChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSettings2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemMainSettingsLogout = new System.Windows.Forms.ToolStripMenuItem();

            // Workspace and Master Controls
            this.workspaceContainerPanel = new System.Windows.Forms.Panel();
            this.masterMainGrid = new System.Windows.Forms.TableLayoutPanel();
            this.middleLayoutTable = new System.Windows.Forms.TableLayoutPanel();

            this.pnlLeftFields = new System.Windows.Forms.GroupBox();
            this.leftGrid = new System.Windows.Forms.TableLayoutPanel();
            this.lblInc = new System.Windows.Forms.Label();
            this.txtTotalIncome = new System.Windows.Forms.TextBox();
            this.lblExp = new System.Windows.Forms.Label();
            this.txtTotalExpense = new System.Windows.Forms.TextBox();
            this.lblSav = new System.Windows.Forms.Label();
            this.txtSavings = new System.Windows.Forms.TextBox();

            this.pnlRightWorkspace = new System.Windows.Forms.GroupBox();
            this.rightWorkspaceGrid = new System.Windows.Forms.TableLayoutPanel();
            this.filterHeaderPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblFilterPrompt = new System.Windows.Forms.Label();
            this.chkFilterCategory = new System.Windows.Forms.CheckBox();
            this.chkFilterDescription = new System.Windows.Forms.CheckBox();

            this.filterInputsGrid = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCategoryCheckboxes = new System.Windows.Forms.Panel();
            this.flowCategoryCheckboxes = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDescriptionInput = new System.Windows.Forms.Panel();
            this.lblDescPrompt = new System.Windows.Forms.Label();
            this.txtDescriptionSearch = new System.Windows.Forms.TextBox();

            this.lblListBoxTitle = new System.Windows.Forms.Label();
            this.lstSelectedFiltersSummary = new System.Windows.Forms.ListBox();
            this.bottomListView = new System.Windows.Forms.ListView();

            // Summary Panel Controls
            this.summaryContainerPanel = new System.Windows.Forms.Panel();
            this.grpSummaryFilter = new System.Windows.Forms.GroupBox();
            this.lblSummaryMonth = new System.Windows.Forms.Label();
            this.dudSummaryMonth = new System.Windows.Forms.DomainUpDown();
            this.lblSummaryYear = new System.Windows.Forms.Label();
            this.dudSummaryYear = new System.Windows.Forms.DomainUpDown();
            this.summaryCardsGrid = new System.Windows.Forms.TableLayoutPanel();
            this.lblSummaryIncTitle = new System.Windows.Forms.Label();
            this.lblSummaryExpTitle = new System.Windows.Forms.Label();
            this.lblSummaryBalTitle = new System.Windows.Forms.Label();
            this.pbSummaryIncome = new System.Windows.Forms.ProgressBar();
            this.pbSummaryExpense = new System.Windows.Forms.ProgressBar();
            this.pbSummaryBalance = new System.Windows.Forms.ProgressBar();
            this.lvSummaryTransactions = new System.Windows.Forms.ListView();

            // Graph Panel Controls
            this.graphContainerPanel = new System.Windows.Forms.Panel();
            this.grpGraphFilter = new System.Windows.Forms.GroupBox();
            this.lblGraphMonth = new System.Windows.Forms.Label();
            this.dudGraphMonth = new System.Windows.Forms.DomainUpDown();
            this.lblGraphYear = new System.Windows.Forms.Label();
            this.dudGraphYear = new System.Windows.Forms.DomainUpDown();
            this.lblGraphSummaryInfo = new System.Windows.Forms.Label();
            this.picGraph = new System.Windows.Forms.PictureBox();

            // Suspend Layouts
            this.menuStripMain.SuspendLayout();
            this.workspaceContainerPanel.SuspendLayout();
            this.masterMainGrid.SuspendLayout();
            this.middleLayoutTable.SuspendLayout();
            this.pnlLeftFields.SuspendLayout();
            this.leftGrid.SuspendLayout();
            this.pnlRightWorkspace.SuspendLayout();
            this.rightWorkspaceGrid.SuspendLayout();
            this.filterHeaderPanel.SuspendLayout();
            this.filterInputsGrid.SuspendLayout();
            this.pnlCategoryCheckboxes.SuspendLayout();
            this.pnlDescriptionInput.SuspendLayout();
            this.summaryContainerPanel.SuspendLayout();
            this.grpSummaryFilter.SuspendLayout();
            this.summaryCardsGrid.SuspendLayout();
            this.graphContainerPanel.SuspendLayout();
            this.grpGraphFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).BeginInit();
            this.SuspendLayout();

            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuItemMainFile,
                this.menuItemMainTransaction,
                this.menuItemMainView,
                this.menuItemMainSettings
            });
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(960, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStripMain";

            // 
            // menuItemMainFile
            // 
            this.menuItemMainFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuItemMainFileImportRecords,
                this.menuItemMainFileExportRecords,
                this.toolStripSeparatorFile,
                this.menuItemMainFileExit
            });
            this.menuItemMainFile.Name = "menuItemMainFile";
            this.menuItemMainFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemMainFile.Text = "File";

            // 
            // menuItemMainFileImportRecords
            // 
            this.menuItemMainFileImportRecords.Name = "menuItemMainFileImportRecords";
            this.menuItemMainFileImportRecords.Size = new System.Drawing.Size(155, 22);
            this.menuItemMainFileImportRecords.Text = "Import Records";
            this.menuItemMainFileImportRecords.Click += new System.EventHandler(this.menuItemMainFileImportRecords_Click);

            // 
            // menuItemMainFileExportRecords
            // 
            this.menuItemMainFileExportRecords.Name = "menuItemMainFileExportRecords";
            this.menuItemMainFileExportRecords.Size = new System.Drawing.Size(155, 22);
            this.menuItemMainFileExportRecords.Text = "Export Records";
            this.menuItemMainFileExportRecords.Click += new System.EventHandler(this.menuItemMainFileExportRecords_Click);

            // 
            // toolStripSeparatorFile
            // 
            this.toolStripSeparatorFile.Name = "toolStripSeparatorFile";
            this.toolStripSeparatorFile.Size = new System.Drawing.Size(152, 6);

            // 
            // menuItemMainFileExit
            // 
            this.menuItemMainFileExit.Name = "menuItemMainFileExit";
            this.menuItemMainFileExit.Size = new System.Drawing.Size(155, 22);
            this.menuItemMainFileExit.Text = "Exit";
            this.menuItemMainFileExit.Click += new System.EventHandler(this.menuItemMainFileExit_Click);

            // 
            // menuItemMainTransaction
            // 
            this.menuItemMainTransaction.Name = "menuItemMainTransaction";
            this.menuItemMainTransaction.Size = new System.Drawing.Size(80, 20);
            this.menuItemMainTransaction.Text = "Transaction";
            this.menuItemMainTransaction.Click += new System.EventHandler(this.menuItemMainTransaction_Click);

            // 
            // menuItemMainView
            // 
            this.menuItemMainView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuItemMainViewShowAll,
                this.menuItemMainViewShowIncome,
                this.menuItemMainViewShowExpense,
                this.toolStripSeparatorView,
                this.menuItemMainViewSummary,
                this.menuItemMainViewGraph
            });
            this.menuItemMainView.Name = "menuItemMainView";
            this.menuItemMainView.Size = new System.Drawing.Size(44, 20);
            this.menuItemMainView.Text = "View";

            // 
            // menuItemMainViewShowAll
            // 
            this.menuItemMainViewShowAll.Name = "menuItemMainViewShowAll";
            this.menuItemMainViewShowAll.Size = new System.Drawing.Size(262, 22);
            this.menuItemMainViewShowAll.Text = "Show All (ListView)";
            this.menuItemMainViewShowAll.Click += new System.EventHandler(this.menuItemMainViewShowAll_Click);

            // 
            // menuItemMainViewShowIncome
            // 
            this.menuItemMainViewShowIncome.Name = "menuItemMainViewShowIncome";
            this.menuItemMainViewShowIncome.Size = new System.Drawing.Size(262, 22);
            this.menuItemMainViewShowIncome.Text = "Show Income";
            this.menuItemMainViewShowIncome.Click += new System.EventHandler(this.menuItemMainViewShowIncome_Click);

            // 
            // menuItemMainViewShowExpense
            // 
            this.menuItemMainViewShowExpense.Name = "menuItemMainViewShowExpense";
            this.menuItemMainViewShowExpense.Size = new System.Drawing.Size(262, 22);
            this.menuItemMainViewShowExpense.Text = "Show Expense";
            this.menuItemMainViewShowExpense.Click += new System.EventHandler(this.menuItemMainViewShowExpense_Click);

            // 
            // toolStripSeparatorView
            // 
            this.toolStripSeparatorView.Name = "toolStripSeparatorView";
            this.toolStripSeparatorView.Size = new System.Drawing.Size(259, 6);

            // 
            // menuItemMainViewSummary
            // 
            this.menuItemMainViewSummary.Name = "menuItemMainViewSummary";
            this.menuItemMainViewSummary.Size = new System.Drawing.Size(262, 22);
            this.menuItemMainViewSummary.Text = "Summary (Progress bar, Month wise)";
            this.menuItemMainViewSummary.Click += new System.EventHandler(this.menuItemMainViewSummary_Click);

            // 
            // menuItemMainViewGraph
            // 
            this.menuItemMainViewGraph.Name = "menuItemMainViewGraph";
            this.menuItemMainViewGraph.Size = new System.Drawing.Size(262, 22);
            this.menuItemMainViewGraph.Text = "Graph (Picturebox, Month wise)";
            this.menuItemMainViewGraph.Click += new System.EventHandler(this.menuItemMainViewGraph_Click);

            // 
            // menuItemMainSettings
            // 
            this.menuItemMainSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.menuItemMainSettingsOpen,
                this.toolStripSeparatorSettings1,
                this.menuItemMainSettingsChangeFont,
                this.menuItemMainSettingsChangeColor,
                this.menuItemMainSettingsChangePassword,
                this.toolStripSeparatorSettings2,
                this.menuItemMainSettingsLogout
            });
            this.menuItemMainSettings.Name = "menuItemMainSettings";
            this.menuItemMainSettings.Size = new System.Drawing.Size(61, 20);
            this.menuItemMainSettings.Text = "Settings";

            // 
            // menuItemMainSettingsOpen
            // 
            this.menuItemMainSettingsOpen.Name = "menuItemMainSettingsOpen";
            this.menuItemMainSettingsOpen.Size = new System.Drawing.Size(168, 22);
            this.menuItemMainSettingsOpen.Text = "Settings...";
            this.menuItemMainSettingsOpen.Click += new System.EventHandler(this.menuItemMainSettingsOpen_Click);

            // 
            // toolStripSeparatorSettings1
            // 
            this.toolStripSeparatorSettings1.Name = "toolStripSeparatorSettings1";
            this.toolStripSeparatorSettings1.Size = new System.Drawing.Size(165, 6);

            // 
            // menuItemMainSettingsChangeFont
            // 
            this.menuItemMainSettingsChangeFont.Name = "menuItemMainSettingsChangeFont";
            this.menuItemMainSettingsChangeFont.Size = new System.Drawing.Size(168, 22);
            this.menuItemMainSettingsChangeFont.Text = "Change Font";
            this.menuItemMainSettingsChangeFont.Click += new System.EventHandler(this.menuItemMainSettingsChangeFont_Click);

            // 
            // menuItemMainSettingsChangeColor
            // 
            this.menuItemMainSettingsChangeColor.Name = "menuItemMainSettingsChangeColor";
            this.menuItemMainSettingsChangeColor.Size = new System.Drawing.Size(168, 22);
            this.menuItemMainSettingsChangeColor.Text = "Change Color";
            this.menuItemMainSettingsChangeColor.Click += new System.EventHandler(this.menuItemMainSettingsChangeColor_Click);

            // 
            // menuItemMainSettingsChangePassword
            // 
            this.menuItemMainSettingsChangePassword.Name = "menuItemMainSettingsChangePassword";
            this.menuItemMainSettingsChangePassword.Size = new System.Drawing.Size(168, 22);
            this.menuItemMainSettingsChangePassword.Text = "Change Password";
            this.menuItemMainSettingsChangePassword.Click += new System.EventHandler(this.menuItemMainSettingsChangePassword_Click);

            // 
            // toolStripSeparatorSettings2
            // 
            this.toolStripSeparatorSettings2.Name = "toolStripSeparatorSettings2";
            this.toolStripSeparatorSettings2.Size = new System.Drawing.Size(165, 6);

            // 
            // menuItemMainSettingsLogout
            // 
            this.menuItemMainSettingsLogout.Name = "menuItemMainSettingsLogout";
            this.menuItemMainSettingsLogout.Size = new System.Drawing.Size(168, 22);
            this.menuItemMainSettingsLogout.Text = "Logout";
            this.menuItemMainSettingsLogout.Click += new System.EventHandler(this.menuItemMainSettingsLogout_Click);

            // 
            // workspaceContainerPanel
            // 
            this.workspaceContainerPanel.Controls.Add(this.masterMainGrid);
            this.workspaceContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workspaceContainerPanel.Location = new System.Drawing.Point(0, 24);
            this.workspaceContainerPanel.Name = "workspaceContainerPanel";
            this.workspaceContainerPanel.Padding = new System.Windows.Forms.Padding(8);
            this.workspaceContainerPanel.Size = new System.Drawing.Size(960, 616);
            this.workspaceContainerPanel.TabIndex = 1;

            // 
            // masterMainGrid
            // 
            this.masterMainGrid.ColumnCount = 1;
            this.masterMainGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.masterMainGrid.Controls.Add(this.middleLayoutTable, 0, 0);
            this.masterMainGrid.Controls.Add(this.bottomListView, 0, 1);
            this.masterMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterMainGrid.Location = new System.Drawing.Point(8, 8);
            this.masterMainGrid.Margin = new System.Windows.Forms.Padding(0);
            this.masterMainGrid.Name = "masterMainGrid";
            this.masterMainGrid.RowCount = 2;
            this.masterMainGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.masterMainGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.masterMainGrid.Size = new System.Drawing.Size(944, 600);
            this.masterMainGrid.TabIndex = 0;

            // 
            // middleLayoutTable
            // 
            this.middleLayoutTable.ColumnCount = 2;
            this.middleLayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.middleLayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.middleLayoutTable.Controls.Add(this.pnlLeftFields, 0, 0);
            this.middleLayoutTable.Controls.Add(this.pnlRightWorkspace, 1, 0);
            this.middleLayoutTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.middleLayoutTable.Location = new System.Drawing.Point(0, 0);
            this.middleLayoutTable.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.middleLayoutTable.Name = "middleLayoutTable";
            this.middleLayoutTable.RowCount = 1;
            this.middleLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.middleLayoutTable.Size = new System.Drawing.Size(944, 366);
            this.middleLayoutTable.TabIndex = 0;

            // 
            // pnlLeftFields
            // 
            this.pnlLeftFields.Controls.Add(this.leftGrid);
            this.pnlLeftFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftFields.Location = new System.Drawing.Point(3, 3);
            this.pnlLeftFields.Name = "pnlLeftFields";
            this.pnlLeftFields.Padding = new System.Windows.Forms.Padding(8);
            this.pnlLeftFields.Size = new System.Drawing.Size(274, 360);
            this.pnlLeftFields.TabIndex = 0;
            this.pnlLeftFields.TabStop = false;
            this.pnlLeftFields.Text = "Financial Overview";

            // 
            // leftGrid
            // 
            this.leftGrid.ColumnCount = 2;
            this.leftGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.leftGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftGrid.Controls.Add(this.lblInc, 0, 0);
            this.leftGrid.Controls.Add(this.txtTotalIncome, 1, 0);
            this.leftGrid.Controls.Add(this.lblExp, 0, 1);
            this.leftGrid.Controls.Add(this.txtTotalExpense, 1, 1);
            this.leftGrid.Controls.Add(this.lblSav, 0, 2);
            this.leftGrid.Controls.Add(this.txtSavings, 1, 2);
            this.leftGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftGrid.Location = new System.Drawing.Point(8, 24);
            this.leftGrid.Name = "leftGrid";
            this.leftGrid.RowCount = 3;
            this.leftGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.leftGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.leftGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.leftGrid.Size = new System.Drawing.Size(258, 110);
            this.leftGrid.TabIndex = 0;

            // 
            // lblInc
            // 
            this.lblInc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInc.AutoSize = true;
            this.lblInc.Location = new System.Drawing.Point(3, 11);
            this.lblInc.Name = "lblInc";
            this.lblInc.Size = new System.Drawing.Size(79, 15);
            this.lblInc.TabIndex = 0;
            this.lblInc.Text = "Total Income:";

            // 
            // txtTotalIncome
            // 
            this.txtTotalIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalIncome.Location = new System.Drawing.Point(98, 3);
            this.txtTotalIncome.Name = "txtTotalIncome";
            this.txtTotalIncome.ReadOnly = true;
            this.txtTotalIncome.Size = new System.Drawing.Size(157, 23);
            this.txtTotalIncome.TabIndex = 1;
            this.txtTotalIncome.Text = "5000.00";

            // 
            // lblExp
            // 
            this.lblExp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblExp.AutoSize = true;
            this.lblExp.Location = new System.Drawing.Point(3, 47);
            this.lblExp.Name = "lblExp";
            this.lblExp.Size = new System.Drawing.Size(81, 15);
            this.lblExp.TabIndex = 2;
            this.lblExp.Text = "Total Expense:";

            // 
            // txtTotalExpense
            // 
            this.txtTotalExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTotalExpense.Location = new System.Drawing.Point(98, 39);
            this.txtTotalExpense.Name = "txtTotalExpense";
            this.txtTotalExpense.ReadOnly = true;
            this.txtTotalExpense.Size = new System.Drawing.Size(157, 23);
            this.txtTotalExpense.TabIndex = 3;
            this.txtTotalExpense.Text = "270.00";

            // 
            // lblSav
            // 
            this.lblSav.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSav.AutoSize = true;
            this.lblSav.Location = new System.Drawing.Point(3, 84);
            this.lblSav.Name = "lblSav";
            this.lblSav.Size = new System.Drawing.Size(45, 15);
            this.lblSav.TabIndex = 4;
            this.lblSav.Text = "Saving:";

            // 
            // txtSavings
            // 
            this.txtSavings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSavings.Location = new System.Drawing.Point(98, 75);
            this.txtSavings.Name = "txtSavings";
            this.txtSavings.ReadOnly = true;
            this.txtSavings.Size = new System.Drawing.Size(157, 23);
            this.txtSavings.TabIndex = 5;
            this.txtSavings.Text = "4730.00";

            // 
            // pnlRightWorkspace
            // 
            this.pnlRightWorkspace.Controls.Add(this.rightWorkspaceGrid);
            this.pnlRightWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightWorkspace.Location = new System.Drawing.Point(283, 3);
            this.pnlRightWorkspace.Name = "pnlRightWorkspace";
            this.pnlRightWorkspace.Padding = new System.Windows.Forms.Padding(8);
            this.pnlRightWorkspace.Size = new System.Drawing.Size(658, 360);
            this.pnlRightWorkspace.TabIndex = 1;
            this.pnlRightWorkspace.TabStop = false;
            this.pnlRightWorkspace.Text = "User Filter & Criteria Summary";

            // 
            // rightWorkspaceGrid
            // 
            this.rightWorkspaceGrid.ColumnCount = 1;
            this.rightWorkspaceGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightWorkspaceGrid.Controls.Add(this.filterHeaderPanel, 0, 0);
            this.rightWorkspaceGrid.Controls.Add(this.filterInputsGrid, 0, 1);
            this.rightWorkspaceGrid.Controls.Add(this.lblListBoxTitle, 0, 2);
            this.rightWorkspaceGrid.Controls.Add(this.lstSelectedFiltersSummary, 0, 3);
            this.rightWorkspaceGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightWorkspaceGrid.Location = new System.Drawing.Point(8, 24);
            this.rightWorkspaceGrid.Name = "rightWorkspaceGrid";
            this.rightWorkspaceGrid.RowCount = 4;
            this.rightWorkspaceGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.rightWorkspaceGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.rightWorkspaceGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.rightWorkspaceGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightWorkspaceGrid.Size = new System.Drawing.Size(642, 328);
            this.rightWorkspaceGrid.TabIndex = 0;

            // 
            // filterHeaderPanel
            // 
            this.filterHeaderPanel.AutoSize = true;
            this.filterHeaderPanel.Controls.Add(this.lblFilterPrompt);
            this.filterHeaderPanel.Controls.Add(this.chkFilterCategory);
            this.filterHeaderPanel.Controls.Add(this.chkFilterDescription);
            this.filterHeaderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.filterHeaderPanel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.filterHeaderPanel.Name = "filterHeaderPanel";
            this.filterHeaderPanel.Size = new System.Drawing.Size(642, 25);
            this.filterHeaderPanel.TabIndex = 0;
            this.filterHeaderPanel.WrapContents = false;

            // 
            // lblFilterPrompt
            // 
            this.lblFilterPrompt.AutoSize = true;
            this.lblFilterPrompt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilterPrompt.Location = new System.Drawing.Point(0, 3);
            this.lblFilterPrompt.Margin = new System.Windows.Forms.Padding(0, 3, 10, 0);
            this.lblFilterPrompt.Name = "lblFilterPrompt";
            this.lblFilterPrompt.Size = new System.Drawing.Size(103, 15);
            this.lblFilterPrompt.TabIndex = 0;
            this.lblFilterPrompt.Text = "User Filter Mode:";

            // 
            // chkFilterCategory
            // 
            this.chkFilterCategory.AutoSize = true;
            this.chkFilterCategory.Location = new System.Drawing.Point(113, 0);
            this.chkFilterCategory.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.chkFilterCategory.Name = "chkFilterCategory";
            this.chkFilterCategory.Size = new System.Drawing.Size(73, 19);
            this.chkFilterCategory.TabIndex = 1;
            this.chkFilterCategory.Text = "Category";
            this.chkFilterCategory.UseVisualStyleBackColor = true;
            this.chkFilterCategory.CheckedChanged += new System.EventHandler(this.FilterMode_CheckedChanged);

            // 
            // chkFilterDescription
            // 
            this.chkFilterDescription.AutoSize = true;
            this.chkFilterDescription.Location = new System.Drawing.Point(201, 0);
            this.chkFilterDescription.Margin = new System.Windows.Forms.Padding(0);
            this.chkFilterDescription.Name = "chkFilterDescription";
            this.chkFilterDescription.Size = new System.Drawing.Size(86, 19);
            this.chkFilterDescription.TabIndex = 2;
            this.chkFilterDescription.Text = "Description";
            this.chkFilterDescription.UseVisualStyleBackColor = true;
            this.chkFilterDescription.CheckedChanged += new System.EventHandler(this.FilterMode_CheckedChanged);

            // 
            // filterInputsGrid
            // 
            this.filterInputsGrid.ColumnCount = 2;
            this.filterInputsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.filterInputsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.filterInputsGrid.Controls.Add(this.pnlCategoryCheckboxes, 0, 0);
            this.filterInputsGrid.Controls.Add(this.pnlDescriptionInput, 1, 0);
            this.filterInputsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterInputsGrid.Location = new System.Drawing.Point(0, 31);
            this.filterInputsGrid.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.filterInputsGrid.Name = "filterInputsGrid";
            this.filterInputsGrid.RowCount = 1;
            this.filterInputsGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filterInputsGrid.Size = new System.Drawing.Size(642, 101);
            this.filterInputsGrid.TabIndex = 1;

            // 
            // pnlCategoryCheckboxes
            // 
            this.pnlCategoryCheckboxes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCategoryCheckboxes.Controls.Add(this.flowCategoryCheckboxes);
            this.pnlCategoryCheckboxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategoryCheckboxes.Location = new System.Drawing.Point(3, 3);
            this.pnlCategoryCheckboxes.Name = "pnlCategoryCheckboxes";
            this.pnlCategoryCheckboxes.Size = new System.Drawing.Size(315, 95);
            this.pnlCategoryCheckboxes.TabIndex = 0;
            this.pnlCategoryCheckboxes.Visible = false;

            // 
            // flowCategoryCheckboxes
            // 
            this.flowCategoryCheckboxes.AutoScroll = true;
            this.flowCategoryCheckboxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCategoryCheckboxes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowCategoryCheckboxes.Location = new System.Drawing.Point(0, 0);
            this.flowCategoryCheckboxes.Name = "flowCategoryCheckboxes";
            this.flowCategoryCheckboxes.Padding = new System.Windows.Forms.Padding(4);
            this.flowCategoryCheckboxes.Size = new System.Drawing.Size(313, 93);
            this.flowCategoryCheckboxes.TabIndex = 0;
            this.flowCategoryCheckboxes.WrapContents = false;

            // 
            // pnlDescriptionInput
            // 
            this.pnlDescriptionInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDescriptionInput.Controls.Add(this.txtDescriptionSearch);
            this.pnlDescriptionInput.Controls.Add(this.lblDescPrompt);
            this.pnlDescriptionInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDescriptionInput.Location = new System.Drawing.Point(324, 3);
            this.pnlDescriptionInput.Name = "pnlDescriptionInput";
            this.pnlDescriptionInput.Padding = new System.Windows.Forms.Padding(6);
            this.pnlDescriptionInput.Size = new System.Drawing.Size(315, 95);
            this.pnlDescriptionInput.TabIndex = 1;
            this.pnlDescriptionInput.Visible = false;

            // 
            // txtDescriptionSearch
            // 
            this.txtDescriptionSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDescriptionSearch.Location = new System.Drawing.Point(6, 21);
            this.txtDescriptionSearch.Name = "txtDescriptionSearch";
            this.txtDescriptionSearch.Size = new System.Drawing.Size(301, 23);
            this.txtDescriptionSearch.TabIndex = 1;
            this.txtDescriptionSearch.TextChanged += new System.EventHandler(this.DynamicFilter_Changed);

            // 
            // lblDescPrompt
            // 
            this.lblDescPrompt.AutoSize = true;
            this.lblDescPrompt.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescPrompt.Location = new System.Drawing.Point(6, 6);
            this.lblDescPrompt.Name = "lblDescPrompt";
            this.lblDescPrompt.Size = new System.Drawing.Size(130, 15);
            this.lblDescPrompt.TabIndex = 0;
            this.lblDescPrompt.Text = "Enter Description Text:";

            // 
            // lblListBoxTitle
            // 
            this.lblListBoxTitle.AutoSize = true;
            this.lblListBoxTitle.Location = new System.Drawing.Point(0, 140);
            this.lblListBoxTitle.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.lblListBoxTitle.Name = "lblListBoxTitle";
            this.lblListBoxTitle.Size = new System.Drawing.Size(166, 15);
            this.lblListBoxTitle.TabIndex = 2;
            this.lblListBoxTitle.Text = "Active Selected Filters (ListBox):";
            this.lblListBoxTitle.Visible = false;

            // 
            // lstSelectedFiltersSummary
            // 
            this.lstSelectedFiltersSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSelectedFiltersSummary.Enabled = false;
            this.lstSelectedFiltersSummary.FormattingEnabled = true;
            this.lstSelectedFiltersSummary.ItemHeight = 15;
            this.lstSelectedFiltersSummary.Location = new System.Drawing.Point(3, 160);
            this.lstSelectedFiltersSummary.Name = "lstSelectedFiltersSummary";
            this.lstSelectedFiltersSummary.Size = new System.Drawing.Size(636, 165);
            this.lstSelectedFiltersSummary.TabIndex = 3;
            this.lstSelectedFiltersSummary.Visible = false;

            // 
            // bottomListView
            // 
            this.bottomListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader { Text = "ID", Width = 50 },
                new System.Windows.Forms.ColumnHeader { Text = "Date", Width = 100 },
                new System.Windows.Forms.ColumnHeader { Text = "Category", Width = 130 },
                new System.Windows.Forms.ColumnHeader { Text = "Description", Width = 280 },
                new System.Windows.Forms.ColumnHeader { Text = "Amount ($)", Width = 110 },
                new System.Windows.Forms.ColumnHeader { Text = "Type", Width = 90 }
            });
            this.bottomListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomListView.FullRowSelect = true;
            this.bottomListView.GridLines = true;
            this.bottomListView.Location = new System.Drawing.Point(0, 372);
            this.bottomListView.Margin = new System.Windows.Forms.Padding(0);
            this.bottomListView.Name = "bottomListView";
            this.bottomListView.Size = new System.Drawing.Size(944, 228);
            this.bottomListView.TabIndex = 1;
            this.bottomListView.UseCompatibleStateImageBehavior = false;
            this.bottomListView.View = System.Windows.Forms.View.Details;

            // 
            // summaryContainerPanel
            // 
            this.summaryContainerPanel.Controls.Add(this.lvSummaryTransactions);
            this.summaryContainerPanel.Controls.Add(this.summaryCardsGrid);
            this.summaryContainerPanel.Controls.Add(this.grpSummaryFilter);
            this.summaryContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryContainerPanel.Location = new System.Drawing.Point(0, 0);
            this.summaryContainerPanel.Name = "summaryContainerPanel";
            this.summaryContainerPanel.Size = new System.Drawing.Size(944, 600);
            this.summaryContainerPanel.TabIndex = 2;
            this.summaryContainerPanel.Visible = false;

            // 
            // grpSummaryFilter
            // 
            this.grpSummaryFilter.Controls.Add(this.dudSummaryYear);
            this.grpSummaryFilter.Controls.Add(this.lblSummaryYear);
            this.grpSummaryFilter.Controls.Add(this.dudSummaryMonth);
            this.grpSummaryFilter.Controls.Add(this.lblSummaryMonth);
            this.grpSummaryFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSummaryFilter.Location = new System.Drawing.Point(0, 0);
            this.grpSummaryFilter.Name = "grpSummaryFilter";
            this.grpSummaryFilter.Padding = new System.Windows.Forms.Padding(8);
            this.grpSummaryFilter.Size = new System.Drawing.Size(944, 60);
            this.grpSummaryFilter.TabIndex = 0;
            this.grpSummaryFilter.TabStop = false;
            this.grpSummaryFilter.Text = "Month-Wise Filter (DomainUpDown)";

            // 
            // lblSummaryMonth
            // 
            this.lblSummaryMonth.AutoSize = true;
            this.lblSummaryMonth.Location = new System.Drawing.Point(12, 26);
            this.lblSummaryMonth.Name = "lblSummaryMonth";
            this.lblSummaryMonth.Size = new System.Drawing.Size(46, 15);
            this.lblSummaryMonth.TabIndex = 0;
            this.lblSummaryMonth.Text = "Month:";

            // 
            // dudSummaryMonth
            // 
            this.dudSummaryMonth.Items.Add("December");
            this.dudSummaryMonth.Items.Add("November");
            this.dudSummaryMonth.Items.Add("October");
            this.dudSummaryMonth.Items.Add("September");
            this.dudSummaryMonth.Items.Add("August");
            this.dudSummaryMonth.Items.Add("July");
            this.dudSummaryMonth.Items.Add("June");
            this.dudSummaryMonth.Items.Add("May");
            this.dudSummaryMonth.Items.Add("April");
            this.dudSummaryMonth.Items.Add("March");
            this.dudSummaryMonth.Items.Add("February");
            this.dudSummaryMonth.Items.Add("January");
            this.dudSummaryMonth.Location = new System.Drawing.Point(64, 23);
            this.dudSummaryMonth.Name = "dudSummaryMonth";
            this.dudSummaryMonth.Size = new System.Drawing.Size(120, 23);
            this.dudSummaryMonth.TabIndex = 1;
            this.dudSummaryMonth.SelectedItemChanged += new System.EventHandler(this.SummaryPeriod_Changed);

            // 
            // lblSummaryYear
            // 
            this.lblSummaryYear.AutoSize = true;
            this.lblSummaryYear.Location = new System.Drawing.Point(210, 26);
            this.lblSummaryYear.Name = "lblSummaryYear";
            this.lblSummaryYear.Size = new System.Drawing.Size(32, 15);
            this.lblSummaryYear.TabIndex = 2;
            this.lblSummaryYear.Text = "Year:";

            // 
            // dudSummaryYear
            // 
            this.dudSummaryYear.Items.Add("2029");
            this.dudSummaryYear.Items.Add("2028");
            this.dudSummaryYear.Items.Add("2027");
            this.dudSummaryYear.Items.Add("2026");
            this.dudSummaryYear.Items.Add("2025");
            this.dudSummaryYear.Items.Add("2024");
            this.dudSummaryYear.Items.Add("2023");
            this.dudSummaryYear.Location = new System.Drawing.Point(248, 23);
            this.dudSummaryYear.Name = "dudSummaryYear";
            this.dudSummaryYear.Size = new System.Drawing.Size(80, 23);
            this.dudSummaryYear.TabIndex = 3;
            this.dudSummaryYear.SelectedItemChanged += new System.EventHandler(this.SummaryPeriod_Changed);

            // 
            // summaryCardsGrid
            // 
            this.summaryCardsGrid.ColumnCount = 3;
            this.summaryCardsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.summaryCardsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.summaryCardsGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.summaryCardsGrid.Controls.Add(this.lblSummaryIncTitle, 0, 0);
            this.summaryCardsGrid.Controls.Add(this.lblSummaryExpTitle, 1, 0);
            this.summaryCardsGrid.Controls.Add(this.lblSummaryBalTitle, 2, 0);
            this.summaryCardsGrid.Controls.Add(this.pbSummaryIncome, 0, 1);
            this.summaryCardsGrid.Controls.Add(this.pbSummaryExpense, 1, 1);
            this.summaryCardsGrid.Controls.Add(this.pbSummaryBalance, 2, 1);
            this.summaryCardsGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.summaryCardsGrid.Location = new System.Drawing.Point(0, 60);
            this.summaryCardsGrid.Name = "summaryCardsGrid";
            this.summaryCardsGrid.RowCount = 2;
            this.summaryCardsGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.summaryCardsGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.summaryCardsGrid.Size = new System.Drawing.Size(944, 120);
            this.summaryCardsGrid.TabIndex = 1;

            // 
            // lblSummaryIncTitle
            // 
            this.lblSummaryIncTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummaryIncTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryIncTitle.ForeColor = System.Drawing.Color.SeaGreen;
            this.lblSummaryIncTitle.Location = new System.Drawing.Point(3, 0);
            this.lblSummaryIncTitle.Name = "lblSummaryIncTitle";
            this.lblSummaryIncTitle.Size = new System.Drawing.Size(308, 50);
            this.lblSummaryIncTitle.TabIndex = 0;
            this.lblSummaryIncTitle.Text = "Total Income:\r\n$0.00";
            this.lblSummaryIncTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblSummaryExpTitle
            // 
            this.lblSummaryExpTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummaryExpTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryExpTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSummaryExpTitle.Location = new System.Drawing.Point(317, 0);
            this.lblSummaryExpTitle.Name = "lblSummaryExpTitle";
            this.lblSummaryExpTitle.Size = new System.Drawing.Size(308, 50);
            this.lblSummaryExpTitle.TabIndex = 1;
            this.lblSummaryExpTitle.Text = "Total Expense:\r\n$0.00";
            this.lblSummaryExpTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblSummaryBalTitle
            // 
            this.lblSummaryBalTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSummaryBalTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSummaryBalTitle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblSummaryBalTitle.Location = new System.Drawing.Point(631, 0);
            this.lblSummaryBalTitle.Name = "lblSummaryBalTitle";
            this.lblSummaryBalTitle.Size = new System.Drawing.Size(310, 50);
            this.lblSummaryBalTitle.TabIndex = 2;
            this.lblSummaryBalTitle.Text = "Net Balance:\r\n$0.00";
            this.lblSummaryBalTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // pbSummaryIncome
            // 
            this.pbSummaryIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSummaryIncome.Location = new System.Drawing.Point(12, 54);
            this.pbSummaryIncome.Margin = new System.Windows.Forms.Padding(12, 4, 12, 12);
            this.pbSummaryIncome.Name = "pbSummaryIncome";
            this.pbSummaryIncome.Size = new System.Drawing.Size(290, 54);
            this.pbSummaryIncome.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSummaryIncome.TabIndex = 3;

            // 
            // pbSummaryExpense
            // 
            this.pbSummaryExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSummaryExpense.Location = new System.Drawing.Point(326, 54);
            this.pbSummaryExpense.Margin = new System.Windows.Forms.Padding(12, 4, 12, 12);
            this.pbSummaryExpense.Name = "pbSummaryExpense";
            this.pbSummaryExpense.Size = new System.Drawing.Size(290, 54);
            this.pbSummaryExpense.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSummaryExpense.TabIndex = 4;

            // 
            // pbSummaryBalance
            // 
            this.pbSummaryBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbSummaryBalance.Location = new System.Drawing.Point(640, 54);
            this.pbSummaryBalance.Margin = new System.Windows.Forms.Padding(12, 4, 12, 12);
            this.pbSummaryBalance.Name = "pbSummaryBalance";
            this.pbSummaryBalance.Size = new System.Drawing.Size(292, 54);
            this.pbSummaryBalance.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbSummaryBalance.TabIndex = 5;

            // 
            // lvSummaryTransactions
            // 
            this.lvSummaryTransactions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                new System.Windows.Forms.ColumnHeader { Text = "ID", Width = 50 },
                new System.Windows.Forms.ColumnHeader { Text = "Date", Width = 100 },
                new System.Windows.Forms.ColumnHeader { Text = "Category", Width = 130 },
                new System.Windows.Forms.ColumnHeader { Text = "Description", Width = 280 },
                new System.Windows.Forms.ColumnHeader { Text = "Amount ($)", Width = 110 },
                new System.Windows.Forms.ColumnHeader { Text = "Type", Width = 90 }
            });
            this.lvSummaryTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSummaryTransactions.FullRowSelect = true;
            this.lvSummaryTransactions.GridLines = true;
            this.lvSummaryTransactions.Location = new System.Drawing.Point(0, 180);
            this.lvSummaryTransactions.Name = "lvSummaryTransactions";
            this.lvSummaryTransactions.Size = new System.Drawing.Size(944, 420);
            this.lvSummaryTransactions.TabIndex = 2;
            this.lvSummaryTransactions.UseCompatibleStateImageBehavior = false;
            this.lvSummaryTransactions.View = System.Windows.Forms.View.Details;

            // 
            // graphContainerPanel
            // 
            this.graphContainerPanel.Controls.Add(this.picGraph);
            this.graphContainerPanel.Controls.Add(this.grpGraphFilter);
            this.graphContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphContainerPanel.Location = new System.Drawing.Point(0, 0);
            this.graphContainerPanel.Name = "graphContainerPanel";
            this.graphContainerPanel.Size = new System.Drawing.Size(944, 600);
            this.graphContainerPanel.TabIndex = 3;
            this.graphContainerPanel.Visible = false;

            // 
            // grpGraphFilter
            // 
            this.grpGraphFilter.Controls.Add(this.lblGraphSummaryInfo);
            this.grpGraphFilter.Controls.Add(this.dudGraphYear);
            this.grpGraphFilter.Controls.Add(this.lblGraphYear);
            this.grpGraphFilter.Controls.Add(this.dudGraphMonth);
            this.grpGraphFilter.Controls.Add(this.lblGraphMonth);
            this.grpGraphFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpGraphFilter.Location = new System.Drawing.Point(0, 0);
            this.grpGraphFilter.Name = "grpGraphFilter";
            this.grpGraphFilter.Padding = new System.Windows.Forms.Padding(8);
            this.grpGraphFilter.Size = new System.Drawing.Size(944, 60);
            this.grpGraphFilter.TabIndex = 0;
            this.grpGraphFilter.TabStop = false;
            this.grpGraphFilter.Text = "Month-Wise Graph Filter (DomainUpDown)";

            // 
            // lblGraphMonth
            // 
            this.lblGraphMonth.AutoSize = true;
            this.lblGraphMonth.Location = new System.Drawing.Point(12, 26);
            this.lblGraphMonth.Name = "lblGraphMonth";
            this.lblGraphMonth.Size = new System.Drawing.Size(46, 15);
            this.lblGraphMonth.TabIndex = 0;
            this.lblGraphMonth.Text = "Month:";

            // 
            // dudGraphMonth
            // 
            this.dudGraphMonth.Items.Add("December");
            this.dudGraphMonth.Items.Add("November");
            this.dudGraphMonth.Items.Add("October");
            this.dudGraphMonth.Items.Add("September");
            this.dudGraphMonth.Items.Add("August");
            this.dudGraphMonth.Items.Add("July");
            this.dudGraphMonth.Items.Add("June");
            this.dudGraphMonth.Items.Add("May");
            this.dudGraphMonth.Items.Add("April");
            this.dudGraphMonth.Items.Add("March");
            this.dudGraphMonth.Items.Add("February");
            this.dudGraphMonth.Items.Add("January");
            this.dudGraphMonth.Location = new System.Drawing.Point(64, 23);
            this.dudGraphMonth.Name = "dudGraphMonth";
            this.dudGraphMonth.Size = new System.Drawing.Size(120, 23);
            this.dudGraphMonth.TabIndex = 1;
            this.dudGraphMonth.SelectedItemChanged += new System.EventHandler(this.GraphPeriod_Changed);

            // 
            // lblGraphYear
            // 
            this.lblGraphYear.AutoSize = true;
            this.lblGraphYear.Location = new System.Drawing.Point(210, 26);
            this.lblGraphYear.Name = "lblGraphYear";
            this.lblGraphYear.Size = new System.Drawing.Size(32, 15);
            this.lblGraphYear.TabIndex = 2;
            this.lblGraphYear.Text = "Year:";

            // 
            // dudGraphYear
            // 
            this.dudGraphYear.Items.Add("2029");
            this.dudGraphYear.Items.Add("2028");
            this.dudGraphYear.Items.Add("2027");
            this.dudGraphYear.Items.Add("2026");
            this.dudGraphYear.Items.Add("2025");
            this.dudGraphYear.Items.Add("2024");
            this.dudGraphYear.Items.Add("2023");
            this.dudGraphYear.Location = new System.Drawing.Point(248, 23);
            this.dudGraphYear.Name = "dudGraphYear";
            this.dudGraphYear.Size = new System.Drawing.Size(80, 23);
            this.dudGraphYear.TabIndex = 3;
            this.dudGraphYear.SelectedItemChanged += new System.EventHandler(this.GraphPeriod_Changed);

            // 
            // lblGraphSummaryInfo
            // 
            this.lblGraphSummaryInfo.AutoSize = true;
            this.lblGraphSummaryInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblGraphSummaryInfo.Location = new System.Drawing.Point(360, 26);
            this.lblGraphSummaryInfo.Name = "lblGraphSummaryInfo";
            this.lblGraphSummaryInfo.Size = new System.Drawing.Size(176, 15);
            this.lblGraphSummaryInfo.TabIndex = 4;
            this.lblGraphSummaryInfo.Text = "Displaying financial distribution";

            // 
            // picGraph
            // 
            this.picGraph.BackColor = System.Drawing.Color.White;
            this.picGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGraph.Location = new System.Drawing.Point(0, 60);
            this.picGraph.Name = "picGraph";
            this.picGraph.Size = new System.Drawing.Size(944, 540);
            this.picGraph.TabIndex = 1;
            this.picGraph.TabStop = false;
            this.picGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.picGraph_Paint);

            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.workspaceContainerPanel);
            this.Controls.Add(this.menuStripMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(820, 540);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MoneyFlow Desktop";

            // Resume Layouts
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.workspaceContainerPanel.ResumeLayout(false);
            this.masterMainGrid.ResumeLayout(false);
            this.middleLayoutTable.ResumeLayout(false);
            this.pnlLeftFields.ResumeLayout(false);
            this.leftGrid.ResumeLayout(false);
            this.leftGrid.PerformLayout();
            this.pnlRightWorkspace.ResumeLayout(false);
            this.rightWorkspaceGrid.ResumeLayout(false);
            this.rightWorkspaceGrid.PerformLayout();
            this.filterHeaderPanel.ResumeLayout(false);
            this.filterHeaderPanel.PerformLayout();
            this.filterInputsGrid.ResumeLayout(false);
            this.pnlCategoryCheckboxes.ResumeLayout(false);
            this.pnlDescriptionInput.ResumeLayout(false);
            this.pnlDescriptionInput.PerformLayout();
            this.summaryContainerPanel.ResumeLayout(false);
            this.grpSummaryFilter.ResumeLayout(false);
            this.grpSummaryFilter.PerformLayout();
            this.summaryCardsGrid.ResumeLayout(false);
            this.graphContainerPanel.ResumeLayout(false);
            this.grpGraphFilter.ResumeLayout(false);
            this.grpGraphFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}