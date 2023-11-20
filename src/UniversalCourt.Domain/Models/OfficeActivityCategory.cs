﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Domain.Models
{
    public class OfficeActivityCategory
    {
        public long Id { get;set;}
        public string Title { get;set;}
        public ICollection<OfficeActivityDialy> OfficeActivityDialies { get; set;}
    }
}
