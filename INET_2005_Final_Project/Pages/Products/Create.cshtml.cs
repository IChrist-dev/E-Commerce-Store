using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace INET_2005_Final_Project.Pages.Products
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly INET_2005_Final_ProjectContext _context;
        IWebHostEnvironment _env;

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageUpload { get; set; } = null!;

        public List<string> ConditionList { get; set; } = new();

        public List<SelectListItem> SelectConditionList { get; set; } = new();

        public CreateModel(INET_2005_Final_ProjectContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

            // Hard-coded condition names cannot be modified
            ConditionList.Add("Like New");
            ConditionList.Add("Good");
            ConditionList.Add("Fair");
            ConditionList.Add("Some Wear");
            ConditionList.Add("Poor");

            for (int i = 0; i < ConditionList.Count; i++)
            {
                SelectConditionList.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = ConditionList[i]
                });
            }
        }

        public IActionResult OnGet()
        {
            return Page();
        }
       

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // Make a unique image name and set for product
            string imageName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss-") + ImageUpload.FileName;

            Product.FileName = imageName;

            // Validate model
            if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }

            // Save to database
            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            // Assuming successful DB insert, save image file to public folder
            string file = Path.Combine(_env.ContentRootPath, "wwwroot\\photos\\", imageName);

            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                ImageUpload.CopyTo(fs);
            }

            return RedirectToPage("./Index");
        }
    }
}
