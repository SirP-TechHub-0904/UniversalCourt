﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UniversalCourt.Domain.Models;

namespace UniversalCourt.Web.Areas.Staff.Pages.Dashboard
{
    public class TrainingDetailsModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public TrainingDetailsModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public Training Training { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Training = await _context.Trainings
                .Include(t => t.TrainingAttendances)
                .Include(t => t.Moderator)
                .Include(t => t.Trainner).FirstOrDefaultAsync(m => m.Id == id);

            if (Training == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
