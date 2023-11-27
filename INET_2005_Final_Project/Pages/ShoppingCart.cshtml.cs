using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace INET_2005_Final_Project.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly ILogger<ShoppingCartModel> _logger;
        private readonly INET_2005_Final_ProjectContext _context;
     
        public IList<Product> productCart { get; set; } = default!;
        // Dictionary stores each product ID and how many times it has appeared
        public Dictionary<int, int> productQuantities { get; set; } = new Dictionary<int, int>();
        public bool emptyCart { get; set; } = false;

        public ShoppingCartModel(ILogger<ShoppingCartModel> logger, INET_2005_Final_ProjectContext context)
        {
            _logger = logger;
            _context = context;
            productCart = new List<Product>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string cartList = Request.Cookies["ProductCart"];

            if (cartList != null) {
                // Delimit ProductCart cookie string
                string[] cartListSplit = cartList.Split(',');
                
                // Build Product list based on db returns of cookie IDs
                foreach (var id in cartListSplit)
                {
                    int productId = int.Parse(id);

                    Product product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == productId);

                    // Increment a pre-existing product ID count
                    if (productQuantities.ContainsKey(productId))
                    {
                        productQuantities[productId]++;
                    }
                    else
                    {
                        // First occurence of given ID
                        productQuantities.Add(productId, 1);
                    }

                    if (product != null)
                    {
                        // Avoid duplicates
                        if (!productCart.Contains(product))
                        {
                            productCart.Add(product);
                        }                        
                    }
                }
            }
            else
            {
                emptyCart = true;
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            // Stubbed until sprint 4
            return RedirectToPage("/Checkout");
        }
    }
}
