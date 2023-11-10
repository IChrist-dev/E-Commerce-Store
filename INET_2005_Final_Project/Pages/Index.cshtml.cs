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

        public IndexModel(ILogger<IndexModel> logger, INET_2005_Final_ProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            // Populate list of products from DB for homepage display
            if (_context.Product != null)
            {
                productsList = _context.Product.OrderByDescending(p => p.ProductId).ToList();
            }
        }
    }
}