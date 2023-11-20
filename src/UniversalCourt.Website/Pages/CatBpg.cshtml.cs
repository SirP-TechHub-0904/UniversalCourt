using UniversalCourt.Application.Dtos;
using UniversalCourt.Application.Repository.Services;
using UniversalCourt.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UniversalCourt.Website.Pages
{
    public class CatBpgModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly ISettingsService _settingsService;
        public CatBpgModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, ISettingsService settingsService)
        {
            _context = context;
            _settingsService = settingsService;
        }
        public string Templatechoose { get; set; }
        public Setting Setting { get; set; }
        public SuperSetting SuperSetting { get; set; }
        public IList<Blog> Blogs { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public async Task<IActionResult> OnGetAsync(long id, string? title)
        {
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

            Templatechoose = setting.SuperSetting.TemplateLayoutKey;
            SuperSetting = setting.SuperSetting;
            Blogs = await _context.Blogs.Where(x => x.Publish == true).OrderByDescending(x => x.Date).ToListAsync();
            BlogCategory = await _context.BlogCategories.FirstOrDefaultAsync(x=>x.Id == id);
            Setting = await _context.Settings.FirstOrDefaultAsync();
            return Page();
        }
    }
}
