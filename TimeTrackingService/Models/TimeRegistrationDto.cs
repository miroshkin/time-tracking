using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingService.Models
{
    public class TimeRegistrationDto
    {
        public int TimeRegistrationId { get; set; }

        public DateTime Date { get; set; }

        public string WorkTypeName { get; set; }

        public int WorkTypeId { get; set; }

        public double Price { get; set; }

        public int Duration { get; set; }

        public double Sum { get; set; }
    }
}
