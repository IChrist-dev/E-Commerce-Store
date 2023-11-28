using INET_2005_Final_Project.Data;
using INET_2005_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Razor;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace INET_2005_Final_Project.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ILogger<CheckoutModel> _logger;
        private readonly INET_2005_Final_ProjectContext _context;

        [BindProperty]
        public APIContent apiContent { get; set; } = default!;

        public string validationMessage { get; set; } = string.Empty;
        public CheckoutModel(ILogger<CheckoutModel> logger, INET_2005_Final_ProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? cart = Request.Cookies["ProductCart"];

            if (cart.IsNullOrEmpty())
            {
                validationMessage = "Your shopping cart is empty. Please select products from the home page.";
                
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the list of products from cookie
            string? cart = Request.Cookies["ProductCart"];
            apiContent.products = Request.Cookies["ProductCart"]!;

            // Validate apiContent data
            if (apiContent.ccNumber.ToString().Length != 16)
            {
                validationMessage = "Your credit card must contain 16 digits";
                return Page();
            }

            Regex ccExpiryRegex = new Regex(@"^\d{4}$");
            if (!ccExpiryRegex.IsMatch(apiContent.ccExpiryDate))
            {
                validationMessage = "Your credit card expiration must be in the format [MMYY]";
                return Page();
            }

            if (apiContent.cvv.ToString().Length != 3)
            {
                validationMessage = "Your CVV must be 3 digits long";
                return Page();
            }

            string apiURL = "https://nscc-inet2005-purchase-api.azurewebsites.net/purchase";
            HttpClient client = new HttpClient();

            string dataAsJson = JsonSerializer.Serialize(apiContent);
            var callContent = new StringContent(dataAsJson, Encoding.UTF8, "application/json");

            // Make API call
            var response = await client.PostAsync(apiURL, callContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            
            // If reached, successful API response
            // Cookie can be deleted by expiring prematurely
            Response.Cookies.Append("ProductCart", "", new CookieOptions
            {
                Expires = DateTime.Now.AddHours(-1)
            });

            // Save the response code to make accessible on Confirmation page
            TempData["ConfirmCode"] = responseContent;

            return RedirectToPage("/Confirmation");
        }
    }
}
