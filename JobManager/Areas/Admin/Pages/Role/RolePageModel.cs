using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobManager.Areas.Admin.Pages.Role
{
    public class RolePageModel : PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly ApplicationDbContext _context;
        protected readonly INotyfService _notyf;
        protected readonly LanguageService _localization;


        [TempData]
        public string StatusMessage { get; set; }

        public RolePageModel(RoleManager<IdentityRole> roleManager, ApplicationDbContext context, INotyfService notyf, LanguageService localization)
        {
            _roleManager = roleManager;
            _context = context;
            _notyf = notyf;
            _localization = localization;
        }
    }
}
