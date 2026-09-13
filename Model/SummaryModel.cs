namespace MoneyFlow.Model
{
    public class SummaryModel
    {
        public int SummaryId { get; set; }
        public int UserId { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Savings => TotalIncome - TotalExpense;
    }
}
