﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using UniversalCourt.Domain.Models;

namespace UniversalCourt.Website.Areas.Admin.Pages.IReport
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager")]

    public class UpdateReportModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public UpdateReportModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Report Report { get; set; }
        [BindProperty]
        public string Note { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report = await _context.Report
                .Include(r => r.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Report == null)
            {
                return NotFound();
            }
             return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var reportdata = await _context.Report.FirstOrDefaultAsync(x => x.Id == Report.Id);
            reportdata.ManagerReport = reportdata.ManagerReport + "<hr style=\"border:2px solid white; margin:0;padding:0;\"> @updated on " + DateTime.UtcNow.AddHours(1) + "<br>" + Note;
            reportdata.VerificationStatus = Report.VerificationStatus;
            reportdata.ReportPeriodStatus = Report.ReportPeriodStatus;
            _context.Attach(reportdata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(Report.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReportExists(long id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
