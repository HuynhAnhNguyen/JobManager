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
    public class ConfirmAccountModel : UserPageModel
    {
        public ConfirmAccountModel(UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, INotyfService notyf, ILogger<UserPageModel> logger, LanguageService localization, IEmailSender emailSender) : base(userManager, signInManager, roleManager, context, notyf, logger, localization, emailSender)
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string? KhachHangID { get; set; }
            public bool Confirm { get; set; }
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
                    Confirm = user.EmailConfirmed
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

            var oldDisableAccount = user.EmailConfirmed;
            if (oldDisableAccount != Input.Confirm)
            {
                _context.Update(user);
                user.EmailConfirmed = Input.Confirm;
                await _context.SaveChangesAsync();

                _notyf.Success("Xác thực thành công", 3);
            }
            else
            {
                _notyf.Information("Không thay đổi thông tin xác thực", 3);
            }

            return RedirectToPage("./Index");
        }
    }
}
