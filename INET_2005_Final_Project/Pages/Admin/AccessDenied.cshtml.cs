using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INET_2005_Final_Project.Pages.Admin
{
    public class AccessDeniedModel : PageModel
    {
        private readonly ILogger<AccessDeniedModel> _logger;

        public AccessDeniedModel(ILogger<AccessDeniedModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            _logger.Log(LogLevel.Information, "Access Denied OnGet reached");

            return Page();
        }
    }
}
