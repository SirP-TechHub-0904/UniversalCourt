﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Admin.Pages.IOffice.Activity
{        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class DeleteModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OfficeActivityDialy OfficeActivityDialy { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OfficeActivityDialy = await _context.OfficeActivityDialies
                .Include(o => o.LastUpdateBy)
                .Include(o => o.OfficeActivityCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (OfficeActivityDialy == null)
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

            OfficeActivityDialy = await _context.OfficeActivityDialies.FindAsync(id);

            if (OfficeActivityDialy != null)
            {
                _context.OfficeActivityDialies.Remove(OfficeActivityDialy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
