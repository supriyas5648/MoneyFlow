namespace MoneyFlow.Model
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryType { get; set; } = string.Empty;
        public int? CreatedByUserId { get; set; }
    }
}
