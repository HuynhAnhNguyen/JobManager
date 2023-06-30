using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobManager.Areas.Admin.Pages.Task
{
    public class CreateModel : TaskPageModel
    {
        public CreateModel(ApplicationDbContext context, INotyfService notyf, ILogger<TaskPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Tên dự án là bắt buộc!")]
            [StringLength(255, MinimumLength = 2, ErrorMessage = "Tên dự án phải có độ dài từ 2 đến 255 ký tự!")]
            public string? TenDuAn { get; set; }

            [Required(ErrorMessage = "Mô tả dự án là bắt buộc!")]
            [StringLength(1000, MinimumLength = 2, ErrorMessage = "Mô tả dự án phải có độ dài từ 2 đến 1000 ký tự!")]
            public string? MoTaDuAn { get; set; }

            [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc!")]
            public DateTime? NgayBatDau { get; set; }

            [Required(ErrorMessage = "Ngày kết thúc là bắt buộc!")]
            public DateTime? NgayKetThuc { get; set; }

            [Required(ErrorMessage = "Trạng thái là bắt buộc!")]
            public int? TrangThai { get; set; }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var maDuAn = "" + DateTime.Now.Ticks;

            DuAn duAn = new DuAn();
            duAn.MaDuAn = maDuAn;
            duAn.TenDuAn = Input.TenDuAn;
            duAn.MoTaDuAn = Input.MoTaDuAn;
            duAn.NgayBatDau = Input.NgayBatDau;
            duAn.NgayKetThuc = Input.NgayKetThuc;
            duAn.TrangThai = Input.TrangThai;
            duAn.Deleted = false;
            duAn.NgayTaoDuAn = DateTime.Now;
            _context.DuAn.Add(duAn);
            await _context.SaveChangesAsync();

            _notyf.Success("Thêm dự án thành công", 3);
            return RedirectToPage("./Index");
        }
    }
}
