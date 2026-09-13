using System.Drawing;
using System.Windows.Forms;

namespace MoneyFlow
{
    partial class FrmReport
    {
        private ComboBox _filterModeComboBox = null!;
        private DateTimePicker _filterDatePicker = null!;
        private Label _periodLabel = null!;
        private Label _incomeLabel = null!;
        private Label _expenseLabel = null!;
        private Label _balanceLabel = null!;
        private ProgressBar _incomeProgressBar = null!;
        private ProgressBar _expenseProgressBar = null!;
        private ProgressBar _balanceProgressBar = null!;
        private ListView _reportListView = null!;
        private PictureBox _reportPictureBox = null!;

        private void InitializeComponent()
        {
            Text = "MoneyFlow Reports";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(900, 620);
            Size = new Size(1100, 720);
            Font = new Font("Segoe UI", 9F);

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 145F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            GroupBox filterGroup = new GroupBox { Text = "Report Filter", Dock = DockStyle.Fill, Padding = new Padding(10) };
            FlowLayoutPanel filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };

            filterPanel.Controls.Add(new Label { Text = "Period:", AutoSize = true, Margin = new Padding(0, 7, 6, 0) });
            _filterModeComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 110,
                Margin = new Padding(0, 2, 18, 0)
            };
            _filterModeComboBox.Items.AddRange(new object[] { "Day", "Month", "Year" });
            _filterModeComboBox.SelectedIndex = 0;
            _filterModeComboBox.SelectedIndexChanged += FilterChanged;
            filterPanel.Controls.Add(_filterModeComboBox);

            filterPanel.Controls.Add(new Label { Text = "Date:", AutoSize = true, Margin = new Padding(0, 7, 6, 0) });
            _filterDatePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd",
                Width = 125,
                Value = DateTime.Today,
                Margin = new Padding(0, 2, 18, 0)
            };
            _filterDatePicker.ValueChanged += FilterChanged;
            filterPanel.Controls.Add(_filterDatePicker);

            _periodLabel = new Label { AutoSize = true, Margin = new Padding(0, 7, 0, 0) };
            filterPanel.Controls.Add(_periodLabel);
            filterGroup.Controls.Add(filterPanel);
            mainLayout.Controls.Add(filterGroup, 0, 0);

            TableLayoutPanel summaryLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 2,
                Padding = new Padding(0, 5, 0, 5)
            };
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            summaryLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            summaryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _incomeLabel = CreateSummaryLabel(Color.SeaGreen);
            _expenseLabel = CreateSummaryLabel(Color.Firebrick);
            _balanceLabel = CreateSummaryLabel(Color.DarkSlateBlue);
            summaryLayout.Controls.Add(_incomeLabel, 0, 0);
            summaryLayout.Controls.Add(_expenseLabel, 1, 0);
            summaryLayout.Controls.Add(_balanceLabel, 2, 0);

            _incomeProgressBar = CreateProgressBar();
            _expenseProgressBar = CreateProgressBar();
            _balanceProgressBar = CreateProgressBar();
            summaryLayout.Controls.Add(_incomeProgressBar, 0, 1);
            summaryLayout.Controls.Add(_expenseProgressBar, 1, 1);
            summaryLayout.Controls.Add(_balanceProgressBar, 2, 1);
            mainLayout.Controls.Add(summaryLayout, 0, 1);

            TableLayoutPanel resultLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            resultLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));

            _reportListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                HideSelection = false
            };
            _reportListView.Columns.Add("ID", 55);
            _reportListView.Columns.Add("Date", 100);
            _reportListView.Columns.Add("Category", 120);
            _reportListView.Columns.Add("Description", 220);
            _reportListView.Columns.Add("Type", 85);
            _reportListView.Columns.Add("Amount ($)", 105);
            resultLayout.Controls.Add(_reportListView, 0, 0);

            _reportPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Normal,
                Margin = new Padding(12, 0, 0, 0)
            };
            _reportPictureBox.Paint += ReportPictureBoxPaint;
            resultLayout.Controls.Add(_reportPictureBox, 1, 0);
            mainLayout.Controls.Add(resultLayout, 0, 2);

            Controls.Add(mainLayout);
        }

        private Label CreateSummaryLabel(Color color)
        {
            return new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = color,
                Font = new Font(Font, FontStyle.Bold)
            };
        }

        private ProgressBar CreateProgressBar()
        {
            return new ProgressBar
            {
                Dock = DockStyle.Fill,
                Maximum = 100,
                Style = ProgressBarStyle.Continuous,
                Margin = new Padding(16, 8, 16, 8)
            };
        }
    }
}
