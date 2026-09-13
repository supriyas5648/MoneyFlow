using System;

namespace MoneyFlow.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public int TransactionCategoryId { get; set; }

        public decimal TransactionAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public int SummaryId { get; set; }

        public int UserId { get; set; }

        public DateTime TransactionCreatedAt { get; set; }

        public DateTime TransactionUpdatedAt { get; set; }

        public string? TransactionDescription { get; set; }
        public int UserTransactionNo { get; set; }
        public int DisplayId => UserTransactionNo > 0 ? UserTransactionNo : TransactionId;
    }
}