using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;

namespace MoneyFlow
{
    public partial class FrmReport : Form
    {
        private readonly TransactionService _transactionService = new TransactionService();
        private readonly int _currentUserId;
        private List<TransactionModel> _transactions = new List<TransactionModel>();
        private decimal _income;
        private decimal _expense;
        private decimal _balance;
        private List<(string Label, decimal Income, decimal Expense)> _chartData = new List<(string, decimal, decimal)>();

        public FrmReport(int userId)
        {
            _currentUserId = userId;
            InitializeComponent();
            LoadTransactions();
            ConfigureFilterControls();
            ApplyReportFilter();
        }

        private void LoadTransactions()
        {
            try
            {
                _transactions = _transactionService.GetAllTransactions(_currentUserId);
            }
            catch (Exception exception)
            {
                _transactions = new List<TransactionModel>();
                MessageBox.Show($"Error loading report data:\n{exception.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterChanged(object? sender, EventArgs e)
        {
            ConfigureFilterControls();
            ApplyReportFilter();
        }

        private void ApplyReportFilter()
        {
            string selectedMode = _filterModeComboBox.SelectedItem?.ToString() ?? "Day";
            IEnumerable<TransactionModel> filteredTransactions = _transactions;
            string periodText;

            if (selectedMode == "Day")
            {
                DateTime selectedMonth = _filterDatePicker.Value;
                filteredTransactions = filteredTransactions.Where(transaction =>
                    transaction.TransactionDate.Year == selectedMonth.Year &&
                    transaction.TransactionDate.Month == selectedMonth.Month);
                periodText = selectedMonth.ToString("MMMM yyyy");
                _chartData = CreateChartData(filteredTransactions, selectedMode, selectedMonth.Year, selectedMonth.Month, 0, 0);
            }
            else if (selectedMode == "Month")
            {
                int selectedYear = _filterDatePicker.Value.Year;
                filteredTransactions = filteredTransactions.Where(transaction => transaction.TransactionDate.Year == selectedYear);
                periodText = selectedYear.ToString();
                _chartData = CreateChartData(filteredTransactions, selectedMode, selectedYear, 0, 0, 0);
            }
            else
            {
                int startYear = Math.Min((int)_startYearPicker.Value, (int)_endYearPicker.Value);
                int endYear = Math.Max((int)_startYearPicker.Value, (int)_endYearPicker.Value);
                filteredTransactions = filteredTransactions.Where(transaction =>
                    transaction.TransactionDate.Year >= startYear && transaction.TransactionDate.Year <= endYear);
                periodText = $"{startYear} - {endYear}";
                _chartData = CreateChartData(filteredTransactions, selectedMode, 0, 0, startYear, endYear);
            }

            List<TransactionModel> reportTransactions = filteredTransactions.OrderByDescending(transaction => transaction.TransactionDate).ToList();
            _periodLabel.Text = $"Showing: {periodText}";
            UpdateSummary(reportTransactions);
            PopulateReportListView(reportTransactions);
            _reportPictureBox.Invalidate();
        }

        private void ConfigureFilterControls()
        {
            string selectedMode = _filterModeComboBox.SelectedItem?.ToString() ?? "Day";
            bool isDay = selectedMode == "Day";
            bool isMonth = selectedMode == "Month";
            _filterDateLabel.Text = isDay ? "Month:" : "Year:";
            _filterDatePicker.CustomFormat = isDay ? "MMMM yyyy" : "yyyy";
            _filterDatePicker.Visible = isDay || isMonth;
            _startYearLabel.Visible = !isDay && !isMonth;
            _startYearPicker.Visible = !isDay && !isMonth;
            _endYearLabel.Visible = !isDay && !isMonth;
            _endYearPicker.Visible = !isDay && !isMonth;
        }

        private List<(string Label, decimal Income, decimal Expense)> CreateChartData(
            IEnumerable<TransactionModel> transactions,
            string mode,
            int selectedYear,
            int selectedMonth,
            int startYear,
            int endYear)
        {
            int count = mode == "Day"
                ? DateTime.DaysInMonth(selectedYear, selectedMonth)
                : mode == "Month" ? 12 : endYear - startYear + 1;
            List<(string Label, decimal Income, decimal Expense)> points = new List<(string, decimal, decimal)>();

            for (int index = 0; index < count; index++)
            {
                int year = mode == "Year" ? startYear + index : selectedYear;
                int month = mode == "Day" ? selectedMonth : mode == "Month" ? index + 1 : 0;
                IEnumerable<TransactionModel> periodTransactions = transactions.Where(transaction =>
                    mode == "Day"
                        ? transaction.TransactionDate.Day == index + 1
                        : mode == "Month"
                            ? transaction.TransactionDate.Month == month
                            : transaction.TransactionDate.Year == year);
                decimal income = periodTransactions.Where(transaction => transaction.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase)).Sum(transaction => transaction.TransactionAmount);
                decimal expense = periodTransactions.Where(transaction => transaction.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase)).Sum(transaction => transaction.TransactionAmount);
                string label = mode == "Day" ? (index + 1).ToString() : mode == "Month" ? new DateTime(selectedYear, month, 1).ToString("MMM") : year.ToString();
                points.Add((label, income, expense));
            }

            return points;
        }

        private void UpdateSummary(List<TransactionModel> reportTransactions)
        {
            _income = reportTransactions.Where(transaction => transaction.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase)).Sum(transaction => transaction.TransactionAmount);
            _expense = reportTransactions.Where(transaction => transaction.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase)).Sum(transaction => transaction.TransactionAmount);
            _balance = _income - _expense;
            decimal maximum = Math.Max(Math.Max(_income, _expense), Math.Abs(_balance));

            _incomeLabel.Text = $"Total Income\n{_income:N2}";
            _expenseLabel.Text = $"Total Expense\n{_expense:N2}";
            _balanceLabel.Text = $"Balance\n{_balance:N2}";
            _incomeProgressBar.Value = GetProgressValue(_income, maximum);
            _expenseProgressBar.Value = GetProgressValue(_expense, maximum);
            _balanceProgressBar.Value = GetProgressValue(Math.Abs(_balance), maximum);
        }

        private int GetProgressValue(decimal value, decimal maximum)
        {
            return maximum <= 0 ? 0 : Math.Min(100, (int)Math.Round(value / maximum * 100));
        }

        private void PopulateReportListView(List<TransactionModel> reportTransactions)
        {
            _reportListView.Items.Clear();
            foreach (TransactionModel transaction in reportTransactions)
            {
                _reportListView.Items.Add(new ListViewItem(new[]
                {
                    transaction.TransactionId.ToString(),
                    transaction.TransactionDate.ToString("yyyy-MM-dd"),
                    transaction.CategoryName,
                    transaction.TransactionDescription ?? string.Empty,
                    transaction.TransactionType,
                    transaction.TransactionAmount.ToString("N2")
                }));
            }
        }

        private void ReportPictureBoxPaint(object? sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            using Font titleFont = new Font(Font, FontStyle.Bold);
            using Font labelFont = new Font(Font.FontFamily, 8F);
            using Brush textBrush = new SolidBrush(Color.FromArgb(45, 45, 45));
            using Pen savingsPen = new Pen(Color.DarkSlateBlue, 2F);

            e.Graphics.DrawString("Savings Trend", titleFont, textBrush, 16, 16);
            DrawLine(e.Graphics, savingsPen, "Savings", point => point.Income - point.Expense, Color.DarkSlateBlue, labelFont, textBrush, true);
        }

        private void DrawLine(Graphics graphics, Pen pen, string legendText, Func<(string Label, decimal Income, decimal Expense), decimal> valueSelector, Color color, Font labelFont, Brush textBrush, bool drawXAxisLabels)
        {
            if (_chartData.Count == 0) return;
            int left = 70, top = 58, right = 18, bottom = 42;
            int width = Math.Max(1, _reportPictureBox.ClientSize.Width - left - right);
            int height = Math.Max(1, _reportPictureBox.ClientSize.Height - top - bottom);
            decimal minimum = Math.Min(0m, _chartData.Min(value => value.Income - value.Expense));
            decimal maximum = Math.Max(1m, _chartData.Max(value => value.Income - value.Expense));
            decimal range = Math.Max(1m, maximum - minimum);

            using Pen axisPen = new Pen(Color.FromArgb(80, 80, 80), 1F);
            using Pen gridPen = new Pen(Color.FromArgb(225, 225, 225), 1F);
            graphics.DrawLine(axisPen, left, top, left, top + height);
            graphics.DrawLine(axisPen, left, top + height, left + width, top + height);
            graphics.DrawString("Amount", labelFont, textBrush, 4, top + height / 2 - 8);

            for (int tick = 0; tick <= 5; tick++)
            {
                decimal amount = minimum + range * tick / 5m;
                float y = top + height - height * tick / 5f;
                if (tick > 0 && tick < 5)
                {
                    graphics.DrawLine(gridPen, left, y, left + width, y);
                }

                string amountLabel = amount.ToString("N0");
                SizeF labelSize = graphics.MeasureString(amountLabel, labelFont);
                graphics.DrawString(amountLabel, labelFont, textBrush, left - labelSize.Width - 6, y - labelSize.Height / 2);
            }

            PointF[] points = new PointF[_chartData.Count];
            for (int index = 0; index < _chartData.Count; index++)
            {
                decimal value = valueSelector(_chartData[index]);
                float x = left + (width * index / (float)Math.Max(1, _chartData.Count - 1));
                float y = top + height - (float)((value - minimum) / range * height);
                points[index] = new PointF(x, y);
                using Brush pointBrush = new SolidBrush(color);
                graphics.FillEllipse(pointBrush, x - 3, y - 3, 6, 6);
            }
            if (points.Length > 1)
            {
                graphics.DrawLines(pen, points);
            }

            if (drawXAxisLabels)
            {
                for (int index = 0; index < _chartData.Count; index++)
                {
                    float x = left + (width * index / (float)Math.Max(1, _chartData.Count - 1));
                    SizeF labelSize = graphics.MeasureString(_chartData[index].Label, labelFont);
                    graphics.DrawString(_chartData[index].Label, labelFont, textBrush, x - labelSize.Width / 2, top + height + 5);
                }
            }

            using Brush legendBrush = new SolidBrush(color);
            float legendX = left;
            graphics.FillRectangle(legendBrush, legendX, top - 22, 10, 10);
            graphics.DrawString(legendText, labelFont, textBrush, legendX + 14, top - 24);
        }
    }
}
