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
        public string Condition { get; set; } = string.Empty;
    }
}
