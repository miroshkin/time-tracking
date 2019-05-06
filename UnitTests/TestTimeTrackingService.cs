using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTrackingService.Controllers;
using TimeTrackingService.Models;

namespace UnitTests
{
    [TestClass]
    public class TestTimeTrackingService
    {
        private IConfigurationRoot _configuration;
        private DbContextOptions<TimeTrackingServiceContext> _options;
        private TimeTrackingServiceContext _context;

        public TestTimeTrackingService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<TimeTrackingServiceContext>().UseSqlServer(_configuration.GetConnectionString("TimeTrackingServiceContext")).Options;
            _context = new TimeTrackingServiceContext(_options);
        }

        [TestMethod]
        public void TestAddProject_OK()
        {
            var count = _context.Projects.Count();

            var controller = new TimeRegistrationsController(_context);

            var project = new Project() {Name = "New project"};
            _context.Projects.Add(project);
            _context.SaveChanges();

            var newCount = _context.Projects.Count();
            Assert.IsTrue(count + 1 == newCount);

            var createdProject = _context.Projects.SingleOrDefault(m => m.ProjectId == project.ProjectId);
            _context.Remove(createdProject);
            _context.SaveChanges();

        }
    }
}
