﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using UniversalCourt.Domain.Models;

namespace UniversalCourt.Website.Areas.Admin.Pages.IFunds.Source
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Fund")]

    public class IndexModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<FundSource> FundSource { get;set; }

        public async Task OnGetAsync()
        {
            FundSource = await _context.FundSources
                .Include(f => f.User).ToListAsync();
        }
    }
}
