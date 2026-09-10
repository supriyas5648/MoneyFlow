using System;

namespace MoneyFlow.Model
{
    /// <summary>
    /// Represents a transaction from t_transaction joined with t_category.
    /// </summary>
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; } = string.Empty; // "Income" or "Expense"
        public int TransactionCategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal TransactionAmount { get; set; }
        public string? TransactionDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? SummaryId { get; set; }
        public int UserId { get; set; }
    }
}
