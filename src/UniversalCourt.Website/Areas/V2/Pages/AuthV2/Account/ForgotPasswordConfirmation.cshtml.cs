﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UniversalCourt.Website.V2.Pages.Authv2.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public ForgotPasswordConfirmation(DashboardDbContext context)
        {
            _context = context;
        }
        public SuperSetting SuperSetting { get; set; }

        public string TemplateLogin { get; set; }
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            var check = await _context.SuperSettings.FirstOrDefaultAsync();
            if (check == null)
            {
                return RedirectToPage("/AuthVadmin/Readonly", new { area = "V2" });
            }
            TemplateLogin = check.LoginTemplateKey;
            SuperSetting = check;
            return Page();
        }
    }
}
