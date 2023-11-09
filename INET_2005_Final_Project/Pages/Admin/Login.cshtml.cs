using INET_2005_Final_Project.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace INET_2005_Final_Project.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly INET_2005_Final_ProjectContext _context;

        [BindProperty]
        public string InputUsername { get; set; } = string.Empty;

        [BindProperty]
        public string InputPassword { get; set; } = string.Empty;

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
            if (InputUsername != null && InputPassword != null)
            {
                if (InputUsername.Equals(validUsername) && InputPassword.Equals(validPassword))
                {
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
            
            // Return to login page if ipnuts did not match
            return Page();
        }
    }
}
