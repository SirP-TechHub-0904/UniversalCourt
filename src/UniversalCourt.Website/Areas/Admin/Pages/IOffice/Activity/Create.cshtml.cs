using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Admin.Pages.IOffice.Activity
{        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class CreateModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LastUpdateById"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["OfficeActivityCategoryId"] = new SelectList(_context.OfficeActivityCategories, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public OfficeActivityDialy OfficeActivityDialy { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OfficeActivityDialies.Add(OfficeActivityDialy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
