using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HydroController : ControllerBase
    {
        private readonly epionierContext _context;

        public HydroController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/Hydro
        [HttpGet]
        public async Task<List<string>> GetHydro()
        {
            var wkt = await _context.Hydro
                .Where(item => item.Geom != null)
                .Select(item => item.Geom.ToString())
                .ToListAsync();
            return wkt;
        }
    }
}
