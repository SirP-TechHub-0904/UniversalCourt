using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversalCourt.Domain.Models;
using UniversalCourt.Persistence.EF.SQL;
using System.Management;
using System.Net.NetworkInformation;
using UniversalCourt.Application.Repository.DeviceInformation;

namespace UniversalCourt.Website.Areas.Management.Pages.DeviceSetup
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin")]

    public class CreateModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IDeviceService _device;
        public CreateModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, IDeviceService device)
        {
            _context = context;
            _device = device;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EnableDevice EnableDevice { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var result = await _device.GetMackAddress();
            if (!String.IsNullOrEmpty(result))
            {
                EnableDevice.DeviceInformation = result;
                _context.EnableDevices.Add(EnableDevice);
                await _context.SaveChangesAsync();
                TempData["success"] = "success";
            }
            else
            {
                TempData["error"] = "Unable to fetch device information";
            }
            return RedirectToPage("./Index");
        }
    }
}
