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
            var hydro = await _context.Hydro.ToListAsync();
            var wkt = hydro.ConvertAll(new Converter<Hydro, string>(ToWkt));
            return wkt;
        }

        private static string ToWkt(Hydro hydro)
        {
            if (hydro.Geom != null)
                return hydro.Geom.ToString();
            else
                return "MULTIPOLYGON EMPTY";
        }

        // GET: api/Hydro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hydro>> GetHydro(double id)
        {
            var hydro = await _context.Hydro.FindAsync(id);

            if (hydro == null)
            {
                return NotFound();
            }

            return hydro;
        }
    }
}
