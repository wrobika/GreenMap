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
            var greenery = await _context.Zielen.ToListAsync();
            var wkt = greenery.ConvertAll(new Converter<Zielen, string>(ToWkt));
            return wkt;
        }

        private static string ToWkt(Zielen greenery)
        {
            if (greenery.Geom != null)
                return greenery.Geom.ToString();
            else
                return "MULTIPOLYGON EMPTY";
        }
    }
}
