using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INET_2005_Final_Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INET_2005_Final_ProjectContext _context;

        public IList<Product> productsList { get; set; } = default!;
        public IList<Product> moviesList { get; set; } = default!;
        public IList<Product> albumsList { get; set; } = default!;
        public Product randProduct { get; set; } = default!;
        public IndexModel(ILogger<IndexModel> logger, INET_2005_Final_ProjectContext context)
        {
            _logger = logger;
            _context = context;

            moviesList = new List<Product>();
            albumsList = new List<Product>();
        }

        public void OnGet()
        {
            // Populate list of products from DB for homepage display
            if (_context.Product != null)
            {
                // Save products to memory
                productsList = _context.Product.OrderByDescending(p => p.ProductId).ToList();

                // Get a random product for feature display
                Random random = new Random();
                randProduct = productsList[random.Next(productsList.Count)];
                // Remove randProduct from the list so there's no duplicate
                productsList.Remove(randProduct);

                // Split products into movie list and album list for display
                
                foreach (var product in productsList)
                {
                    if (product.isMovie)
                    {
                        moviesList.Add(product);
                    }
                    else
                    {
                        albumsList.Add(product);
                    }
                }
            }
        }
    }
}