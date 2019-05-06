using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingService.Models
{
    public class TimeRegistration
    {
        public int TimeRegistrationId { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public int WorkTypeId { get; set; }

        public WorkType WorkType { get; set; }
    }
}
