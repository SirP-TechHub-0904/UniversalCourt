using UniversalCourt.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UniversalCourt.Website.Areas.Content.Pages.WebSetting
{
    public class IndexModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public SuperSetting SuperSetting { get; set; }
        public Setting Setting { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync();
            Setting = await _context.Settings.FirstOrDefaultAsync();

            return Page();
        }
    }
}
