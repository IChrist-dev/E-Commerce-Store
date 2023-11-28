using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INET_2005_Final_Project.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string confirmMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            if (TempData.ContainsKey("ConfirmCode"))
            {
                confirmMessage = TempData["ConfirmCode"] as string;
            }

            return Page();
        }
    }
}
