using UniversalCourt.Application.Dtos;
using UniversalCourt.Application.Repository.Services;
using UniversalCourt.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UniversalCourt.Website.Pages
{
    public class ContactModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly ISettingsService _settingsService;
        public ContactModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, ISettingsService settingsService)
        {
            _context = context;
            _settingsService = settingsService;
        }
        public SuperSetting SuperSetting { get; set; }
        public Setting Setting { get; set; }
        
        [BindProperty]
        public ClientRequest ClientRequest { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Setting = await _context.Settings.FirstOrDefaultAsync();
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
            
             SuperSetting = setting.SuperSetting;
            return Page();
        }

         // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
             
            _context.ClientRequest.Add(ClientRequest);
            await _context.SaveChangesAsync(); 
            TempData["success"] = "Successful";

            return RedirectToPage("/ThankYou");
        }
    }
}
