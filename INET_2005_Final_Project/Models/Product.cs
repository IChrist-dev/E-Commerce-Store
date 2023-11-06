namespace INET_2005_Final_Project.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool isMovie { get; set; }
        public int Runtime { get; set; }
        public string Condition { get; set; } = string.Empty;
    }
}
