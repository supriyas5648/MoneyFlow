using System;
using System.Drawing;
using System.Windows.Forms;

namespace MoneyFlow
{
    partial class FrmSummary
    {
        private ComboBox _filterModeComboBox = null!;
        private DateTimePicker _filterDatePicker = null!;
        private NumericUpDown _startYearPicker = null!;
        private NumericUpDown _endYearPicker = null!;
        private Label _filterDateLabel = null!;
        private Label _startYearLabel = null!;
        private Label _endYearLabel = null!;
        private Label _periodLabel = null!;
        private Label _incomeLabel = null!;
        private Label _expenseLabel = null!;
        private Label _savingsLabel = null!;
        private ProgressBar _incomeProgressBar = null!;
        private ProgressBar _expenseProgressBar = null!;
        private ProgressBar _savingsProgressBar = null!;

        private void InitializeComponent()
        {
            Text = "Financial Summary";
            StartPosition = FormStartPosition.CenterParent;
            MinimumSize = new Size(620, 390);
            Size = new Size(760, 450);
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(20)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

            TableLayoutPanel header = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            header.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            header.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            header.Controls.Add(new Label
            {
                Text = "Financial Summary",
                Dock = DockStyle.Fill,
                Font = new Font(Font, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, 0);

            FlowLayoutPanel filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                AutoScroll = true
            };
            filterPanel.Controls.Add(new Label { Text = "Period:", AutoSize = true, Margin = new Padding(0, 5, 6, 0) });
            _filterModeComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 100,
                Margin = new Padding(0, 0, 12, 0)
            };
            _filterModeComboBox.Items.AddRange(new object[] { "Day", "Month", "Year" });
            _filterModeComboBox.SelectedIndex = 0;
            _filterModeComboBox.SelectedIndexChanged += FilterChanged;
            filterPanel.Controls.Add(_filterModeComboBox);

            _filterDateLabel = new Label { Text = "Day:", AutoSize = true, Margin = new Padding(0, 5, 6, 0) };
            filterPanel.Controls.Add(_filterDateLabel);
            _filterDatePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MMMM dd, yyyy",
                ShowUpDown = false,
                Width = 125,
                Value = DateTime.Today,
                Margin = new Padding(0, 0, 12, 0)
            };
            _filterDatePicker.ValueChanged += FilterChanged;
            filterPanel.Controls.Add(_filterDatePicker);

            _startYearLabel = new Label { Text = "From:", AutoSize = true, Visible = false, Margin = new Padding(0, 5, 6, 0) };
            _startYearPicker = CreateYearPicker();
            _startYearPicker.Visible = false;
            _startYearPicker.ValueChanged += FilterChanged;
            _endYearLabel = new Label { Text = "To:", AutoSize = true, Visible = false, Margin = new Padding(0, 5, 6, 0) };
            _endYearPicker = CreateYearPicker();
            _endYearPicker.Visible = false;
            _endYearPicker.ValueChanged += FilterChanged;
            filterPanel.Controls.Add(_startYearLabel);
            filterPanel.Controls.Add(_startYearPicker);
            filterPanel.Controls.Add(_endYearLabel);
            filterPanel.Controls.Add(_endYearPicker);
            _periodLabel = new Label { AutoSize = true, Margin = new Padding(0, 5, 0, 0) };
            filterPanel.Controls.Add(_periodLabel);
            header.Controls.Add(filterPanel, 0, 1);
            layout.Controls.Add(header, 0, 0);

            _incomeLabel = CreateSummaryLabel(Color.SeaGreen);
            _incomeProgressBar = CreateProgressBar();
            layout.Controls.Add(CreateSummaryRow(_incomeLabel, _incomeProgressBar), 0, 1);

            _expenseLabel = CreateSummaryLabel(Color.Firebrick);
            _expenseProgressBar = CreateProgressBar();
            layout.Controls.Add(CreateSummaryRow(_expenseLabel, _expenseProgressBar), 0, 2);

            _savingsLabel = CreateSummaryLabel(Color.DarkSlateBlue);
            _savingsProgressBar = CreateProgressBar();
            layout.Controls.Add(CreateSummaryRow(_savingsLabel, _savingsProgressBar), 0, 3);

            Controls.Add(layout);
        }

        private static TableLayoutPanel CreateSummaryRow(Label label, ProgressBar progressBar)
        {
            TableLayoutPanel row = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(0, 4, 0, 4)
            };
            row.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            row.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            row.Controls.Add(label, 0, 0);
            row.Controls.Add(progressBar, 0, 1);
            return row;
        }

        private static Label CreateSummaryLabel(Color color)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = color,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static ProgressBar CreateProgressBar()
        {
            return new ProgressBar
            {
                Dock = DockStyle.Fill,
                Maximum = 100,
                Style = ProgressBarStyle.Continuous,
                Margin = new Padding(0, 2, 0, 2)
            };
        }

        private static NumericUpDown CreateYearPicker()
        {
            return new NumericUpDown
            {
                Minimum = 2000,
                Maximum = 2100,
                Value = DateTime.Today.Year,
                Width = 70,
                Margin = new Padding(0, 0, 10, 0)
            };
        }
    }
}
