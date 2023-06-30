using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace JobManager.Areas.Admin.Pages.Job
{
    public class EditModel : JobPageModel
    {
        public EditModel(ApplicationDbContext context, INotyfService notyf, ILogger<JobPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        public const int ITEMS_PER_PAGE = 10;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPage { get; set; }

        public List<CongViec> congViecs { get; set; }
        public int soLuongCongViec { get; set; }

        public async Task<IActionResult> OnGetAsync(string Search)
        {
            soLuongCongViec = await _context.CongViec.CountAsync();
            if (soLuongCongViec > 0)
            {
                int totalCongViec = await _context.CongViec.CountAsync();

                countPage = (int)Math.Ceiling((double)totalCongViec / ITEMS_PER_PAGE);

                if (currentPage < 1)
                    currentPage = 1;
                if (currentPage > countPage)
                    currentPage = countPage;
                var qr = (from p in _context.CongViec orderby p.NgayTaoCongViec descending select p);

                if (!string.IsNullOrEmpty(Search))
                {
                    congViecs = await qr.Where(x => x.TenCongViec.Contains(Search)).Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToListAsync();
                }
                else
                {
                    congViecs = await qr.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToListAsync();
                }
            }
            return Page();
        }
    }
}
