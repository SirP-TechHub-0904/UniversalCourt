﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Content.Pages.IPrograms.IProgramUser
{
    public class DetailsModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public DetailsModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public ProgramUser ProgramUser { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProgramUser = await _context.ProgramUsers
                .Include(p => p.CompanyProgram)
                .Include(p => p.User).FirstOrDefaultAsync(m => m.Id == id);

            if (ProgramUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
