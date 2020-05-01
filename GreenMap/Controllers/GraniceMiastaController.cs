using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap;
using GreenMap.Models;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraniceMiastaController : ControllerBase
    {
        private readonly epionierContext _context;

        public GraniceMiastaController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/GraniceMiasta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetGraniceMiasta()
        {
            var boundaries = await _context.GraniceMiasta.ToListAsync();
            var wkt = boundaries.ConvertAll(new Converter<GraniceMiasta, string>(ToWkt));
            return wkt;
        }

        public static string ToWkt(GraniceMiasta border)
        {
            return border.Geom.ToString();
        }
    }
}
