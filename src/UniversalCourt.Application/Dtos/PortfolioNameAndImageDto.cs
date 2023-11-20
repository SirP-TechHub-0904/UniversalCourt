using UniversalCourt.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Application.Dtos
{
    public class PortfolioNameAndImageDto
    {
        public string Fullname { get;set; }
        public string Image { get;set; }
         public string Description { get;set;}
         public string Website { get;set;}
        public List<PortfolioContactUs> PortfolioContactUs { get;set; }
        public int ContactMeCount { get;set; }
    }
}
