﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Content.Pages.IDashboardSettings.ISettings
{
    public class DetailsModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public DetailsModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public DashboardSetting DashboardSetting { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DashboardSetting = await _context.DashboardSettings.FirstOrDefaultAsync(m => m.Id == id);

            if (DashboardSetting == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
