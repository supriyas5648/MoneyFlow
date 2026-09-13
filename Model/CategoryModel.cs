namespace MoneyFlow.Model
{
    /// <summary>
    /// Represents a category from the t_category table.
    /// </summary>
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryType { get; set; } = string.Empty; // "Income" or "Expense"
        public int? CreatedByUserId { get; set; }
    }
}
