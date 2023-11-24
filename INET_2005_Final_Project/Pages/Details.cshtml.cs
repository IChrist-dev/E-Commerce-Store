using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace INET_2005_Final_Project.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private readonly INET_2005_Final_ProjectContext _context;

        [BindProperty]
        public Product Product { get; set; } = default!;

        public List<string> ConditionList { get; set; } = new();

        public DetailsModel(ILogger<DetailsModel> logger, INET_2005_Final_ProjectContext context)
        {
            _logger = logger;
            _context = context;

            // Hard-coded condition names cannot be modified
            ConditionList.Add("Like New");
            ConditionList.Add("Good");
            ConditionList.Add("Fair");
            ConditionList.Add("Some Wear");
            ConditionList.Add("Poor");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            Product dbProduct = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == id);
            if (dbProduct == null)
            {
                return NotFound();
            }
            else
            {
                Product = dbProduct;
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            // Adding to Cookie
            string? cart = Request.Cookies["ProductCart"];

            if (string.IsNullOrEmpty(cart))
            {
                // Cart is empty, initialize it
                Response.Cookies.Append("ProductCart", Product.ProductId.ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(24)
                });
                
            }
            else
            {
                // Add product to existing cart and update expiration counter
                Response.Cookies.Append("ProductCart", cart + "," + Product.ProductId.ToString(), new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(24)
                });
            }

            return RedirectToPage("/Index");
        }
    }
}
