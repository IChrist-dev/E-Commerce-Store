using INET_2005_Final_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Razor;

namespace INET_2005_Final_Project.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ILogger<CheckoutModel> _logger;
        private readonly INET_2005_Final_ProjectContext _context;

        [BindProperty]
        public string name { get; set; } = string.Empty;

        [BindProperty]
        public string address { get; set; } = string.Empty;

        [BindProperty]
        public string city { get; set; } = string.Empty;

        [BindProperty]
        public string province { get; set; } = string.Empty;

        [BindProperty]
        public string postalCode{ get; set; } = string.Empty;

        [BindProperty]
        public string country { get; set; } = string.Empty;

        [BindProperty]
        public long ccNumber { get; set; }

        [BindProperty]
        public string ccExpiryDate { get; set; } = string.Empty;

        [BindProperty]
        public int cvv { get; set; }

        public string productsList { get; set; } = string.Empty;

        public CheckoutModel(ILogger<CheckoutModel> logger, INET_2005_Final_ProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            productsList = Request.Cookies["ProductCart"];

            string apiURL = "https://nscc-inet2005-purchase-api.azurewebsites.net/purchase";

            HttpClient client = new HttpClient();

            // This looks promising: https://learn.microsoft.com/en-us/answers/questions/1180617/send-a-request-to-a-restapi-with-json

            // For debugging
            //_logger.Log(LogLevel.Information, "Checkout content:\n" +
            //    name + "\n" +
            //    address + "\n" +
            //    city + "\n" +
            //    province + "\n" +
            //    postalCode + "\n" +
            //    country + "\n" +
            //    ccNumber + "\n" +
            //    ccExpiryDate + "\n" +
            //    cvv + "\n" +
            //    productsList);

            return RedirectToPage("/Confirmation");
        }
    }
}
