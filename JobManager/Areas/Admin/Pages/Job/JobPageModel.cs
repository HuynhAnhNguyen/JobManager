using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Areas.Admin.Pages.Home;
using JobManager.Data;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobManager.Areas.Admin.Pages.Job
{
    public class JobPageModel : PageModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly INotyfService _notyf;
        protected readonly ILogger<JobPageModel> _logger;
        protected readonly LanguageService _localization;

        [TempData]
        public string StatusMessage { get; set; }
        public JobPageModel(ApplicationDbContext context, INotyfService notyf, ILogger<JobPageModel> logger, LanguageService localization)
        {
            _context = context;
            _notyf = notyf;
            _logger = logger;
            _localization = localization;
        }
    }
}
