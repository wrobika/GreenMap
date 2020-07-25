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
using NetTopologySuite.Operation.Union;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly epionierContext _context;
        private OdwiertSearch Preferences { get; set; }
        private IQueryable<Odwiert> SearchedSet { get; set; }


        public SearchController(epionierContext context)
        {
            _context = context;
            SearchedSet = _context.Odwiert;
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<long?, string>>> SetPreferencesAndSearch(
            [Bind("NazwaObiektu,NrRbdh,Lokalizacja,Status,EurefX1,EurefX2,EurefY1,EurefY2,GlebokoscZwierciadla1,GlebokoscZwierciadla2,KlasaFiltracji,Filtracja1,Filtracja2,PowierzchniaZieleni1,PowierzchniaZieleni2")] 
            OdwiertSearch preferences)
        {
            Preferences = preferences;
            return await Search();
        }

        private async Task<Dictionary<long?, string>> Search()
        {
            SearchByName();
            SearchByRbdh();
            SearchByFiltering();
            SearchByDepth();
            SearchByStatus();
            SearchByFilteringClass();
            SearchByDistrict();
            SearchByX();
            SearchByY();
            SearchByGreeneryArea();

            return await OdwiertController.GetWktWithId(SearchedSet);
        }

        private void SearchByStatus()
        {
            if (Preferences.Status != null && !Preferences.Status.Equals(""))
                SearchedSet = SearchedSet
                    .Where(item => item.Status.Equals(Preferences.Status));
        }

        private void SearchByFilteringClass()
        {
            if (Preferences.KlasaFiltracji != null && !Preferences.KlasaFiltracji.Equals(""))
                SearchedSet = SearchedSet
                    .Where(item => item.NazwaKlasy.Equals(Preferences.KlasaFiltracji));
        }

        private void SearchByDistrict()
        {
            if (Preferences.Lokalizacja != null && !Preferences.Lokalizacja.Equals(""))
                SearchedSet = SearchedSet
                    .Where(item => item.DzielnicaId.ToString().Equals(Preferences.Lokalizacja));
        }

        private void SearchByGreeneryArea()
        {
            var area1 = Preferences.PowierzchniaZieleni1;
            var area2 = Preferences.PowierzchniaZieleni2;
            if (area1.HasValue || area2.HasValue)
            {
                if (area1 > area2)
                {
                    double? temp = area1;
                    area1 = area2;
                    area2 = temp;
                }
                var greeneryCollection = _context.Zielen
                    .Where(item => item.Powierzchn > area1)
                    .Where(item => item.Powierzchn < area2)
                    .Select(item => item.Geom)
                    .ToListAsync();
                Geometry specifiedGreenery = UnaryUnionOp.Union(greeneryCollection.Result);
                var drillingsCollection = SearchedSet.ToListAsync();
                var drillingIds = new List<short?>();
                foreach(var drilling in drillingsCollection.Result)
                {
                    if (Within(drilling, specifiedGreenery))
                        drillingIds.Add(drilling.Id);
                }
                SearchedSet = SearchedSet.Where(item => drillingIds.Contains(item.Id));
            }
        }

        private bool Within(Odwiert drilling, Geometry geometry)
        {
            Point point = new Point(drilling.EurefY.Value, drilling.EurefX.Value);
            return point.Within(geometry);
        }

        private void SearchByDepth()
        {
            var glebokoscZwierciadla1 = Preferences.GlebokoscZwierciadla1;
            var glebokoscZwierciadla2 = Preferences.GlebokoscZwierciadla2;
            if (glebokoscZwierciadla1.HasValue || glebokoscZwierciadla2.HasValue)
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
                SearchedSet = SearchedSet
                    .Where(item => nrRbdhList.Contains(item.NrRbdh));
            }
        }

        private void SearchByFiltering()
        {
            var filter1 = Preferences.Filtracja1;
            var filter2 = Preferences.Filtracja2;
            if (filter1.HasValue || filter2.HasValue)
            {
                if (filter1 > filter2)
                {
                    decimal? temp = filter1;
                    filter1 = filter2;
                    filter2 = temp;
                }
                SearchedSet = SearchedSet
                    .Where(item => item.WspFiltracji > filter1)
                    .Where(item => item.WspFiltracji < filter2);
            }
        }

        private void SearchByY()
        {
            if (Preferences.EurefY1.HasValue || Preferences.EurefY2.HasValue)
                SearchedSet = SearchedSet
                    .Where(item => item.EurefY > Preferences.EurefY1)
                    .Where(item => item.EurefY < Preferences.EurefY2);
        }

        private void SearchByX()
        {
            if (Preferences.EurefX1.HasValue || Preferences.EurefX2.HasValue)
                SearchedSet = SearchedSet
                    .Where(item => item.EurefX > Preferences.EurefX1)
                    .Where(item => item.EurefX < Preferences.EurefX2);
        }

        private void SearchByRbdh()
        {
            if (Preferences.NrRbdh.HasValue)
                SearchedSet = SearchedSet
                    .Where(item => item.NrRbdh.Equals(Preferences.NrRbdh));
        }

        private void SearchByName()
        {
            if (Preferences.NazwaObiektu != null && !Preferences.NazwaObiektu.Equals(""))
                SearchedSet = SearchedSet
                    .Where(item => item.NazwaObiektu.Equals(Preferences.NazwaObiektu));
        }
    }
}
