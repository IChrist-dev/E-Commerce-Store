using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;

namespace INET_2005_Final_Project.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly INET_2005_Final_ProjectContext _context;
        IWebHostEnvironment _env;

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImageUpload { get; set; } = null!;

        public List<string> ConditionList { get; set; } = new();

        public List<SelectListItem> SelectConditionList { get; set; } = new();

        public EditModel(INET_2005_Final_ProjectContext context, IWebHostEnvironment env)
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product =  await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Convert Condition from index to simple string value
            int selectedConditionIndex = int.Parse(Product.Condition);
            Product.Condition = ConditionList.ElementAt(selectedConditionIndex);


            // Replace fileName in Product if a new image was provided
            if (ImageUpload != null)
            {
                // Make a unique image name and set for product
                string imageName = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss-") + ImageUpload.FileName;

                Product.FileName = imageName;

                _context.Attach(Product).State = EntityState.Modified;

                // Save changes to DB
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(Product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Assuming successful DB insert, save image file to public folder
                string file = Path.Combine(_env.ContentRootPath, "wwwroot\\photos\\", imageName);

                using (FileStream fs = new FileStream(file, FileMode.Create))
                {
                    ImageUpload.CopyTo(fs);
                }

                return RedirectToPage("./Index");
            }
            else
            {
                _context.Attach(Product).State = EntityState.Modified;

                // Save changes to DB
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(Product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToPage("./Index");
            }          
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
