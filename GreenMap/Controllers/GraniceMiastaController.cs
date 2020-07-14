using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap;
using GreenMap.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GraniceMiastaController : ControllerBase
    {
        private readonly epionierContext _context;

        public GraniceMiastaController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/GraniceMiasta
        [HttpGet]
        public async Task<List<string>> GetGraniceMiasta()
        {
            var wkt = await _context.GraniceMiasta
                .Where(item => item.Geom != null)
                .Select(item => item.Geom.ToString())
                .ToListAsync();
            return wkt;
        }
    }
}
