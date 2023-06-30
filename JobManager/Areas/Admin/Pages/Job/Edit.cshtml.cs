using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace JobManager.Areas.Admin.Pages.Job
{
    public class EditModel : JobPageModel
    {
        public EditModel(ApplicationDbContext context, INotyfService notyf, ILogger<JobPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string? MaCongViec { get; set; }

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
            public int? TrangThai { get; set; }

            [Required(ErrorMessage = "Mức độ ưu tiên là bắt buộc!")]
            public int? MucDoUuTien { get; set; }

            public string? MaDuAn { get; set; }

        }

        public List<DuAn> duAns { get; set; }

        public CongViec congViec { get; set; }

        public async Task<IActionResult> OnGetAsync(string jobid)
        {
            duAns = await _context.DuAn.ToListAsync();

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
            else
            {
                Input = new InputModel()
                {
                    MaCongViec = congViec.MaCongViec,
                    TenCongViec = congViec.TenCongViec,
                    MoTaCongViec = congViec.MoTaCongViec,
                    NgayBatDau = congViec.NgayBatDau,
                    NgayKetThuc = congViec.NgayKetThuc,
                    TrangThai = congViec.TrangThai,
                    MucDoUuTien = congViec.MucDoUuTien,
                    MaDuAn = congViec.MaDuAn,
                };
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync(string jobid)
        {
            duAns = await _context.DuAn.ToListAsync();

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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Update(congViec);

            congViec.TenCongViec = Input.TenCongViec;
            congViec.MoTaCongViec = Input.MoTaCongViec;
            congViec.NgayBatDau = Input.NgayBatDau;
            congViec.NgayKetThuc = Input.NgayKetThuc;
            congViec.TrangThai = Input.TrangThai;
            congViec.MucDoUuTien = Input.MucDoUuTien;
            congViec.MaDuAn = Input.MaDuAn;
            congViec.Deleted = false;
            congViec.NgayTaoCongViec = DateTime.Now;

            await _context.SaveChangesAsync();

            _notyf.Success("Chỉnh sửa công việc thành công", 3);
            return RedirectToPage("./Index");
        }
    }
}
