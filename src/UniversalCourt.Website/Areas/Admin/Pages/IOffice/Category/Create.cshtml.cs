using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;

namespace UniversalCourt.Website.Areas.Admin.Pages.IOffice.Category
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class CreateModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public OfficeActivityCategory OfficeActivityCategory { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.OfficeActivityCategories.Add(OfficeActivityCategory);
            await _context.SaveChangesAsync();

            DateTime currentDate = DateTime.Now;
            DateTime endDate = new DateTime(currentDate.Year, 12, 31);

            while (currentDate <= endDate)
            {
                OfficeActivityDialy dialy = new OfficeActivityDialy();
                dialy.Date = currentDate; 
                dialy.Customer = 0;
                dialy.Amount = 0;
                dialy.LastUpdate = currentDate;
                dialy.OfficeActivityCategoryId = OfficeActivityCategory.Id;
                 _context.OfficeActivityDialies.Add(dialy);

                    currentDate = currentDate.AddDays(1);
            }
            await _context.SaveChangesAsync();
            TempData["success"] = "Successfully";
            return RedirectToPage("./Index");
        }
    }
}
