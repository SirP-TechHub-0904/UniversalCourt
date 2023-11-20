using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Content.Pages.IDashboardSettings.IDataList
{
    public class CreateModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DashboardDataId"] = new SelectList(_context.DashboardDatas, "Id", "Title");

            ViewData["CompanyProgramId"] = new SelectList(_context.CompanyProgramCategories, "Id", "Title");

            return Page();
        }

        [BindProperty]
        public DashboardDataList DashboardDataList { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            _context.DashboardDataLists.Add(DashboardDataList);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
