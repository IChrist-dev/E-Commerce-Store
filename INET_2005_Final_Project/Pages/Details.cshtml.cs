using INET_2005_Final_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INET_2005_Final_Project.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;
        private readonly INET_2005_Final_ProjectContext _context;

        public DetailsModel(ILogger<DetailsModel> logger, INET_2005_Final_ProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}
