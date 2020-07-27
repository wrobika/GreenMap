using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap.Models;
using Microsoft.AspNetCore.Authorization;
using NetTopologySuite.Geometries;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ZanieczyszczenieGlebController : ControllerBase
    {
        private readonly epionierContext _context;

        public ZanieczyszczenieGlebController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/ZanieczyszczenieGleb
        [HttpGet]
        public async Task<Dictionary<string, string>> GetZanieczyszczenieGleb()
        {
            var wktWithId = await _context.ZanieczyszczenieGleb
                .Where(item => item.X.HasValue)
                .Where(item => item.Y.HasValue)
                .ToDictionaryAsync(item => item.Symbol,
                   item => new Point(item.Y.Value, item.X.Value).ToText());
            return wktWithId;
        }

        // GET: api/ZanieczyszczenieGleb/O-1
        [HttpGet("{id}")]
        public async Task<ActionResult<ZanieczyszczenieGleb>> GetZanieczyszczenieGleb(string id)
        {
            var record = await _context.ZanieczyszczenieGleb
                .Where(item => item.Symbol == id)
                .FirstOrDefaultAsync();
            if (record == null)
                return NotFound();
            record.Geom = null;
            return record;
        }
    }
}