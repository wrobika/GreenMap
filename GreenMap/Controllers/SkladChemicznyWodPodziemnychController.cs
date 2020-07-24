using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap.Models;
using Microsoft.AspNetCore.Authorization;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Hosting;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SkladChemicznyWodPodziemnychController : ControllerBase
    {
        private readonly epionierContext _context;

        public SkladChemicznyWodPodziemnychController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/SkladChemicznyWodPodziemnych
        [HttpGet]
        public async Task<Dictionary<string, string>> GetSkladChemicznyWodPodziemnych()
        {
            var wktWithId = await _context.SkladChemicznyWodPodziemnych
                .Where(item => item.X.HasValue)
                .Where(item => item.Y.HasValue)
                .ToDictionaryAsync(item => item.SymbolPunktu,
                   item => new Point(item.Y.Value, item.X.Value).ToText());
            return wktWithId;
        }

        // GET: api/SkladChemicznyWodPodziemnych/O-1
        [HttpGet("{id}")]
        public async Task<ActionResult<SkladChemicznyWodPodziemnych>> GetSkladChemicznyWodPodziemnych(string id)
        {
            var record = await _context.SkladChemicznyWodPodziemnych
                .Where(item => item.SymbolPunktu == id)
                .FirstOrDefaultAsync();
            if (record == null)
                return NotFound();
            return record;
        }
    }
}