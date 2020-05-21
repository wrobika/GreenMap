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

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdwiertController : ControllerBase
    {
        public IQueryable<Odwiert> FilteredDrilling { get; set; }
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
            return await GetWktWithId(_context.Odwiert);
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

        public async Task<SelectList> GetStatusSelectList()
        {
            var possibleStatus = await _context.Odwiert
                .Select(item => item.Status)
                .Distinct()
                .Select(status => new SelectListItem { Text = status, Value = status })
                .ToListAsync();
            List<SelectListItem> statusList = new List<SelectListItem>();
            statusList.Add(new SelectListItem { Selected=true, Text = "", Value = "" });
            statusList.AddRange(possibleStatus);
            return new SelectList(statusList, "Value", "Text");
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<long,string>>> Search([Bind("NazwaObiektu,NrRbdh,Lokalizacja,Status,EurefX1,EurefX2,EurefY1,EurefY2,GlebokoscZwierciadla1,GlebokoscZwierciadla2,Filtracja1,Filtracja2,HydroGleby,ZanieczyszczenieGleby, JakoscWody,Nawodnienie")] OdwiertSearch preferences)
        //public async Task<ActionResult<Dictionary<long, string>>> Search([FromForm] OdwiertSearch preferences)
        {
            IQueryable<Odwiert> filteredSet = _context.Odwiert;
            if (preferences.NazwaObiektu != null && !preferences.NazwaObiektu.Equals(""))
                SearchByName(preferences.NazwaObiektu);
            if (preferences.EurefX1.HasValue || preferences.EurefX2.HasValue)
                SearchByX(preferences.EurefX1, preferences.EurefX2);
            if (preferences.EurefY1.HasValue || preferences.EurefY2.HasValue)
                SearchByY(preferences.EurefY1, preferences.EurefY2);
            if (preferences.Filtracja1.HasValue || preferences.Filtracja2.HasValue)
                SearchByFilter(preferences.Filtracja1, preferences.Filtracja2);
            if (preferences.GlebokoscZwierciadla1.HasValue || preferences.GlebokoscZwierciadla2.HasValue)
                SearchByDepth(preferences.GlebokoscZwierciadla1, preferences.GlebokoscZwierciadla2);
            if (preferences.Lokalizacja != null && !preferences.Lokalizacja.Equals(""))
                filteredSet = SearchByDistrict(filteredSet, preferences.Lokalizacja);
            if (preferences.Status != null && !preferences.Status.Equals(""))
                SearchByStatus(preferences.Status);
            return await GetWktWithId(filteredSet);
        }

        private async Task<Dictionary<long, string>> GetWktWithId(IQueryable<Odwiert> drillings)
        {
            var wkt = await drillings
                .Where(item => item.EurefX != null)
                .Where(item => item.EurefY != null)
                .ToDictionaryAsync(item => item.Objectid,
                    item => new Point(item.EurefY.Value, item.EurefX.Value).ToString());
            return wkt;
        }

        private void SearchByStatus(string status)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Odwiert> SearchByDistrict(IQueryable<Odwiert> filteredSet, string lokalizacja)
        {
            return filteredSet
                .Where(item => item.DzielnicaId.ToString().Equals(lokalizacja));
        }

        private void SearchByDepth(decimal? glebokoscZwierciadla1, decimal? glebokoscZwierciadla2)
        {
            throw new NotImplementedException();
        }

        private void SearchByFilter(decimal? filtracja1, decimal? filtracja2)
        {
            throw new NotImplementedException();
        }

        private void SearchByY(decimal? eurefY1, decimal? eurefY2)
        {
            throw new NotImplementedException();
        }

        private void SearchByX(decimal? eurefX1, decimal? eurefX2)
        {
            throw new NotImplementedException();
        }

        private void SearchByName(string nazwaObiektu)
        {
            throw new NotImplementedException();
        }
    }
}
