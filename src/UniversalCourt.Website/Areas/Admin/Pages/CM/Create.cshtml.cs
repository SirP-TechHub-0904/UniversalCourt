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

namespace UniversalCourt.Website.Areas.Content.Pages.CM
{
    public class CreateModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(long id)
        {
            ViewData["CompanyProgramCategoryId"] = new SelectList(_context.CompanyProgramCategories, "Id", "Title");
            CompanyProgramCategory = await _context.CompanyProgramCategories.FirstOrDefaultAsync(x => x.Id == id);
            return Page();
        }

        [BindProperty]
        public CompanyProgram CompanyProgram { get; set; }
        public CompanyProgramCategory CompanyProgramCategory { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {


            _context.CompanyPrograms.Add(CompanyProgram);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = CompanyProgram.CompanyProgramCategoryId });
        }
    }
}
