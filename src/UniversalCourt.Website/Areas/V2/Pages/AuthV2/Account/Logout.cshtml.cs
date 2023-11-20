using System;
using System.Collections.Generic;
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
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Profile> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public LogoutModel(SignInManager<Profile> signInManager, ILogger<LogoutModel> logger, Persistence.EF.SQL.DashboardDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        //public void OnGet()
        //{
        //}

        public string TemplateLogin { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
             
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            
           if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToPage("./Login");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
