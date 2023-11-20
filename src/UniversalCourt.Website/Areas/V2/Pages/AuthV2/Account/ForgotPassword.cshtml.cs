using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UniversalCourt.Domain.Models;
using UniversalCourt.Application.Repository.NotifyRegister;
using Amazon.S3.Transfer;
using Amazon.S3;
using UniversalCourt.Application.Dtos.NotifyDtos;
using Microsoft.EntityFrameworkCore;

namespace UniversalCourt.Website.V2.Pages.Authv2.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        private readonly UserManager<Profile> _userManager; 
        private readonly INotifyRegisterService _notify;

        public ForgotPasswordModel(UserManager<Profile> userManager, INotifyRegisterService notify, Persistence.EF.SQL.DashboardDbContext context)
        {
            _userManager = userManager; 
            _notify = notify;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
        public string TemplateLogin { get; set; }
        public SuperSetting SuperSetting { get; set; }
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            var check = await _context.SuperSettings.FirstOrDefaultAsync();
            if (check == null)
            {
                return RedirectToPage("/AuthVadmin/Readonly", new { area = "V2" });
            }
            TemplateLogin = check.LoginTemplateKey;
            SuperSetting =check;
            return Page();
        }

            public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    var check = await _context.SuperSettings.FirstOrDefaultAsync();
                    if (check == null)
                    {
                        return RedirectToPage("/AuthVadmin/Readonly", new { area = "V2" });
                    }
                    TemplateLogin = check.LoginTemplateKey;
                    SuperSetting = check;
                    TempData["error"] = "Invalid Email";
                    return Page();
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/AuthV2/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "V2", code },
                    protocol: Request.Scheme);

              string datavalue = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                var datasend = new NotifyDto()
                {
                    Title = "Reset Password",
                    NotificationTitle = "Reset Password",
                    Content = datavalue,
                    Receipient = Input.Email,
                    Name = user.FullnameX,
                    IsEmail = true
                    
                };
                await _notify.RegisterNotification(datasend);


                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
