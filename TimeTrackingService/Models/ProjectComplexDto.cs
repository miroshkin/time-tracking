using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackingService.Models
{
    public class ProjectComplexDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TimeRegistrationDto> TimeRegistrations { get; set; }

        public double TotalSum { get; set; }
    }
}
