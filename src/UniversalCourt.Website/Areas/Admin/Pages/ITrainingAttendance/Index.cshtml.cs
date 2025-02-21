﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UniversalCourt.Domain.Models;

namespace UniversalCourt.Website.Areas.Admin.Pages.ITrainingAttendance
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager")]

    public class IndexModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<TrainingAttendance> TrainingAttendance { get;set; }

        public async Task OnGetAsync()
        {
            TrainingAttendance = await _context.TrainingAttendances
                .Include(t => t.Training)
                .Include(t => t.User).ToListAsync();
        }
    }
}
