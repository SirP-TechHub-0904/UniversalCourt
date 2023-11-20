using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniversalCourt.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UniversalCourt.Website.V2.Pages.Authv2.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        private readonly SignInManager<Profile> _signInManager;
        private readonly ILogger<LoginWith2faModel> _logger;
        private readonly UserManager<Profile> _userManager;
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public LoginWith2faModel(SignInManager<Profile> signInManager, ILogger<LoginWith2faModel> logger, UserManager<Profile> userManager, Persistence.EF.SQL.DashboardDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }

            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }
        }

           public string TemplateLogin { get; set; }
               public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
         {
            var check = await _context.SuperSettings.FirstOrDefaultAsync();
            if (check == null)
            {
                return RedirectToPage("/AuthVadmin/Readonly", new { area = "V2" });
            }
            TemplateLogin = check.LoginTemplateKey;
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);
                var superrole = await _userManager.IsInRoleAsync(user, "mSuperAdmin");
                var adminrole = await _userManager.IsInRoleAsync(user, "Admin");
                var editorrole = await _userManager.IsInRoleAsync(user, "Editor");
                var managerrole = await _userManager.IsInRoleAsync(user, "Manager");

                if (superrole.Equals(true))
                {
                    return RedirectToPage("/Major/Index", new { area = "Dashboard" });
                }
                if (adminrole.Equals(true))
                {
                    return RedirectToPage("/Main/Index", new { area = "Dashboard" });
                }
                if (editorrole.Equals(true))
                {
                    return RedirectToPage("/Editor/Index", new { area = "Dashboard" });
                }
                if (managerrole.Equals(true))
                {
                    return RedirectToPage("/Manager/Index", new { area = "Dashboard" });
                }

                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
        }
    }
}
