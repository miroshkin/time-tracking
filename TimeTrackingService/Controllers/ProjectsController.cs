﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackingService.Models;

namespace TimeTrackingService.Controllers
{
    [Produces("application/json")]
    [Route("api/Projects")]
    public class ProjectsController : Controller
    {
        private readonly TimeTrackingServiceContext _context;

        public ProjectsController(TimeTrackingServiceContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public IEnumerable<ProjectDto> GetProject()
        {
            return _context.Projects.Select(c => new ProjectDto(){Id = c.ProjectId, Name = c.Name});
        }

        // GET: api/Projects/Customer/5
        [HttpGet("Customers/{id}")]
        public async Task<IActionResult> GetProjectsByCustomerId([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projects = _context.Projects.Where(m => m.CustomerId == id);

            if (projects == null || !projects.Any())
            {
                return NotFound();
            }

            List<ProjectComplexDto> customerProjectsDtos = new List<ProjectComplexDto>();

            foreach (var customerProject in projects)
            {
                ProjectComplexDto projectDto = new ProjectComplexDto();
                projectDto.TimeRegistrations = new List<TimeRegistrationDto>();

                var project = await _context.Projects.
                    Include("TimeRegistrations").SingleOrDefaultAsync(m => m.ProjectId == customerProject.ProjectId);

                projectDto.Id = project.ProjectId;
                projectDto.Name = project.Name;


                foreach (var ptr in project.TimeRegistrations)
                {
                    ptr.WorkType =
                        _context.WorkTypes.Single(c => c.WorkTypeId == ptr.WorkTypeId);
                    ptr.WorkType.TimeRegistrations = null;

                    TimeRegistrationDto trdto = new TimeRegistrationDto()
                    {
                        Date = ptr.Date,
                        Duration = ptr.Duration,
                        Sum = ptr.Duration * ptr.WorkType.Price,
                        Price = ptr.WorkType.Price,
                        WorkTypeName = ptr.WorkType.Name,
                        TimeRegistrationId = ptr.TimeRegistrationId
                    };

                    projectDto.TimeRegistrations.Add(trdto);
                }

                projectDto.TotalSum = projectDto.TimeRegistrations.Sum(item => item.Sum);

                customerProjectsDtos.Add(projectDto);
            }

            

            

            return Ok(customerProjectsDtos);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject([FromRoute] int id)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectComplexDto projectDto = new ProjectComplexDto();
            projectDto.TimeRegistrations = new List<TimeRegistrationDto>();


            var project = await _context.Projects.
                Include("TimeRegistrations").SingleOrDefaultAsync(m => m.ProjectId == id);

            projectDto.Id = project.ProjectId;
            projectDto.Name = project.Name;


            foreach (var ptr in project.TimeRegistrations)
            {
                ptr.WorkType =
                    _context.WorkTypes.Single(c => c.WorkTypeId == ptr.WorkTypeId);
                ptr.WorkType.TimeRegistrations = null;

                TimeRegistrationDto trdto = new TimeRegistrationDto()
                {
                    Date = ptr.Date,
                    Duration = ptr.Duration,
                    Sum = ptr.Duration * ptr.WorkType.Price,
                    Price = ptr.WorkType.Price,
                    WorkTypeName = ptr.WorkType.Name,
                    TimeRegistrationId = ptr.TimeRegistrationId
                };

                projectDto.TimeRegistrations.Add(trdto);
            }

            projectDto.TotalSum = projectDto.TimeRegistrations.Sum(item => item.Sum);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(projectDto);
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject([FromRoute] int id, [FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.ProjectId }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}