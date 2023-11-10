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
        private readonly INET_2005_Final_ProjectContext _context;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public LoginModel(INET_2005_Final_ProjectContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // Hard coded password and username check; should use full validation longterm
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
