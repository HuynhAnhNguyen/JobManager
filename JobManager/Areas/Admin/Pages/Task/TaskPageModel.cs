using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Areas.Admin.Pages.Home;
using JobManager.Data;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobManager.Areas.Admin.Pages.Task
{
    public class TaskPageModel : PageModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly INotyfService _notyf;
        protected readonly ILogger<TaskPageModel> _logger;
        protected readonly LanguageService _localization;

        [TempData]
        public string StatusMessage { get; set; }
        public TaskPageModel(ApplicationDbContext context, INotyfService notyf, ILogger<TaskPageModel> logger, LanguageService localization)
        {
            _context = context;
            _notyf = notyf;
            _logger = logger;
            _localization = localization;
        }
    }
}
