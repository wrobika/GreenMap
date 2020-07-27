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
    public class ZielenController : Controller
    {
        private readonly epionierContext _context;
     
        public ZielenController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/Zielen
        [HttpGet]
        public async Task<List<string>> GetZielen()
        {
            var wkt = await _context.Zielen
                .Where(item => item.Geom != null)
                .Select(item => item.Geom.ToString())
                .ToListAsync();
            return wkt;
        }

        public async Task<List<MultiPolygon>> GetSpecifiedArea(double minArea, double maxArea)
        {
            if (minArea > maxArea)
            {
                double temp = minArea;
                minArea = maxArea;
                maxArea = temp;
            }
            return await _context.Zielen
                .Where(item => item.Powierzchn > minArea)
                .Where(item => item.Powierzchn < maxArea)
                .Select(item => item.Geom)
                .ToListAsync();
        }
    }
}
