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
    public class DzielniceController : ControllerBase
    {
        private readonly epionierContext _context;

        public DzielniceController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/Dzielnice
        [HttpGet]
        public async Task<List<string>> GetDzielnice()
        {
            var wkt = await _context.Dzielnice
                .Where(item => item.Geom != null)
                .Select(item => item.Geom.ToString())
                .ToListAsync();
            return wkt;
        }

        // GET: api/Dzielnice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dzielnice>> GetDzielnice(int id)
        {
            var dzielnice = await _context.Dzielnice.FindAsync(id);

            if (dzielnice == null)
            {
                return NotFound();
            }

            return dzielnice;
        }

        public async Task<ActionResult<string>> GetName(int? id)
        {
            if (!id.HasValue)
                return new EmptyResult();
            var dzielnice = await _context.Dzielnice.FindAsync(id.Value);
            if (dzielnice == null)
                return new EmptyResult();
            return dzielnice.NazwaPelna;
        }
    }
}
