using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobManager.Areas.Admin.Pages.User
{
    public class AddRoleModel : UserPageModel
    {
        public AddRoleModel(UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, INotyfService notyf, ILogger<UserPageModel> logger, LanguageService localization, IEmailSender emailSender) : base(userManager, signInManager, roleManager, context, notyf, logger, localization, emailSender)
        {
        }

        public NguoiDung user { get; set; }

        [BindProperty]
        public string[] RoleNames { get; set; }

        public List<string> allRoles { get; set; }

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


            RoleNames = (await _userManager.GetRolesAsync(user)).ToArray<string>();

            List<string> roleName = await _roleManager.Roles.Select(x => x.Name).ToListAsync();

            allRoles = roleName.ToList();

            return Page();
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

            var OldRoleNames = (await _userManager.GetRolesAsync(user)).ToArray();

            var deleteRoles = OldRoleNames.Where(r => !RoleNames.Contains(r));

            var addRoles = RoleNames.Where(r => !OldRoleNames.Contains(r));

            List<string> roleName = await _roleManager.Roles.Select(x => x.Name).ToListAsync();

            allRoles = roleName.ToList();

            var resultDelete = await _userManager.RemoveFromRolesAsync(user, deleteRoles);
            if (!resultDelete.Succeeded)
            {
                resultDelete.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                });
                return Page();
            }


            var resultAdd = await _userManager.AddToRolesAsync(user, addRoles);
            if (!resultAdd.Succeeded)
            {
                resultAdd.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                });
                return Page();
            }

            _notyf.Success("Thêm role cho user thành công", 3);

            return RedirectToPage("./Index");
        }
    }
}
