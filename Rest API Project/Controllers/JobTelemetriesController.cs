using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Rest_API_Project.Models;

namespace Rest_API_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobTelemetriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobTelemetriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobTelemetries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
          if (_context.JobTelemetries == null)
          {
              return NotFound();
          }
            return await _context.JobTelemetries.ToListAsync();
        }

        // GET: api/JobTelemetries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
          if (_context.JobTelemetries == null)
          {
              return NotFound();
          }
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        // PUT: api/JobTelemetries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
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

        // POST: api/JobTelemetries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
          if (_context.JobTelemetries == null)
          {
              return Problem("Entity set 'ApplicationDbContext.JobTelemetries'  is null.");
          }
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobTelemetry", new { id = jobTelemetry.Id }, jobTelemetry);
        }

        // DELETE: api/JobTelemetries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            if (_context.JobTelemetries == null)
            {
                return NotFound();
            }
            
            if (!JobTelemetryExists(id))
            {
                return NotFound();
            }

            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobTelemetryExists(int id)
        {
            return (_context.JobTelemetries?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("GetSavingsByProject/{projectId}")]
        public async Task<ActionResult> GetSavingsByProject(Guid projectId, DateTime startDate, DateTime endDate)
        {
            // Ensure the project exists
            var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == projectId);
            if (!projectExists)
            {
                return NotFound($"Project with ID {projectId} not found.");
            }

            // Calculate the total human time saved within the date range for the given project
            var totalHumanTimeSaved = await _context.JobTelemetries
                .Where(jt => jt.ProjectID == projectId && jt.EntryDate >= startDate && jt.EntryDate <= endDate)
                .SumAsync(jt => (int?)jt.HumanTime) ?? 0;

            // If you don't need to calculate cost, just return the time saved
            return Ok(new { ProjectId = projectId, TotalHumanTimeSaved = totalHumanTimeSaved });
        }

        [HttpGet("GetSavingsByClient/{clientId}")]
        public async Task<ActionResult> GetSavingsByClient(Guid clientId, DateTime startDate, DateTime endDate)
        {
            if (_context.JobTelemetries == null)
            {
                return NotFound();
            }

            var savings = await (from jt in _context.JobTelemetries
                                 join p in _context.Projects on jt.ProjectID equals p.ProjectId
                                 where p.ClientId == clientId && jt.EntryDate >= startDate && jt.EntryDate <= endDate
                                 group jt by p.ClientId into g
                                 select new
                                 {
                                     TotalTimeSaved = g.Sum(jt => jt.HumanTime),
                                     
                                     TotalCostSaved = 0
                                 }).FirstOrDefaultAsync();

            if (savings == null)
            {
                return NotFound();
            }

            return Ok(savings);
        }


    }
}
