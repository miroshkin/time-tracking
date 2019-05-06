using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingService.Models
{
    public class WorkType
    {
        public int WorkTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [ForeignKey("WorkTypeId")]
        public ICollection<TimeRegistration> TimeRegistrations { get; set; }
    }
}
