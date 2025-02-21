﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UniversalCourt.Domain.Enum.Enum;
using System.Xml.Linq;

namespace UniversalCourt.Domain.Models
{
    public class PageCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public ICollection<WebPage> WebPages { get; set; }
        public bool Publish { get; set; }

        [Display(Name = "Menu Sort Order")]
        public int MenuSortOrder { get; set; }

        [Display(Name = "Home Sort From")]
        public HomeSortFrom HomeSortFrom { get; set; }



    }
}
