﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Content.Pages.IPrograms.ProgramData
{
    public class DeleteModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CompanyProgram CompanyProgram { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompanyProgram = await _context.CompanyPrograms
                .Include(c => c.CompanyProgramCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (CompanyProgram == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompanyProgram = await _context.CompanyPrograms.FindAsync(id);

            if (CompanyProgram != null)
            {
                _context.CompanyPrograms.Remove(CompanyProgram);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
