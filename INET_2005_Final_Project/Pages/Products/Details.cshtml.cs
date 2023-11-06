using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;

namespace INET_2005_Final_Project.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly INET_2005_Final_Project.Data.INET_2005_Final_ProjectContext _context;

        public DetailsModel(INET_2005_Final_Project.Data.INET_2005_Final_ProjectContext context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
    }
}
