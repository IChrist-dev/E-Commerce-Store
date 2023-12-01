using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace INET_2005_Final_Project.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INET_2005_Final_ProjectContext _context;

        public IList<Product> Product { get; set; } = default!;
        public List<string> ConditionList { get; set; } = new();

        public IndexModel(INET_2005_Final_ProjectContext context, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _context = context;

            // Hard-coded condition names cannot be modified
            ConditionList.Add("Like New");
            ConditionList.Add("Good");
            ConditionList.Add("Fair");
            ConditionList.Add("Some Wear");
            ConditionList.Add("Poor");
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            _logger.Log(LogLevel.Information, "Products Index OnGet reached");

            if (_context.Product != null)
            {
                Product = await _context.Product.ToListAsync();
            }
        }
    }
}
