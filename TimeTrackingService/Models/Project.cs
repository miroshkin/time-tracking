using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeTrackingService.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        public ICollection<TimeRegistration> TimeRegistrations { get; set; }
    }
}
