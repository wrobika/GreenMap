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
        private readonly  DzielniceController _dzielniceController;
        private readonly ZwierciadloGlController _zwierciadloController;

        public OdwiertController(epionierContext context)
        {
            _context = context;
            _dzielniceController = new DzielniceController(_context);
            _zwierciadloController = new ZwierciadloGlController(_context);
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

            var dzielnica = await _dzielniceController.GetName(odwiert.DzielnicaId);
            var zwierciadlo = await _zwierciadloController.GetGlebokosc(odwiert.NrRbdh);
            OdwiertInfo info = new OdwiertInfo
            {
                NazwaObiektu = odwiert.NazwaObiektu,
                NrRbdh = odwiert.NrRbdh,
                Lokalizacja = dzielnica.Value,
                Status = odwiert.Status,
                Wspolrzedne = odwiert.EurefX.ToString() + " " + odwiert.EurefY.ToString(),
                GlebokoscZwierciadla = zwierciadlo.Value
            };
            return info;
        }
    }
}
