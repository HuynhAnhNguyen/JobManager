using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace JobManager.Areas.Admin.Pages.Home
{
    public class IndexModel : HomePageModel
    {
        public IndexModel(ApplicationDbContext context, INotyfService notyf, ILogger<HomePageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        public int totalUsers { get; set; }

        public int totalTasks { get; set; }

        public int totalJobs { get; set; }

        public int totalRoles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            totalUsers = await _context.NguoiDung.CountAsync();
            totalTasks = await _context.DuAn.CountAsync();
            totalJobs = await _context.CongViec.CountAsync();
            totalRoles= await _context.Roles.CountAsync();
            return Page();
        }

        public void OnPost() => RedirectToPage("./Index");
        //public void OnPost() => RedirectToPage();
    }
}
