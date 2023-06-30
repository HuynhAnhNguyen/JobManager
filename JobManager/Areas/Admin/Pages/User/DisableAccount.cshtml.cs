using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobManager.Areas.Admin.Pages.User
{
    public class DisableAccountModel : UserPageModel
    {
        public DisableAccountModel(UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, INotyfService notyf, ILogger<UserPageModel> logger, LanguageService localization, IEmailSender emailSender) : base(userManager, signInManager, roleManager, context, notyf, logger, localization, emailSender)
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string? KhachHangID { get; set; }
            public int DisableAccount { get; set; }
        }

        public NguoiDung user { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _notyf.Error("Không tìm thấy", 3);
                return RedirectToPage("./Index");
            }

            user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _notyf.Error("Không tìm thấy", 3);
                return RedirectToPage("./Index");
            }
            else
            {
                Input = new InputModel()
                {
                    KhachHangID = user.Id,
                    DisableAccount = user.DisableAccount
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                _notyf.Error("Không tìm thấy", 3);
                return RedirectToPage("./Index");
            }

            user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _notyf.Error("Không tìm thấy", 3);
                return RedirectToPage("./Index");
            }

            var oldDisableAccount = user.DisableAccount;

            if (oldDisableAccount != Input.DisableAccount)
            {
                _context.Update(user);
                user.DisableAccount = Input.DisableAccount;
                await _context.SaveChangesAsync();
                string email = user.Email;

                await _emailSender.SendEmailAsync(email, "Thay đổi trạng thái đăng nhập",
                    "Trạng thái đăng nhập của bạn đã đổi từ " + GetTenTrangThai(oldDisableAccount) + " thành " + GetTenTrangThai(Input.DisableAccount) + " lúc "+ DateTime.Now);

                _notyf.Success("Cập nhật trạng thái thành công", 3);
            }
            else
            {
                _notyf.Information("Không thay đổi trạng thái đăng nhập", 3);
            }

            return RedirectToPage("./Index");
        }


        public string GetTenTrangThai(int trangThai)
        {
            if (trangThai == -1)
            {
                return "<strong style=\"color: red;\">Tài khoản đã bị khóa</strong>";
            }
            else
            {
                return "<strong style=\"color: green;\">Tài khoản truy cập bình thường</strong>";
            }
        }
    }
}
