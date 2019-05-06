using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeTrackingService.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<TimeRegistration> TimeRegistrations { get; set; }
    }
}
