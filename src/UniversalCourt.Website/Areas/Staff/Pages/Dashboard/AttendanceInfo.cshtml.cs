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
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AttendanceInfoModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public AttendanceInfoModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public Attendance Attendance { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Attendance = await _context.Attendances
                .Include(a => a.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Attendance == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
