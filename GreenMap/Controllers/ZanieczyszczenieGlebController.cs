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
            var wktWithSymbol = await _context.ZanieczyszczenieGleb
                .Where(item => item.X != null)
                .Where(item => item.Y != null)
                .ToDictionaryAsync(item => item.Symbol,
                   item => new Point(item.Y.Value, item.X.Value).ToText());
            return wktWithSymbol;
        }

        // GET: api/ZanieczyszczenieGleb/O-1
        [HttpGet("{symbol}")]
        public async Task<ActionResult<ZanieczyszczenieGleb>> GetZanieczyszczenieGleb(string symbol)
        {
            var record = await _context.ZanieczyszczenieGleb
                .Where(item => item.Symbol == symbol)
                .FirstOrDefaultAsync();
            if (record == null)
                return NotFound();
            return record;
        }
    }
}