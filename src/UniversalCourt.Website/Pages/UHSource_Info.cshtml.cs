﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;
using UniversalCourt.Application.Dtos;
using UniversalCourt.Application.Repository.Services;

namespace UniversalCourt.Website.Pages
{
    public class UHSource_InfoModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly ISettingsService _settingsService;
        public UHSource_InfoModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, ISettingsService settingsService)
        {
            _context = context;
            _settingsService = settingsService;
        }
        public Setting Setting { get; set; }

        public Product Product { get; set; }
        public SuperSetting SuperSetting { get; set; }
        public string Templatechoose { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
             var httpContext = HttpContext;
            VerificationWebDto setting = await _settingsService.ValidateWeb(httpContext);
            if (setting.SettingFound == false)
            {
                return RedirectToPage(setting.Path, new { area = setting.Area });
            }
            if (setting.Portfolio == true)
            {
                return RedirectToPage(setting.PortfolioPath);

            }
            
            
            Product = await _context.Products
                .Include(x => x.ProductFeatures)
                .Include(x => x.ProductSamples)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
             
            Templatechoose = setting.SuperSetting.TemplateLayoutKey;
            SuperSetting = setting.SuperSetting;
            Setting = setting.Setting;
            return Page();
        }
    }
}
