using System.Drawing;
using System.Windows.Forms;

namespace MoneyFlow
{
    partial class FrmReport
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel mainLayout;
        private GroupBox filterGroup;
        private FlowLayoutPanel filterPanel;
        private Label lblPeriodPrompt;
        private ComboBox _filterModeComboBox;
        private Label lblDatePrompt;
        private DateTimePicker _filterDatePicker;
        private Label _periodLabel;

        private TableLayoutPanel summaryLayout;
        private Label _incomeLabel;
        private Label _expenseLabel;
        private Label _balanceLabel;
        private ProgressBar _incomeProgressBar;
        private ProgressBar _expenseProgressBar;
        private ProgressBar _balanceProgressBar;

        private TableLayoutPanel resultLayout;
        private ListView _reportListView;
        private PictureBox _reportPictureBox;

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

            this.mainLayout = new TableLayoutPanel();
            this.filterGroup = new GroupBox();
            this.filterPanel = new FlowLayoutPanel();
            this.lblPeriodPrompt = new Label();
            this._filterModeComboBox = new ComboBox();
            this.lblDatePrompt = new Label();
            this._filterDatePicker = new DateTimePicker();
            this._periodLabel = new Label();

            this.summaryLayout = new TableLayoutPanel();
            this._incomeLabel = new Label();
            this._expenseLabel = new Label();
            this._balanceLabel = new Label();
            this._incomeProgressBar = new ProgressBar();
            this._expenseProgressBar = new ProgressBar();
            this._balanceProgressBar = new ProgressBar();

            this.resultLayout = new TableLayoutPanel();
            this._reportListView = new ListView();
            this._reportPictureBox = new PictureBox();

            this.mainLayout.SuspendLayout();
            this.filterGroup.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.summaryLayout.SuspendLayout();
            this.resultLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._reportPictureBox)).BeginInit();
            this.SuspendLayout();

            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.filterGroup, 0, 0);
            this.mainLayout.Controls.Add(this.summaryLayout, 0, 1);
            this.mainLayout.Controls.Add(this.resultLayout, 0, 2);
            this.mainLayout.Dock = DockStyle.Fill;
            this.mainLayout.Location = new Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new Padding(12);
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 145F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.mainLayout.Size = new Size(1100, 720);
            this.mainLayout.TabIndex = 0;

            // 
            // filterGroup
            // 
            this.filterGroup.Controls.Add(this.filterPanel);
            this.filterGroup.Dock = DockStyle.Fill;
            this.filterGroup.Location = new Point(15, 15);
            this.filterGroup.Name = "filterGroup";
            this.filterGroup.Padding = new Padding(10);
            this.filterGroup.Size = new Size(1070, 72);
            this.filterGroup.TabIndex = 0;
            this.filterGroup.TabStop = false;
            this.filterGroup.Text = "Report Filter";

            // 
            // filterPanel
            // 
            this.filterPanel.AutoSize = true;
            this.filterPanel.Controls.Add(this.lblPeriodPrompt);
            this.filterPanel.Controls.Add(this._filterModeComboBox);
            this.filterPanel.Controls.Add(this.lblDatePrompt);
            this.filterPanel.Controls.Add(this._filterDatePicker);
            this.filterPanel.Controls.Add(this._periodLabel);
            this.filterPanel.Dock = DockStyle.Fill;
            this.filterPanel.FlowDirection = FlowDirection.LeftToRight;
            this.filterPanel.Location = new Point(10, 26);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new Size(1050, 36);
            this.filterPanel.TabIndex = 0;
            this.filterPanel.WrapContents = false;

            // 
            // lblPeriodPrompt
            // 
            this.lblPeriodPrompt.AutoSize = true;
            this.lblPeriodPrompt.Location = new Point(0, 7);
            this.lblPeriodPrompt.Margin = new Padding(0, 7, 6, 0);
            this.lblPeriodPrompt.Name = "lblPeriodPrompt";
            this.lblPeriodPrompt.Size = new Size(44, 15);
            this.lblPeriodPrompt.TabIndex = 0;
            this.lblPeriodPrompt.Text = "Period:";

            // 
            // _filterModeComboBox
            // 
            this._filterModeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this._filterModeComboBox.FormattingEnabled = true;
            this._filterModeComboBox.Items.AddRange(new object[] { "Day", "Month", "Year" });
            this._filterModeComboBox.Location = new Point(53, 2);
            this._filterModeComboBox.Margin = new Padding(0, 2, 18, 0);
            this._filterModeComboBox.Name = "_filterModeComboBox";
            this._filterModeComboBox.Size = new Size(110, 23);
            this._filterModeComboBox.TabIndex = 1;
            this._filterModeComboBox.SelectedIndex = 0;
            this._filterModeComboBox.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);

            // 
            // lblDatePrompt
            // 
            this.lblDatePrompt.AutoSize = true;
            this.lblDatePrompt.Location = new Point(184, 7);
            this.lblDatePrompt.Margin = new Padding(0, 7, 6, 0);
            this.lblDatePrompt.Name = "lblDatePrompt";
            this.lblDatePrompt.Size = new Size(34, 15);
            this.lblDatePrompt.TabIndex = 2;
            this.lblDatePrompt.Text = "Date:";

            // 
            // _filterDatePicker
            // 
            this._filterDatePicker.CustomFormat = "yyyy-MM-dd";
            this._filterDatePicker.Format = DateTimePickerFormat.Custom;
            this._filterDatePicker.Location = new Point(221, 2);
            this._filterDatePicker.Margin = new Padding(0, 2, 18, 0);
            this._filterDatePicker.Name = "_filterDatePicker";
            this._filterDatePicker.Size = new Size(125, 23);
            this._filterDatePicker.TabIndex = 3;
            this._filterDatePicker.ValueChanged += new System.EventHandler(this.FilterChanged);

            // 
            // _periodLabel
            // 
            this._periodLabel.AutoSize = true;
            this._periodLabel.Location = new Point(367, 7);
            this._periodLabel.Margin = new Padding(0, 7, 0, 0);
            this._periodLabel.Name = "_periodLabel";
            this._periodLabel.Size = new Size(95, 15);
            this._periodLabel.TabIndex = 4;
            this._periodLabel.Text = "Showing: Today";

            // 
            // summaryLayout
            // 
            this.summaryLayout.ColumnCount = 3;
            this.summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.summaryLayout.Controls.Add(this._incomeLabel, 0, 0);
            this.summaryLayout.Controls.Add(this._expenseLabel, 1, 0);
            this.summaryLayout.Controls.Add(this._balanceLabel, 2, 0);
            this.summaryLayout.Controls.Add(this._incomeProgressBar, 0, 1);
            this.summaryLayout.Controls.Add(this._expenseProgressBar, 1, 1);
            this.summaryLayout.Controls.Add(this._balanceProgressBar, 2, 1);
            this.summaryLayout.Dock = DockStyle.Fill;
            this.summaryLayout.Location = new Point(15, 93);
            this.summaryLayout.Name = "summaryLayout";
            this.summaryLayout.RowCount = 2;
            this.summaryLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            this.summaryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.summaryLayout.Size = new Size(1070, 139);
            this.summaryLayout.TabIndex = 1;

            // 
            // _incomeLabel
            // 
            this._incomeLabel.Dock = DockStyle.Fill;
            this._incomeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._incomeLabel.ForeColor = Color.SeaGreen;
            this._incomeLabel.Location = new Point(3, 0);
            this._incomeLabel.Name = "_incomeLabel";
            this._incomeLabel.Size = new Size(350, 52);
            this._incomeLabel.TabIndex = 0;
            this._incomeLabel.Text = "Total Income\r\n0.00";
            this._incomeLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // _expenseLabel
            // 
            this._expenseLabel.Dock = DockStyle.Fill;
            this._expenseLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._expenseLabel.ForeColor = Color.Firebrick;
            this._expenseLabel.Location = new Point(359, 0);
            this._expenseLabel.Name = "_expenseLabel";
            this._expenseLabel.Size = new Size(350, 52);
            this._expenseLabel.TabIndex = 1;
            this._expenseLabel.Text = "Total Expense\r\n0.00";
            this._expenseLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // _balanceLabel
            // 
            this._balanceLabel.Dock = DockStyle.Fill;
            this._balanceLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this._balanceLabel.ForeColor = Color.DarkSlateBlue;
            this._balanceLabel.Location = new Point(715, 0);
            this._balanceLabel.Name = "_balanceLabel";
            this._balanceLabel.Size = new Size(352, 52);
            this._balanceLabel.TabIndex = 2;
            this._balanceLabel.Text = "Balance\r\n0.00";
            this._balanceLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // _incomeProgressBar
            // 
            this._incomeProgressBar.Dock = DockStyle.Fill;
            this._incomeProgressBar.Location = new Point(16, 60);
            this._incomeProgressBar.Margin = new Padding(16, 8, 16, 8);
            this._incomeProgressBar.Maximum = 100;
            this._incomeProgressBar.Name = "_incomeProgressBar";
            this._incomeProgressBar.Size = new Size(324, 71);
            this._incomeProgressBar.Style = ProgressBarStyle.Continuous;
            this._incomeProgressBar.TabIndex = 3;

            // 
            // _expenseProgressBar
            // 
            this._expenseProgressBar.Dock = DockStyle.Fill;
            this._expenseProgressBar.Location = new Point(372, 60);
            this._expenseProgressBar.Margin = new Padding(16, 8, 16, 8);
            this._expenseProgressBar.Maximum = 100;
            this._expenseProgressBar.Name = "_expenseProgressBar";
            this._expenseProgressBar.Size = new Size(324, 71);
            this._expenseProgressBar.Style = ProgressBarStyle.Continuous;
            this._expenseProgressBar.TabIndex = 4;

            // 
            // _balanceProgressBar
            // 
            this._balanceProgressBar.Dock = DockStyle.Fill;
            this._balanceProgressBar.Location = new Point(728, 60);
            this._balanceProgressBar.Margin = new Padding(16, 8, 16, 8);
            this._balanceProgressBar.Maximum = 100;
            this._balanceProgressBar.Name = "_balanceProgressBar";
            this._balanceProgressBar.Size = new Size(326, 71);
            this._balanceProgressBar.Style = ProgressBarStyle.Continuous;
            this._balanceProgressBar.TabIndex = 5;

            // 
            // resultLayout
            // 
            this.resultLayout.ColumnCount = 2;
            this.resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            this.resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            this.resultLayout.Controls.Add(this._reportListView, 0, 0);
            this.resultLayout.Controls.Add(this._reportPictureBox, 1, 0);
            this.resultLayout.Dock = DockStyle.Fill;
            this.resultLayout.Location = new Point(15, 238);
            this.resultLayout.Name = "resultLayout";
            this.resultLayout.RowCount = 1;
            this.resultLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.resultLayout.Size = new Size(1070, 467);
            this.resultLayout.TabIndex = 2;

            // 
            // _reportListView
            // 
            this._reportListView.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader { Text = "ID", Width = 55 },
                new ColumnHeader { Text = "Date", Width = 100 },
                new ColumnHeader { Text = "Category", Width = 120 },
                new ColumnHeader { Text = "Description", Width = 220 },
                new ColumnHeader { Text = "Type", Width = 85 },
                new ColumnHeader { Text = "Amount ($)", Width = 105 }
            });
            this._reportListView.Dock = DockStyle.Fill;
            this._reportListView.FullRowSelect = true;
            this._reportListView.GridLines = true;
            this._reportListView.HideSelection = false;
            this._reportListView.Location = new Point(3, 3);
            this._reportListView.Name = "_reportListView";
            this._reportListView.Size = new Size(614, 461);
            this._reportListView.TabIndex = 0;
            this._reportListView.UseCompatibleStateImageBehavior = false;
            this._reportListView.View = System.Windows.Forms.View.Details;

            // 
            // _reportPictureBox
            // 
            this._reportPictureBox.BackColor = Color.White;
            this._reportPictureBox.BorderStyle = BorderStyle.FixedSingle;
            this._reportPictureBox.Dock = DockStyle.Fill;
            this._reportPictureBox.Location = new Point(632, 3);
            this._reportPictureBox.Margin = new Padding(12, 3, 3, 3);
            this._reportPictureBox.Name = "_reportPictureBox";
            this._reportPictureBox.Size = new Size(435, 461);
            this._reportPictureBox.TabIndex = 1;
            this._reportPictureBox.TabStop = false;
            this._reportPictureBox.Paint += new PaintEventHandler(this.ReportPictureBoxPaint);

            // 
            // FrmReport
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 720);
            this.Controls.Add(this.mainLayout);
            this.Font = new Font("Segoe UI", 9F);
            this.MinimumSize = new Size(900, 620);
            this.Name = "FrmReport";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "MoneyFlow Reports";

            this.mainLayout.ResumeLayout(false);
            this.filterGroup.ResumeLayout(false);
            this.filterGroup.PerformLayout();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.summaryLayout.ResumeLayout(false);
            this.resultLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._reportPictureBox)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
