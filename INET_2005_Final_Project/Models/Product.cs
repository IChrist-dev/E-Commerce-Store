namespace INET_2005_Final_Project.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Runtime { get; set; }
        public Condition Condition { get; set; } = new();
        public int ConditionId { get; set; }
    }
}
