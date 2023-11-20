using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;
using Microsoft.EntityFrameworkCore;

namespace UniversalCourt.Website.Areas.Management.Pages.V3
{
    public class CreateModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

       
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnGetAsync()
        {
            var se = await _context.SuperSettings.FirstOrDefaultAsync();
            if (se == null)
            {
                SuperSetting SuperSetting = new SuperSetting();
                _context.SuperSettings.Add(SuperSetting);
                Setting s = new Setting();
                s.AddressOne = "address";
                _context.Settings.Add(s);
                 DataConfig fig = new DataConfig();
                _context.DataConfigs.Add(fig);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
