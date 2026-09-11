namespace MoneyFlow.Model
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryType { get; set; }

        public int? CreatedByUserId { get; set; }
    }
}