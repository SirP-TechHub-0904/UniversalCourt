﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Content.Pages.IPrograms.IProgramUser
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
        ViewData["CompanyProgramId"] = new SelectList(_context.CompanyPrograms, "Id", "Title");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName");
            return Page();
        }

        [BindProperty]
        public ProgramUser ProgramUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            _context.ProgramUsers.Add(ProgramUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
