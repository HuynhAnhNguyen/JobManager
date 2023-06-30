using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Models;
using JobManager.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace JobManager.Areas.Admin.Pages.User
{
    public class IndexModel : UserPageModel
    {
        public IndexModel(UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, INotyfService notyf, ILogger<UserPageModel> logger, LanguageService localization, IEmailSender emailSender) : base(userManager, signInManager, roleManager, context, notyf, logger, localization, emailSender)
        {
        }
        public class UserAndRole : NguoiDung
        {
            public int statusAccount { get; set; }
            public string RoleNames { get; set; }
        }

        public List<NguoiDung> soLuongUser { get; set; }

        public List<UserAndRole> users { get; set; }

        public const int ITEMS_PER_PAGE = 10;


        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPage { get; set; }

        public bool IsValidEmail(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }

        public async void OnGetAsync(string Search)
        {
            soLuongUser = await _context.NguoiDung.ToListAsync();
            if (soLuongUser.Count() > 0)
            {
                int totalUser = await _context.NguoiDung.CountAsync();
                countPage = (int)Math.Ceiling((double)totalUser / ITEMS_PER_PAGE);

                if (currentPage < 1)
                    currentPage = 1;
                if (currentPage > countPage)
                    currentPage = countPage;


                var qr = await _userManager.Users.OrderBy(u => u.UserName).Select(u => new UserAndRole()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed,
                }).ToListAsync();

                if (!string.IsNullOrEmpty(Search))
                {
                    if (IsValidEmail(Search))
                    {
                        users = qr.Where(x => x.Email.Contains(Search)).Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
                    }
                    else
                    {
                        users = qr.Where(x => x.UserName.Contains(Search)).Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();
                    }
                }
                else
                {
                    users = qr.Skip((currentPage - 1) * ITEMS_PER_PAGE).Take(ITEMS_PER_PAGE).ToList();

                }
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var statusAccount = await _context.NguoiDung.Where(a => a.Id == user.Id).Select(a => a.DisableAccount).FirstOrDefaultAsync();
                    user.statusAccount = statusAccount;
                    user.RoleNames = string.Join(", ", roles);
                }
            }

        }

        public void OnPost() => RedirectToPage();
    }
}
