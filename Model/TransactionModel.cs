using System;

namespace MoneyFlow.Model
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? TransactionDescription { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int TransactionCategoryId { get; set; }
        public int UserId { get; set; }
    }
}
