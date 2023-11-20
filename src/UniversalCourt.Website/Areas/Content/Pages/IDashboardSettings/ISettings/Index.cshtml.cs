using System;
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
    public class IndexModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public DashboardSetting DashboardSetting { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            

            DashboardSetting = await _context.DashboardSettings.FirstOrDefaultAsync();

            if (DashboardSetting == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
