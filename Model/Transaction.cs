using System;

namespace MoneyFlow.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string TransactionType { get; set; }

        public int TransactionCategoryId { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public int SummaryId { get; set; }

        public int UserId { get; set; }

        public DateTime TransactionCreatedAt { get; set; }

        public DateTime TransactionUpdatedAt { get; set; }

        public string? TransactionDescription { get; set; }
    }
}