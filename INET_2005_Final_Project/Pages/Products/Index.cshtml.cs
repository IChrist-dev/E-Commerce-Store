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
    public class IndexModel : PageModel
    {
        private readonly INET_2005_Final_ProjectContext _context;

        public IList<Product> Product { get; set; } = default!;

        public IndexModel(INET_2005_Final_ProjectContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                Product = await _context.Product.ToListAsync();
            }
        }
    }
}
