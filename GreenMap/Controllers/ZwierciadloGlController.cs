using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap.Models;
using NetTopologySuite.Geometries;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZwierciadloGlController : ControllerBase
    {
        private readonly epionierContext _context;

        public ZwierciadloGlController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/ZwierciadloGl
        [HttpGet]
        public async Task<List<string>> GetZwierciadloGl()
        {
            var wkt = await _context.ZwierciadloGl
                 .Where(item => item.EurefX != null)
                 .Where(item => item.EurefY != null)
                 .Select(item => new Point(item.EurefY.Value, item.EurefX.Value).ToString())
                 .ToListAsync();
            return wkt;
        }

        public async Task<Dictionary<string, string>> GetZwierciadloGlebokosc()
        {
            var points = await _context.ZwierciadloGl
                 .Where(item => item.EurefX != null)
                 .Where(item => item.EurefY != null)
                 .Where(item => item.GlUstabilizowana != null)
                 .Select(item => new { item.EurefX, item.EurefY, item.GlUstabilizowana})
                 .ToListAsync();
            var wktDepthDictionary = new Dictionary<string, string>();
            foreach(var item in points)
            {
                string wkt = new Point(item.EurefY.Value, item.EurefX.Value).ToString();
                wktDepthDictionary[wkt] = DepthColor.GetColor(item.GlUstabilizowana.Value);
            }
            return wktDepthDictionary;
        }

        public async Task<List<string>> GetZwierciadloGl(decimal upperBound, decimal lowerBound)
        {
            var wkt = await _context.ZwierciadloGl
                 .Where(item => item.EurefX != null)
                 .Where(item => item.EurefY != null)
                 .Where(item => item.GlUstabilizowana != null)
                 .Where(item => item.GlUstabilizowana.Value < upperBound)
                 .Where(item => item.GlUstabilizowana.Value > lowerBound)
                 .Select(item => new Point(item.EurefY.Value, item.EurefX.Value).ToString())
                 .ToListAsync();
            return wkt;
        }

        // GET: api/ZwierciadloGl/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZwierciadloGl>> GetZwierciadloGl(int id)
        {
            var zwierciadloGl = await _context.ZwierciadloGl.FindAsync(id);

            if (zwierciadloGl == null)
            {
                return NotFound();
            }

            return zwierciadloGl;
        }
    }
}
