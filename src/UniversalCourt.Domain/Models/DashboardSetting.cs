using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Domain.Models
{
    public class DashboardSetting
    {
        public DashboardSetting()
        {
            ShowActivityProgram = true;
            ActivityProgramTitle = "Program Title";
        }

        public long Id { get; set; }

        [Display(Name = "Activity Program Title")]
        public string? ActivityProgramTitle { get; set; }


        [Display(Name = "Show Activity Program")]
        public bool ShowActivityProgram { get; set; }

        [Display(Name = "User Custom Dashboard")]
        public bool UserCustomDashboard { get; set; }
    }
}
