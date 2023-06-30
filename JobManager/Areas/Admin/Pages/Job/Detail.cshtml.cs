using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace JobManager.Areas.Admin.Pages.Job
{
    public class DetailModel : JobPageModel
    {
        public DetailModel(ApplicationDbContext context, INotyfService notyf, ILogger<JobPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        public CongViec congViec { get; set; }

        public string duAn { get; set; }
        public string trangThai { get; set; }
        public string uuTien { get; set; }
        public async Task<IActionResult> OnGetAsync(string jobid)
        {
            if (jobid == null)
            {
                _notyf.Error("Không tìm thấy công việc có mã "+ jobid, 3);
                return RedirectToPage("./Index");
            }

            congViec = await _context.CongViec.Where(x => x.MaCongViec == jobid).FirstOrDefaultAsync();

            if (congViec == null)
            {
                _notyf.Error("Không tìm thấy công việc có mã " + jobid, 3);
                return RedirectToPage("./Index");
            }

            duAn = await (from cv in _context.CongViec join da in _context.DuAn on cv.MaDuAn equals da.MaDuAn where cv.MaCongViec == jobid select da.TenDuAn).FirstOrDefaultAsync();

            if (congViec.TrangThai.Equals(-1))
            {
                trangThai = "Quá hạn";
            }
            else if (congViec.TrangThai.Equals(0))
            {
                trangThai = "Chưa bắt đầu";
            }
            else if (congViec.TrangThai.Equals(1))
            {
                trangThai = "Đang thực hiện";
            }
            else
                trangThai = "Đã kết thúc";


            if (congViec.UuTien.Equals(-1))
            {
                uuTien = "Không áp dụng";
            }
            else if (congViec.UuTien.Equals(0))
            {
                uuTien = "Thấp";
            }
            else if (congViec.UuTien.Equals(1))
            {
                uuTien = "Trung bình";
            }
            else
                uuTien = "Cao";

            return Page();
        }

        public IActionResult OnPost() => RedirectToPage();
    }
}
