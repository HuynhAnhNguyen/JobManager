using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobManager.Areas.Admin.Pages.Task
{
    public class IndexModel : TaskPageModel
    {
        public IndexModel(ApplicationDbContext context, INotyfService notyf, ILogger<TaskPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        public const int ITEMS_PER_PAGE = 10;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPage { get; set; }

        public List<DuAn> duAns { get; set; }
        public int soLuongDuAn { get; set; }

        public async Task<IActionResult> OnGetAsync(string Search)
        {
            soLuongDuAn = await _context.DuAn.Where(x => x.Deleted == false).CountAsync();
            if (soLuongDuAn > 0)
            {
                countPage = (int)Math.Ceiling((double)soLuongDuAn / ITEMS_PER_PAGE);

                if (currentPage < 1)
                    currentPage = 1;
                if (currentPage > countPage)
                    currentPage = countPage;
                var qr = (from p in _context.DuAn where p.Deleted == false orderby p.NgayTaoDuAn descending select p);

                if (!string.IsNullOrEmpty(Search))
                {
                    duAns = await qr.Where(x => x.TenDuAn.Contains(Search)).Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToListAsync();
                }
                else
                {
                    duAns = await qr.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToListAsync();
                }
            }
            return Page();
        }

        public void OnPost() => RedirectToPage("./Index");
    }
}
