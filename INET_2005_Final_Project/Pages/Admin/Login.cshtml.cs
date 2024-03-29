using INET_2005_Final_Project.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace INET_2005_Final_Project.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public string Username { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            _logger.Log(LogLevel.Information, "Login OnGet reached");

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            _logger.Log(LogLevel.Information, "Login OnPost reached");

            // Hard coded for app testing. TODO: Encrypt with BCrypt and move valid creds to table in DB
            string validUsername = "admin";
            string validPassword = "admin";

            // Compare inputs
            if (!Username.Equals(validUsername))
            {
                TempData["ErrorMessage"] = "Username not found";
                return Page();
            }

            if (!Password.Equals(validPassword))
            {
                TempData["ErrorMessage"] = "Incorrect password";
                return Page();
            }

            // Setup session
            var claims = new List<Claim>
                    {
                        new Claim("Fullname", "admin"),
                        new Claim(ClaimTypes.Role, "administrator")
                    };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());

            return RedirectToPage("/Products/Index");
        }
    }
}
