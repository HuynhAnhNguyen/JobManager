using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobManager.Areas.Admin.Pages.Home
{
    public class HomePageModel : PageModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly INotyfService _notyf;
        protected readonly ILogger<HomePageModel> _logger;
        protected readonly LanguageService _localization;

        [TempData]
        public string StatusMessage { get; set; }
        public HomePageModel(ApplicationDbContext context, INotyfService notyf, ILogger<HomePageModel> logger, LanguageService localization)
        {
            _context = context;
            _notyf = notyf;
            _logger = logger;
            _localization = localization;
        }
    }
}
