using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Domain.Models
{
    public class JobDesignation
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public ICollection<Profile> Profiles { get; set; }
    }
}
