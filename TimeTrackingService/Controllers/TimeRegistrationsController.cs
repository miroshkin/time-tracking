using System;
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
    [Route("api/TimeRegistrations")]
    public class TimeRegistrationsController : Controller
    {
        private readonly TimeTrackingServiceContext _context;

        public TimeRegistrationsController(TimeTrackingServiceContext context)
        {
            _context = context;
        }

        // GET: api/TimeRegistrations
        [HttpGet]
        public IEnumerable<TimeRegistration> GetTimeRegistration()
        {
            return _context.TimeRegistrations;
        }

        // GET: api/TimeRegistrations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeRegistration = await _context.TimeRegistrations.SingleOrDefaultAsync(m => m.TimeRegistrationId == id);

            if (timeRegistration == null)
            {
                return NotFound();
            }

            return Ok(timeRegistration);
        }

        // PUT: api/TimeRegistrations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeRegistration([FromRoute] int id, [FromBody] TimeRegistration timeRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeRegistration.TimeRegistrationId)
            {
                return BadRequest();
            }

            _context.Entry(timeRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeRegistrationExists(id))
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

        // POST: api/TimeRegistrations?projectId={id}
        [HttpPost]
        public async Task<IActionResult> PostTimeRegistration([FromBody] TimeRegistration timeRegistration, int projectId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Add(timeRegistration);

            var project = _context.Projects.Include("TimeRegistrations").First(p => p.ProjectId == projectId);
            project.TimeRegistrations.Add(timeRegistration);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeRegistration", new { id = timeRegistration.TimeRegistrationId }, timeRegistration);
        }

        // DELETE: api/TimeRegistration/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeRegistration([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var timeRegistration = await _context.TimeRegistrations.SingleOrDefaultAsync(m => m.TimeRegistrationId == id);
            if (timeRegistration == null)
            {
                return NotFound();
            }

            _context.TimeRegistrations.Remove(timeRegistration);
            await _context.SaveChangesAsync();

            return Ok(timeRegistration);
        }

        private bool TimeRegistrationExists(int id)
        {
            return _context.TimeRegistrations.Any(e => e.TimeRegistrationId == id);
        }
    }
}