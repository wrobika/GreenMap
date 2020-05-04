using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap;
using NetTopologySuite.Geometries;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdwiertController : ControllerBase
    {
        private readonly epionierContext _context;

        public OdwiertController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/Odwiert
        [HttpGet]
        public async Task<List<string>> GetOdwiert()
        {
            var wkt = await _context.Odwiert
                .Where(item => item.EurefX != null)
                .Where(item => item.EurefY != null)
                .Select(item => new Point(item.EurefY.Value, item.EurefX.Value).ToString())
                .ToListAsync();
            return wkt;
        }

        // GET: api/Odwiert/5
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
    }
}
