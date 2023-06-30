using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobManager.Areas.Admin.Pages.Task
{
    public class DetailModel : TaskPageModel
    {
        public DetailModel(ApplicationDbContext context, INotyfService notyf, ILogger<TaskPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }
        public DuAn duAn { get; set; }
        public string trangThai { get; set; }
        public async Task<IActionResult> OnGetAsync(string taskid)
        {
            if (taskid == null)
            {
                _notyf.Error("Không tìm thấy dự án có mã " + taskid, 3);
                return RedirectToPage("./Index");
            }

            duAn = await _context.DuAn.Where(x => x.MaDuAn == taskid).FirstOrDefaultAsync();

            if (duAn == null)
            {
                _notyf.Error("Không tìm thấy dự án có mã " + taskid, 3);
                return RedirectToPage("./Index");
            }

            if (duAn.TrangThai.Equals(-1))
            {
                trangThai = "Quá hạn";
            }
            else if (duAn.TrangThai.Equals(0))
            {
                trangThai = "Chưa bắt đầu";
            }
            else if (duAn.TrangThai.Equals(1))
            {
                trangThai = "Đang thực hiện";
            }
            else
                trangThai = "Đã kết thúc";

            return Page();
        }

        public IActionResult OnPost() => RedirectToPage();
    }
}
