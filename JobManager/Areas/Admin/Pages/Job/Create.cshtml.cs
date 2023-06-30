using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace JobManager.Areas.Admin.Pages.Job
{
    public class CreateModel : JobPageModel
    {
        public CreateModel(ApplicationDbContext context, INotyfService notyf, ILogger<JobPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Tên công việc là bắt buộc!")]
            [StringLength(255, MinimumLength = 2, ErrorMessage = "Tên công việc phải có độ dài từ 2 đến 255 ký tự!")]
            public string? TenCongViec { get; set; }

            [Required(ErrorMessage = "Mô tả công việc là bắt buộc!")]
            [StringLength(1000, MinimumLength = 2, ErrorMessage = "Mô tả công việc phải có độ dài từ 2 đến 1000 ký tự!")]
            public string? MoTaCongViec { get; set; }

            [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc!")]
            public DateTime? NgayBatDau { get; set; }

            [Required(ErrorMessage = "Ngày kết thúc là bắt buộc!")]
            public DateTime? NgayKetThuc { get; set; }

            [Required(ErrorMessage = "Trạng thái là bắt buộc!")]
            public int TrangThai { get; set; }

            [Required(ErrorMessage = "Mức độ ưu tiên là bắt buộc!")]
            public int MucDoUuTien { get; set; }

            public string? MaDuAn { get; set; }

        }

        public List<DuAn> duAns { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            duAns = await _context.DuAn.ToListAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            duAns = await _context.DuAn.ToListAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var maCongViec = "" + DateTime.Now.Ticks;

            CongViec congViec = new CongViec();
            congViec.MaCongViec= maCongViec;
            congViec.TenCongViec = Input.TenCongViec;
            congViec.MoTaCongViec = Input.MoTaCongViec;
            congViec.NgayBatDau = Input.NgayBatDau;
            congViec.NgayKetThuc = Input.NgayKetThuc;
            congViec.TrangThai = Input.TrangThai;
            congViec.UuTien = Input.MucDoUuTien;
            congViec.MaDuAn = Input.MaDuAn;
            congViec.Deleted = null;
            congViec.NgayTaoCongViec = DateTime.Now;
            _context.CongViec.Add(congViec);
            await _context.SaveChangesAsync();

            _notyf.Success("Thêm công việc thành công", 3);
            return RedirectToPage("./Index");
        }
    }
}
