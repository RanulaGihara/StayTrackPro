using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayTrackPro.API.Models;
using StayTrackPro.API.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StayTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuitesController : ControllerBase
    {
        private readonly StayTrackProDbContext _context;

        public SuitesController(StayTrackProDbContext context)
        {
            _context = context;
        }

        // GET: api/suites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuiteDto>>> GetSuites()
        {
            var suites = await _context.Suites
                .Select(s => new SuiteDto
                {
                    Id = s.Id,
                    SuiteType = s.SuiteType,
                    PricePerNight = s.PricePerNight
                })
                .ToListAsync();

            return Ok(suites);
        }

        // GET: api/suites/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SuiteDto>> GetSuite(int id)
        {
            var suite = await _context.Suites.FindAsync(id);
            if (suite == null) return NotFound();

            return Ok(new SuiteDto
            {
                Id = suite.Id,
                SuiteType = suite.SuiteType,
                PricePerNight = suite.PricePerNight
            });
        }

        // POST: api/suites
        [HttpPost]
        public async Task<ActionResult> CreateSuite(SuiteDto dto)
        {
            var suite = new Suite
            {
                SuiteType = dto.SuiteType,
                PricePerNight = dto.PricePerNight
            };

            _context.Suites.Add(suite);
            await _context.SaveChangesAsync();

            dto.Id = suite.Id;
            return CreatedAtAction(nameof(GetSuite), new { id = suite.Id }, dto);
        }

        // PUT: api/suites/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSuite(int id, SuiteDto dto)
        {
            var suite = await _context.Suites.FindAsync(id);
            if (suite == null) return NotFound();

            suite.SuiteType = dto.SuiteType;
            suite.PricePerNight = dto.PricePerNight;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/suites/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSuite(int id)
        {
            var suite = await _context.Suites.FindAsync(id);
            if (suite == null) return NotFound();

            _context.Suites.Remove(suite);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
