namespace MoneyFlow.Model
{
    /// <summary>
    /// Represents the financial summary from t_summary table.
    /// </summary>
    public class SummaryModel
    {
        public int SummaryId { get; set; }
        public int UserId { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }

        /// <summary>
        /// Computed property: TotalIncome - TotalExpense.
        /// </summary>
        public decimal Savings => TotalIncome - TotalExpense;
    }
}
