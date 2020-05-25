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
            IQueryable<Odwiert> searchedSet = _context.Odwiert;
            if (preferences.Status != null && !preferences.Status.Equals(""))
                searchedSet = SearchByStatus(searchedSet, preferences.Status);
            if (preferences.Lokalizacja != null && !preferences.Lokalizacja.Equals(""))
                searchedSet = SearchByDistrict(searchedSet, preferences.Lokalizacja);
            if (preferences.EurefX1.HasValue || preferences.EurefX2.HasValue)
                searchedSet = SearchByX(searchedSet, preferences.EurefX1, preferences.EurefX2);
            if (preferences.EurefY1.HasValue || preferences.EurefY2.HasValue)
                searchedSet = SearchByY(searchedSet, preferences.EurefY1, preferences.EurefY2);
            if (preferences.Filtracja1.HasValue || preferences.Filtracja2.HasValue)
                searchedSet = SearchByFiltering(searchedSet, preferences.Filtracja1, preferences.Filtracja2);
            if (preferences.GlebokoscZwierciadla1.HasValue || preferences.GlebokoscZwierciadla2.HasValue)
                searchedSet = SearchByDepth(searchedSet, preferences.GlebokoscZwierciadla1, preferences.GlebokoscZwierciadla2);
            if (preferences.NrRbdh.HasValue)
                searchedSet = SearchByRbdh(searchedSet, preferences.NrRbdh);
            if (preferences.NazwaObiektu != null && !preferences.NazwaObiektu.Equals(""))
                searchedSet = SearchByName(searchedSet, preferences.NazwaObiektu);
            return await GetWktWithId(searchedSet);
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

        private IQueryable<Odwiert> SearchByStatus(IQueryable<Odwiert> searchedSet, string status)
        {
            return searchedSet
                .Where(item => item.Status.Equals(status));
        }

        private IQueryable<Odwiert> SearchByDistrict(IQueryable<Odwiert> searchedSet, string lokalizacja)
        {
            return searchedSet
                .Where(item => item.DzielnicaId.ToString().Equals(lokalizacja));
        }

        private IQueryable<Odwiert> SearchByDepth(IQueryable<Odwiert> searchedSet, decimal? glebokoscZwierciadla1, decimal? glebokoscZwierciadla2)
        {
            if (glebokoscZwierciadla1 > glebokoscZwierciadla2)
            {
                decimal? temp = glebokoscZwierciadla1;
                glebokoscZwierciadla1 = glebokoscZwierciadla2;
                glebokoscZwierciadla2 = temp;
            }
            List<int?> nrRbdhList = _context.ZwierciadloGl
                .Where(item => item.GlUstabilizowana > glebokoscZwierciadla1)
                .Where(item => item.GlUstabilizowana < glebokoscZwierciadla2)
                .Select(item => item.NrRbdh)
                .ToList();
            return searchedSet
                .Where(item => nrRbdhList.Contains(item.NrRbdh));
        }

        private IQueryable<Odwiert> SearchByFiltering(IQueryable<Odwiert> searchedSet, decimal? filtracja1, decimal? filtracja2)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Odwiert> SearchByY(IQueryable<Odwiert> searchedSet, double? eurefY1, double? eurefY2)
        {
            return searchedSet
                .Where(item => item.EurefY > eurefY1)
                .Where(item => item.EurefY < eurefY2);
        }

        private IQueryable<Odwiert> SearchByX(IQueryable<Odwiert> searchedSet, double? eurefX1, double? eurefX2)
        {
            return searchedSet
                .Where(item => item.EurefX > eurefX1)
                .Where(item => item.EurefX < eurefX2);
        }

        private IQueryable<Odwiert> SearchByRbdh(IQueryable<Odwiert> searchedSet, int? nrRbdh)
        {
            return searchedSet
                .Where(item => item.NrRbdh.Equals(nrRbdh));
        }

        private IQueryable<Odwiert> SearchByName(IQueryable<Odwiert> searchedSet, string nazwaObiektu)
        {
            return searchedSet
                .Where(item => item.NazwaObiektu.Equals(nazwaObiektu));
        }
    }
}
