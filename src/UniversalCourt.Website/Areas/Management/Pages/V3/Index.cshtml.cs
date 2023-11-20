using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Management.Pages.V3
{
    public class IndexModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public SuperSetting SuperSetting { get; set; }
         public DataConfig DataConfig { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
             
            SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync();
                          DataConfig = await _context.DataConfigs.FirstOrDefaultAsync();

            return Page();
        }
    }
}
