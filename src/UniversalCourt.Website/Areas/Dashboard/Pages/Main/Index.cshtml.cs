using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UniversalCourt.Website.Areas.Dashboard.Pages.Main
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Admin,SubAdmin")]

    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
