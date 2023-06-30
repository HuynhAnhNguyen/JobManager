using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Areas.Admin.Pages.Home;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobManager.Areas.Admin.Pages.User
{
    public class UserPageModel : PageModel
    {
        protected readonly UserManager<NguoiDung> _userManager;
        protected readonly SignInManager<NguoiDung> _signInManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly ApplicationDbContext _context;
        protected readonly INotyfService _notyf;
        protected readonly ILogger<UserPageModel> _logger;
        protected readonly LanguageService _localization;
        protected readonly IEmailSender _emailSender;

        [TempData]
        public string StatusMessage { get; set; }
        public UserPageModel(UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, INotyfService notyf, ILogger<UserPageModel> logger, LanguageService localization, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _notyf = notyf;
            _logger = logger;
            _localization = localization;
            _emailSender = emailSender;
        }
    }
}
