using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobManager.Areas.Admin.Pages.User
{
    public class SetPasswordModel : UserPageModel
    {
        public SetPasswordModel(UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, INotyfService notyf, ILogger<UserPageModel> logger, LanguageService localization, IEmailSender emailSender) : base(userManager, signInManager, roleManager, context, notyf, logger, localization, emailSender)
        {
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Mật khẩu là bắt buộc!")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Mật khẩu không đủ mạnh!")]
            public string NewPassword { get; set; }

            [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc!")]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
            public string ConfirmPassword { get; set; }
        }

        public NguoiDung user { get; set; }

        public async Task<IActionResult> OnGetAsync(string id) => RedirectToPage("./Index");

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

            await _userManager.RemovePasswordAsync(user);

            string newPass = GenerateRandomString();

            var addPasswordResult = await _userManager.AddPasswordAsync(user, newPass);

            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            _notyf.Success("Đặt lại mật khẩu thành công", 3);

            await _emailSender.SendEmailAsync(user.Email, "Mật khẩu đã được cập nhật",
                           "Mật khẩu mới của bạn là " + "<strong>" + newPass + "</strong>");

            return RedirectToPage("./Index");
        }

        public string GenerateRandomString()
        {
            string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            string specialCharacters = "!@#$%^&*()";

            Random random = new Random();

            StringBuilder builder = new StringBuilder();

            // Chọn ngẫu nhiên một chữ cái viết hoa
            char uppercaseChar = uppercaseLetters[random.Next(uppercaseLetters.Length)];
            builder.Append(uppercaseChar);

            // Chọn ngẫu nhiên một chữ cái viết thường
            char lowercaseChar = lowercaseLetters[random.Next(lowercaseLetters.Length)];
            builder.Append(lowercaseChar);

            // Chọn ngẫu nhiên một kí tự đặc biệt
            char specialChar = specialCharacters[random.Next(specialCharacters.Length)];
            builder.Append(specialChar);

            // Chọn ngẫu nhiên 5 kí tự bất kỳ
            for (int i = 0; i < 5; i++)
            {
                char randomChar = (char)random.Next(33, 127); // Mã ASCII từ 33 đến 126 đại diện cho các kí tự in được
                builder.Append(randomChar);
            }

            return builder.ToString();
        }
    }
}
