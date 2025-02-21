﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UniversalCourt.Domain.Models;

namespace UniversalCourt.Website.Areas.Admin.Pages.IFunds.CompanyFunds
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Fund")]

    public class DeleteModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CompanyFund CompanyFund { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompanyFund = await _context.CompanyFunds
                .Include(c => c.FundSource).FirstOrDefaultAsync(m => m.Id == id);

            if (CompanyFund == null)
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

            CompanyFund = await _context.CompanyFunds.FindAsync(id);

            if (CompanyFund != null)
            {
                _context.CompanyFunds.Remove(CompanyFund);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
