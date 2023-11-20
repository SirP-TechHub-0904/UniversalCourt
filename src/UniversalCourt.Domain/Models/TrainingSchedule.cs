using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UniversalCourt.Domain.Enum.Enum;

namespace UniversalCourt.Domain.Models
{
    public class TrainingSchedule
    {
        public long Id { get; set; }
        public string? Topic { get; set; }
        public DateTime Date { get; set; }
         public string? Moderator { get; set; }
        public string? Trainner { get; set; }
        public TrainingStatus TrainingStatus { get; set; }
    }
}
