﻿using Amazon.S3;
using UniversalCourt.Persistence.EF.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalCourt.Pages.Shared.ViewComponents
{
    public class CompanyUrlViewComponent : ViewComponent
    {
        private readonly DashboardDbContext _context;
         
        public CompanyUrlViewComponent(
            DashboardDbContext context)
        {
            _context = context;
        }

        public string UserInfo { get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var xsetting = await _context.SuperSettings.FirstOrDefaultAsync();
            TempData["CompanyWebsiteLink"] = xsetting.CompanyWebsiteLink;
            return View();
        }
    }
}
