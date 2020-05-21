using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly epionierContext _context;

        public SearchController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/Search
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Odwiert>>> GetOdwiert()
        {
            return await _context.Odwiert.ToListAsync();
        }

        // GET: api/Search/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Odwiert>> GetOdwiert(long id)
        {
            var odwiert = await _context.Odwiert.FindAsync(id);

            if (odwiert == null)
            {
                return NotFound();
            }

            return odwiert;
        }

        // PUT: api/Search/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOdwiert(long id, Odwiert odwiert)
        {
            if (id != odwiert.Objectid)
            {
                return BadRequest();
            }

            _context.Entry(odwiert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OdwiertExists(id))
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

        // POST: api/Search
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Odwiert>> PostOdwiert(Odwiert odwiert)
        {
            _context.Odwiert.Add(odwiert);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OdwiertExists(odwiert.Objectid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOdwiert", new { id = odwiert.Objectid }, odwiert);
        }

        // DELETE: api/Search/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Odwiert>> DeleteOdwiert(long id)
        {
            var odwiert = await _context.Odwiert.FindAsync(id);
            if (odwiert == null)
            {
                return NotFound();
            }

            _context.Odwiert.Remove(odwiert);
            await _context.SaveChangesAsync();

            return odwiert;
        }

        private bool OdwiertExists(long id)
        {
            return _context.Odwiert.Any(e => e.Objectid == id);
        }
    }
}
