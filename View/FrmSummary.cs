using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MoneyFlow.Model;
using MoneyFlow.Service;

namespace MoneyFlow
{
    public partial class FrmSummary : Form
    {
        private readonly int _userId;
        private readonly SummaryService _summaryService = new SummaryService();
        private readonly TransactionService _transactionService = new TransactionService();
        private readonly List<TransactionModel> _transactions = new List<TransactionModel>();

        public FrmSummary(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadSummary();
        }

        private void LoadSummary()
        {
            try
            {
                SummaryModel? summary = _summaryService.GetSummary(_userId);
                _transactions.Clear();
                _transactions.AddRange(_transactionService.GetAllTransactions(_userId));
                ApplyFilter(summary);
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    "Unable to load the summary.\n\n" + exception.Message,
                    "Summary",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FilterChanged(object? sender, EventArgs e)
        {
            ConfigureFilterControls();
            ApplyFilter(null);
        }

        private void ConfigureFilterControls()
        {
            string mode = _filterModeComboBox.SelectedItem?.ToString() ?? "Day";
            bool isDay = mode == "Day";
            bool isMonth = mode == "Month";
            _filterDateLabel.Text = isDay ? "Day:" : isMonth ? "Month:" : "Year:";
            _filterDatePicker.CustomFormat = isDay
                ? "MMMM dd, yyyy"
                : isMonth ? "MMMM yyyy" : "yyyy";
            _filterDatePicker.Visible = true;
            _startYearLabel.Visible = false;
            _startYearPicker.Visible = false;
            _endYearLabel.Visible = false;
            _endYearPicker.Visible = false;
        }

        private void ApplyFilter(SummaryModel? allTimeSummary)
        {
            string mode = _filterModeComboBox.SelectedItem?.ToString() ?? "Day";
            IEnumerable<TransactionModel> filtered = _transactions;
            string periodText;

            if (mode == "Day")
            {
                DateTime selectedDay = _filterDatePicker.Value.Date;
                filtered = filtered.Where(transaction =>
                    transaction.TransactionDate.Date == selectedDay);
                periodText = selectedDay.ToString("MMMM dd, yyyy");
            }
            else if (mode == "Month")
            {
                DateTime selectedMonth = _filterDatePicker.Value;
                filtered = filtered.Where(transaction =>
                    transaction.TransactionDate.Year == selectedMonth.Year &&
                    transaction.TransactionDate.Month == selectedMonth.Month);
                periodText = selectedMonth.ToString("MMMM yyyy");
            }
            else
            {
                int selectedYear = _filterDatePicker.Value.Year;
                filtered = filtered.Where(transaction => transaction.TransactionDate.Year == selectedYear);
                periodText = selectedYear.ToString();
            }

            List<TransactionModel> selectedTransactions = filtered.ToList();
            decimal income = selectedTransactions
                .Where(transaction => transaction.TransactionType.Equals("Income", StringComparison.OrdinalIgnoreCase))
                .Sum(transaction => transaction.TransactionAmount);
            decimal expense = selectedTransactions
                .Where(transaction => transaction.TransactionType.Equals("Expense", StringComparison.OrdinalIgnoreCase))
                .Sum(transaction => transaction.TransactionAmount);

            if (allTimeSummary != null && mode == "All")
            {
                income = allTimeSummary.TotalIncome;
                expense = allTimeSummary.TotalExpense;
            }

            _periodLabel.Text = $"Showing: {periodText}";
            SetSummaryValues(income, expense, income - expense);
        }

        private void SetSummaryValues(decimal income, decimal expense, decimal savings)
        {
            decimal maximum = Math.Max(Math.Max(income, expense), Math.Abs(savings));
            _incomeLabel.Text = $"Total Income: {income:N2}";
            _expenseLabel.Text = $"Total Expense: {expense:N2}";
            _savingsLabel.Text = $"Savings: {savings:N2}";
            _incomeProgressBar.Value = GetProgressValue(income, maximum);
            _expenseProgressBar.Value = GetProgressValue(expense, maximum);
            _savingsProgressBar.Value = GetProgressValue(Math.Abs(savings), maximum);
        }

        private static int GetProgressValue(decimal value, decimal maximum)
        {
            return maximum <= 0m
                ? 0
                : Math.Min(100, (int)Math.Round(value / maximum * 100m));
        }

    }
}
