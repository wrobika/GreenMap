using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap;
using NetTopologySuite.Geometries;
using GreenMap.Models;

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
        public async Task<Dictionary<long, string>> GetOdwiert()
        {
            var wkt = await _context.Odwiert
                .Where(item => item.EurefX != null)
                .Where(item => item.EurefY != null)
                .ToDictionaryAsync(item => item.Objectid,
                    item => new Point(item.EurefY.Value, item.EurefX.Value).ToString());
            return wkt;
        }

        // GET: api/Odwiert/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OdwiertInfo>> GetOdwiert(long id)
        {
            var odwiert = await _context.Odwiert.FindAsync(id);

            if (odwiert == null)
            {
                return NotFound();
            }

            OdwiertInfo info = new OdwiertInfo
            {
                NazwaObiektu = odwiert.NazwaObiektu,
                NrRbdh = odwiert.NrRbdh,
                Lokalizacja = odwiert.Lokalizacja,
                Status = odwiert.Status,
                EurefX = odwiert.EurefX,
                EurefY = odwiert.EurefY 
            };
            return info;
        }
    }
}
