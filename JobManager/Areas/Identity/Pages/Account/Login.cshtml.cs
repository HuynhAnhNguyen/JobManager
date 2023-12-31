﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using JobManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using AspNetCoreHero.ToastNotification.Abstractions;
using JobManager.Data;
using JobManager.Services;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace JobManager.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly UserManager<NguoiDung> _userManager;
        private readonly INotyfService _notyf;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly GoogleCaptchaService _googleCaptchaService;
        private readonly LanguageService _localization;

        public LoginModel(
            SignInManager<NguoiDung> signInManager,
            ILogger<LoginModel> logger,
            UserManager<NguoiDung> userManager,
            INotyfService notyf,
            ApplicationDbContext context,
            GoogleCaptchaService googleCaptchaService,
            LanguageService localization)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _notyf = notyf;
            _context = context;
            _googleCaptchaService = googleCaptchaService;
            _localization = localization;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Email/Username đăng nhập là bắt buộc!")]
            public string Username { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Mật khẩu là bắt buộc!")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public string Token { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            int qr;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Google Captcha
            var _googleCaptcha = _googleCaptchaService.VerifyreCaptcha(Input.Token);
            if (!_googleCaptcha.Result.success && _googleCaptcha.Result.score <= 0.5)
            {
                _notyf.Error("Mã captcha không hợp lệ", 3);
                ModelState.AddModelError(string.Empty, "Mã captcha không hợp lệ");
                return Page();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (IsValidEmail(Input.Username))
                    {
                        qr = await (from a in _context.NguoiDung
                                    where a.Email == Input.Username
                                    select a.DisableAccount).FirstOrDefaultAsync();
                    }
                    else
                    {
                        qr = await (from a in _context.NguoiDung
                                    where a.UserName == Input.Username
                                    select a.DisableAccount).FirstOrDefaultAsync();
                    }

                    if (qr == 0)
                    {
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                        // Neu dang nhap bang email ( dang nhap mac dinh la username)
                        if (!result.Succeeded)
                        {
                            var user = await _userManager.FindByEmailAsync(Input.Username);
                            if (user != null)
                            {
                                result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                            }
                        }

                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User logged in.");
                            _notyf.Success("Đăng nhập thành công", 3);
                            //return RedirectToPage("Index", new { Areas = "Admin", Page="/Home"});
                            return LocalRedirect(returnUrl);
                        }

                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }

                        if (result.IsLockedOut)
                        {
                            _notyf.Error("Tài khoản bị khóa tạm thời. Vui lòng đăng nhập lại sau.", 3);
                            _logger.LogWarning("User account locked out.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            _notyf.Error("Sai username/email hoặc mật khẩu", 3);
                            //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return Page();
                        }
                    }
                    else
                    {
                        _notyf.Error("Tài khoản đã bị khóa", 3);
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }

        public bool IsValidEmail(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
        }
    }
}
