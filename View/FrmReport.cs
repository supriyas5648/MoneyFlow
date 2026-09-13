using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        // Parameterless constructor for WinForms Designer in VS Code
        public FrmReport() : this(1)
        {
        }

        public FrmReport(int userId)
        {
            _currentUserId = userId;
            InitializeComponent();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            LoadTransactions();
            ApplyReportFilter();
        }

        private void LoadTransactions()
        {
            try
            {
                _transactions = _transactionService.GetAllTransactions(_currentUserId);
            }
            catch
            {
                _transactions = new List<TransactionModel>();
            }
        }

        private void FilterChanged(object? sender, EventArgs e)
        {
            ApplyReportFilter();
        }

        private void ApplyReportFilter()
        {
            DateTime selectedDate = _filterDatePicker.Value.Date;
            string selectedMode = _filterModeComboBox.SelectedItem?.ToString() ?? "Day";
            IEnumerable<TransactionModel> filteredTransactions = _transactions;
            string periodText;

            if (selectedMode == "Month")
            {
                filteredTransactions = filteredTransactions.Where(transaction =>
                    transaction.TransactionDate.Year == selectedDate.Year &&
                    transaction.TransactionDate.Month == selectedDate.Month);
                _filterDatePicker.CustomFormat = "MMMM yyyy";
                periodText = selectedDate.ToString("MMMM yyyy");
            }
            else if (selectedMode == "Year")
            {
                filteredTransactions = filteredTransactions.Where(transaction => transaction.TransactionDate.Year == selectedDate.Year);
                _filterDatePicker.CustomFormat = "yyyy";
                periodText = selectedDate.ToString("yyyy");
            }
            else
            {
                filteredTransactions = filteredTransactions.Where(transaction => transaction.TransactionDate.Date == selectedDate);
                _filterDatePicker.CustomFormat = "yyyy-MM-dd";
                periodText = selectedDate.ToString("yyyy-MM-dd");
            }

            List<TransactionModel> reportTransactions = filteredTransactions.OrderByDescending(transaction => transaction.TransactionDate).ToList();
            _periodLabel.Text = $"Showing: {periodText}";
            UpdateSummary(reportTransactions);
            PopulateReportListView(reportTransactions);
            _reportPictureBox.Invalidate();
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
            using Brush incomeBrush = new SolidBrush(Color.SeaGreen);
            using Brush expenseBrush = new SolidBrush(Color.Firebrick);
            using Brush balanceBrush = new SolidBrush(Color.DarkSlateBlue);

            e.Graphics.DrawString("Income / Expense / Balance", titleFont, textBrush, 16, 16);
            decimal maximum = Math.Max(Math.Max(_income, _expense), Math.Abs(_balance));
            int chartHeight = Math.Max(40, _reportPictureBox.ClientSize.Height - 105);
            int baseline = 60 + chartHeight;
            int barWidth = Math.Max(24, (_reportPictureBox.ClientSize.Width - 90) / 5);
            int firstX = 30;
            DrawBar(e.Graphics, incomeBrush, "Income", _income, maximum, firstX, baseline, barWidth, chartHeight, labelFont, textBrush);
            DrawBar(e.Graphics, expenseBrush, "Expense", _expense, maximum, firstX + barWidth + 20, baseline, barWidth, chartHeight, labelFont, textBrush);
            DrawBar(e.Graphics, balanceBrush, "Balance", Math.Abs(_balance), maximum, firstX + (barWidth + 20) * 2, baseline, barWidth, chartHeight, labelFont, textBrush);
        }

        private void DrawBar(Graphics graphics, Brush brush, string label, decimal value, decimal maximum, int x, int baseline, int width, int chartHeight, Font labelFont, Brush textBrush)
        {
            int height = maximum <= 0 ? 0 : (int)Math.Round(value / maximum * chartHeight);
            graphics.FillRectangle(brush, x, baseline - height, width, height);
            graphics.DrawString(label, labelFont, textBrush, x, baseline + 8);
            graphics.DrawString(value.ToString("N2"), labelFont, textBrush, x, Math.Max(65, baseline - height - 18));
        }
    }
}
