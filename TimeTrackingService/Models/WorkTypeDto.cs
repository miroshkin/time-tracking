using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingService.Models
{
    public class WorkTypeDto
    {
        public int WorkTypeId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
