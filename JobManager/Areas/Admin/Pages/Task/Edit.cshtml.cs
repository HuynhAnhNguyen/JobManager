using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace JobManager.Areas.Admin.Pages.Task
{
    public class EditModel : TaskPageModel
    {
        public EditModel(ApplicationDbContext context, INotyfService notyf, ILogger<TaskPageModel> logger, LanguageService localization) : base(context, notyf, logger, localization)
        {
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string? MaDuAn { get; set; }

            [Required(ErrorMessage = "Tên công việc là bắt buộc!")]
            [StringLength(255, MinimumLength = 2, ErrorMessage = "Tên công việc phải có độ dài từ 2 đến 255 ký tự!")]
            public string? TenDuAn { get; set; }

            [Required(ErrorMessage = "Mô tả công việc là bắt buộc!")]
            [StringLength(1000, MinimumLength = 2, ErrorMessage = "Mô tả công việc phải có độ dài từ 2 đến 1000 ký tự!")]
            public string? MoTaDuAn { get; set; }

            [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc!")]
            public DateTime? NgayBatDau { get; set; }

            [Required(ErrorMessage = "Ngày kết thúc là bắt buộc!")]
            public DateTime? NgayKetThuc { get; set; }

            [Required(ErrorMessage = "Trạng thái là bắt buộc!")]
            public int? TrangThai { get; set; }

        }
        public DuAn duAn { get; set; }

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
            else
            {
                Input = new InputModel()
                {
                    MaDuAn = duAn.MaDuAn,
                    TenDuAn = duAn.TenDuAn,
                    MoTaDuAn = duAn.MoTaDuAn,
                    NgayBatDau = duAn.NgayBatDau,
                    NgayKetThuc = duAn.NgayKetThuc,
                    TrangThai = duAn.TrangThai,
                };
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync(string taskid)
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Update(duAn);

            duAn.TenDuAn = Input.TenDuAn;
            duAn.MoTaDuAn = Input.MoTaDuAn;
            duAn.NgayBatDau = Input.NgayBatDau;
            duAn.NgayKetThuc = Input.NgayKetThuc;
            duAn.TrangThai = Input.TrangThai;
            duAn.Deleted = false;
            duAn.NgayTaoDuAn = DateTime.Now;

            await _context.SaveChangesAsync();

            _notyf.Success("Chỉnh sửa dự án thành công", 3);
            return RedirectToPage("./Index");
        }
    }
}
