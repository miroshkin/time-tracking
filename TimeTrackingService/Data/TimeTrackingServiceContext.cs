using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Models;

namespace TimeTrackingService.Models
{
    public class TimeTrackingServiceContext : DbContext
    {
        public TimeTrackingServiceContext (DbContextOptions<TimeTrackingServiceContext> options)
            : base(options)
        {
        }

        public DbSet<TimeTrackingService.Models.TimeRegistration> TimeRegistrations { get; set; }
        public DbSet<TimeTrackingService.Models.Project> Projects { get; set; }
        public DbSet<TimeTrackingService.Models.Customer> Customers { get; set; }
        public DbSet<TimeTrackingService.Models.WorkType> WorkTypes { get; set; }

    }
}
