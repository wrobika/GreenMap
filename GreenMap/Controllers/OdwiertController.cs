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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OdwiertController : ControllerBase
    {
        private readonly epionierContext _context;
        private readonly DzielniceController _dzielniceController;
        private readonly ZwierciadloGlController _zwierciadloController;

        public OdwiertController(epionierContext context)
        {
            _context = context;
            _dzielniceController = new DzielniceController(_context);
            _zwierciadloController = new ZwierciadloGlController(_context);
        }

        // GET: api/Odwiert
        [HttpGet]
        public async Task<Dictionary<long?, string>> GetOdwiert()
        {
            return await GetWktWithId(_context.Odwiert);
        }

        // GET: api/Odwiert/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OdwiertInfo>> GetOdwiert(long id)
        {
            var odwiert = await _context.Odwiert
                .Where(item => item.Objectid == id)
                .OrderBy(item => item.WspFiltracji)
                .FirstOrDefaultAsync();

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
                GlebokoscZwierciadla = zwierciadlo.Value,
                Filtracja = odwiert.WspFiltracji,
                KlasaFiltracji = odwiert.NazwaKlasy
            };
            return info;
        }

        public async Task<SelectList> GetStatusSelectList()
        {
            var possibleStatus = await _context.Odwiert
                .Select(item => item.Status)
                .Distinct()
                .Select(status => new SelectListItem { Text = status, Value = status })
                .ToListAsync();
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem { Selected = true, Text = "", Value = "" });
            statusList.AddRange(possibleStatus);
            return new SelectList(statusList, "Value", "Text");
        }

        public async Task<SelectList> GetFilterClassSelectList()
        {
            var possibleFilterClass = await _context.Odwiert
                .Select(item => item.NazwaKlasy)
                .Distinct()
                .Select(className => new SelectListItem { Text = className, Value = className })
                .ToListAsync();
            List<SelectListItem> filterClassList = new List<SelectListItem>();
            filterClassList.Add(new SelectListItem { Selected = true, Text = "", Value = "" });
            filterClassList.AddRange(possibleFilterClass);
            return new SelectList(filterClassList, "Value", "Text");
        }

        public static async Task<Dictionary<long?, string>> GetWktWithId(IQueryable<Odwiert> drillings)
        {
            var wktList = await drillings
                .Where(item => item.EurefX != null)
                .Where(item => item.EurefY != null)
                .Select(item => new
                {
                    id = item.Objectid,
                    wkt = new Point(item.EurefY.Value, item.EurefX.Value).ToString()
                })
                .ToListAsync();
            var wktDictionary = new Dictionary<long?, string>();
            foreach (var item in wktList)
                wktDictionary[item.id] = item.wkt;
            return wktDictionary;
        }

        public async Task<Dictionary<string, string>> GetFiltracja()
        {
            var points = await _context.Odwiert
                 .Where(item => item.EurefX != null)
                 .Where(item => item.EurefY != null)
                 .Where(item => item.NrKlasy != null)
                 .Select(item => new { item.EurefX, item.EurefY, item.NrKlasy })
                 .ToListAsync();
            var wktWithFiltering = new Dictionary<string, string>();
            foreach (var item in points)
            {
                string wkt = new Point(item.EurefY.Value, item.EurefX.Value).ToString();
                wktWithFiltering[wkt] = item.NrKlasy;
            }
            return wktWithFiltering;
        }
    }
}
