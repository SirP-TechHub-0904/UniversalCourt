using UniversalCourt.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniversalCourt.Web.Areas.Staff.Pages.Dashboard
{
     [Microsoft.AspNetCore.Authorization.Authorize]

    public class ReferralPageModel : PageModel
    {
        private readonly UniversalCourt.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public ReferralPageModel(UniversalCourt.Persistence.EF.SQL.DashboardDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Profile Profile { get; set; }

        public async Task OnGetAsync()
        {
            Profile= await _userManager.GetUserAsync(User);
   
        }

    }

}
