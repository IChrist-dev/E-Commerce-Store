using INET_2005_Final_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INET_2005_Final_Project.Pages
{
    public class ConfirmationModel : PageModel
    {
        private readonly ILogger<ConfirmationModel> _logger;

        public string confirmMessage { get; set; } = string.Empty;

        public ConfirmationModel(ILogger<ConfirmationModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            _logger.Log(LogLevel.Information, "Confirmation OnGet reached");

            if (TempData.ContainsKey("ConfirmCode"))
            {
                confirmMessage = TempData["ConfirmCode"] as string;
            }

            return Page();
        }
    }
}
