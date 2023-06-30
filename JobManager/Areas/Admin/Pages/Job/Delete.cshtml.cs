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
    public class DeleteModel : JobPageModel
    {
        public DeleteModel(ApplicationDbContext context, INotyfService notyf, ILogger<JobPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }
        public CongViec congViec { get; set; }
        public IActionResult OnGet() => RedirectToPage("./Index");

        public async Task<IActionResult> OnPostAsync(string jobid)
        {
            if (jobid == null)
            {
                _notyf.Error("Không tìm thấy công việc có mã " + jobid, 3);
                return RedirectToPage("./Index");
            }

            congViec = await _context.CongViec.Where(x => x.MaCongViec == jobid).FirstOrDefaultAsync();

            if (congViec == null)
            {
                _notyf.Error("Không tìm thấy công việc có mã " + jobid, 3);
                return RedirectToPage("./Index");
            }

            _context.Update(congViec);
            congViec.Deleted = true;
            await _context.SaveChangesAsync();

            _notyf.Success("Xóa công việc thành công", 3);

            return RedirectToPage("./Index");


        }
    }
}
