﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Domain.Models
{
    public class TrainingAttendance
    {
        public long Id {get; set; }
        public string? UserId { get; set; }
        public Profile User { get; set; }

        public long TrainingId { get; set; }
        public Training Training { get; set; }
        public DateTime Time { get; set; }
    }
}
