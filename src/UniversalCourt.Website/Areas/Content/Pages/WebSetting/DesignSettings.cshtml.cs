using UniversalCourt.Application.Services.AWS;
using UniversalCourt.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UniversalCourt.Website.Areas.Content.Pages.WebSetting
{
       [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,SubAdmin")]

    public class DesignSettingsModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;

        public DesignSettingsModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
        }

         
        [BindProperty]
        public Setting Setting { get; set; }
        [BindProperty]
        public DataConfig DataConfig { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            Setting = await _context.Settings.FirstOrDefaultAsync();

            if (Setting == null)
            {
                return NotFound();
            }
            DataConfig = await _context.DataConfigs.FirstOrDefaultAsync();

            if (DataConfig == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var updatesetting = await _context.Settings.FirstOrDefaultAsync();
            var superupdate = await _context.DataConfigs.FirstOrDefaultAsync();
            
            ///
            
            superupdate.DashboardCSS = DataConfig.DashboardCSS;
            superupdate.LayoutCSS = DataConfig.LayoutCSS;
            superupdate.LoginCSS = DataConfig.LoginCSS;
            superupdate.Configuration = DataConfig.Configuration;
            superupdate.RedirectTohttpswww = DataConfig.RedirectTohttpswww;
            superupdate.RedirectTohttps = DataConfig.RedirectTohttps;
            superupdate.LiveConfiguration = DataConfig.LiveConfiguration;

            superupdate.LayoutJavaScript = DataConfig.LayoutJavaScript;
            

            try
            {
                _context.Attach(superupdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception g) { }
             
            TempData["success"] = "Successful";


            return RedirectToPage("./Index");
        }


    }
}
