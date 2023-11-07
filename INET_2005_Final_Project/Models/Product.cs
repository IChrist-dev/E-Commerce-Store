using System.ComponentModel;

namespace INET_2005_Final_Project.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; } = string.Empty;

        [DisplayName("Cover Art")]
        public string FileName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        
        [DisplayName("Media Type")]
        public bool isMovie { get; set; }

        [DisplayName("Runtime (m)")]
        public int Runtime { get; set; }
        public string Condition { get; set; } = string.Empty;
    }
}
