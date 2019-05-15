using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public void GetTimeRegistration_ReturnAll_OK()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);

            //Act 
            var registrations = controller.GetTimeRegistration();

            //Assert
            Assert.IsTrue(registrations.Any());
            Assert.IsNotNull(registrations.First());
        }


        [TestMethod]
        public void GetTimeRegistration_ReturnOne_OK()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);
            var id = _context.TimeRegistrations.First().TimeRegistrationId;

            //Act 
            var result = controller.GetTimeRegistration(id).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetTimeRegistration_ReturnOne_NotExists()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);

            //Act 
            var result = controller.GetTimeRegistration(-10000).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void PostTimeRegistration_OK_CreatedAtAction()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);
            var timeRegistration = new TimeRegistration()
                {Date = DateTime.Now, Duration = 1, WorkTypeId = 1};
            var projectId = _context.Projects.First().ProjectId;

            //Act 
            var result = controller.PostTimeRegistration(timeRegistration, projectId).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            
        }

        [TestMethod]
        public void DeleteTimeRegistration_OK()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);
            var timeRegistration = _context.TimeRegistrations.First();
            var oldCount = _context.TimeRegistrations.Count();

            //Act 
            var result = controller.DeleteTimeRegistration(timeRegistration.TimeRegistrationId).Result;
            var newCount = _context.TimeRegistrations.Count();

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.IsTrue(oldCount == newCount + 1);

        }

        [TestMethod]
        public void DeleteTimeRegistration_NotFound()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);
            var oldCount = _context.TimeRegistrations.Count();

            //Act 
            var result = controller.DeleteTimeRegistration(-1000000).Result;
            var newCount = _context.TimeRegistrations.Count();

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.IsTrue(oldCount == newCount);

        }

        [TestMethod]
        public void PutTimeRegistration_OK_NoContent()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);
            
            var timeRegistration = _context.TimeRegistrations.First();

            timeRegistration.Date = DateTime.Now.AddDays(10);
            timeRegistration.Duration = 5;
            timeRegistration.WorkTypeId = 1;

            //Act 
            var result = controller.PutTimeRegistration(timeRegistration.TimeRegistrationId, timeRegistration).Result;

            var updatedTimeRegistration =
                _context.TimeRegistrations.First(r => r.TimeRegistrationId == timeRegistration.TimeRegistrationId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.IsTrue(timeRegistration.Date == updatedTimeRegistration.Date);
            Assert.IsTrue(timeRegistration.Duration == updatedTimeRegistration.Duration);
            Assert.IsTrue(timeRegistration.WorkTypeId == updatedTimeRegistration.WorkTypeId);

        }

        [TestMethod]
        public void PutTimeRegistration_IncorrectData_BadRequestResult()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);
            var fakeTimeRegistration = new TimeRegistration();
            fakeTimeRegistration.Date = DateTime.Now.AddDays(10);
            fakeTimeRegistration.Duration = 5;
            fakeTimeRegistration.WorkTypeId = 1;

            var realTimeRegistrationId = _context.TimeRegistrations.First().TimeRegistrationId;

            //Act 
            var result = controller.PutTimeRegistration(realTimeRegistrationId, fakeTimeRegistration).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));

        }

        [TestMethod]
        public void PutTimeRegistration_IncorrectId_NotFound()
        {
            //Arrange 
            var controller = new TimeRegistrationsController(_context);
            var timeRegistration = new TimeRegistration();
            timeRegistration.TimeRegistrationId = -1000000;
            timeRegistration.Date = DateTime.Now.AddDays(10);
            timeRegistration.Duration = 5;
            timeRegistration.WorkTypeId = 1;

            //Act 
            var result = controller.PutTimeRegistration(timeRegistration.TimeRegistrationId, timeRegistration).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void GetProject_ReturnAll_OK()
        {
            //Arrange 
            var controller = new ProjectsController(_context);

            //Act 
            var regs = controller.GetProject();

            //Assert
            Assert.IsTrue(regs.Any());
            Assert.IsNotNull(regs.First());
        }


        [TestMethod]
        public void GetProject_ReturnOne_OK()
        {
            //Arrange 
            var controller = new ProjectsController(_context);
            var id = _context.Projects.First().ProjectId;

            //Act 
            var result = controller.GetProject(id).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            
        }

        [TestMethod]
        public void GetProject_ReturnOne_NotExists()
        {
            //Arrange 
            var controller = new ProjectsController(_context);

            //Act 
            var result = controller.GetProject(-10000).Result;

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

    }
}
