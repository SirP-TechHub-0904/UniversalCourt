using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;
using UniversalCourt.Application.Services.AWS;
using UniversalCourt.Application.Dtos.AwsDtos;
using UniversalCourt.Application.Dtos;

namespace UniversalCourt.Website.Pages
{
    public class CSSSModel : PageModel
    {

        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public CSSSModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;

        }


        [BindProperty]
        public SuperSetting SuperSetting { get; set; }


        [BindProperty]
        public DataConfig DataConfig { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync();
            if (SuperSetting == null)
            {
                return RedirectToPage("/V3/Create", new { area = "Management" });
            }
            var dataConfig = await _context.DataConfigs.FirstOrDefaultAsync();
            if (DataConfig == null)
            {
                DataConfig dx = new DataConfig();
                dx.DashboardCSS = SuperSetting.DashboardCSS;
                dx.LayoutCSS = SuperSetting.LayoutCSS;
                dx.LoginCSS = SuperSetting.LoginCSS;
                dx.Configuration = SuperSetting.Configuration;
                dx.RedirectTohttpswww = SuperSetting.RedirectTohttpswww;
                dx.RedirectTohttps = SuperSetting.RedirectTohttps;
                dx.LiveConfiguration = SuperSetting.LiveConfiguration;

                _context.DataConfigs.Add(dx);

            }
            else
            {
                dataConfig.DashboardCSS = SuperSetting.DashboardCSS;
                dataConfig.LayoutCSS = SuperSetting.LayoutCSS;
                dataConfig.LoginCSS = SuperSetting.LoginCSS;
                dataConfig.Configuration = SuperSetting.Configuration;
                dataConfig.RedirectTohttpswww = SuperSetting.RedirectTohttpswww;
                dataConfig.RedirectTohttps = SuperSetting.RedirectTohttps;
                dataConfig.LiveConfiguration = SuperSetting.LiveConfiguration;

                _context.Attach(dataConfig).State = EntityState.Modified;
            }


            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var SuperSetting = await _context.SuperSettings.FirstOrDefaultAsync();

            var dataConfig = await _context.DataConfigs.FirstOrDefaultAsync();

            dataConfig.DashboardCSS = SuperSetting.DashboardCSS;
            dataConfig.LayoutCSS = SuperSetting.LayoutCSS;
            dataConfig.LoginCSS = SuperSetting.LoginCSS;
            dataConfig.Configuration = SuperSetting.Configuration;
            dataConfig.RedirectTohttpswww = SuperSetting.RedirectTohttpswww;
            dataConfig.RedirectTohttps = SuperSetting.RedirectTohttps;
            dataConfig.LiveConfiguration = SuperSetting.LiveConfiguration;

            _context.Attach(dataConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperSettingExists(SuperSetting.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/");
        }

        private bool SuperSettingExists(long id)
        {
            return _context.SuperSettings.Any(e => e.Id == id);
        }
    }

}

