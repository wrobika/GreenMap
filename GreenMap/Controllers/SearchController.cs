using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using GreenMap.Models;
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
        private OdwiertSearch _preferences;
        private IQueryable<Odwiert> _searchedSet;
        private readonly ZielenController _zielenController;
        private readonly ZwierciadloGlController _zwierciadloGlController;


        public SearchController(epionierContext context)
        {
            _context = context;
            _searchedSet = _context.Odwiert;
            _zielenController = new ZielenController(context);
            _zwierciadloGlController = new ZwierciadloGlController(context);
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<long?, string>>> SetPreferencesAndSearch(
            [Bind("NazwaObiektu,NrRbdh,Lokalizacja,Status,EurefX1,EurefX2,EurefY1,EurefY2,GlebokoscZwierciadla1,GlebokoscZwierciadla2,KlasaFiltracji,Filtracja1,Filtracja2,PowierzchniaZieleni1,PowierzchniaZieleni2")] 
            OdwiertSearch preferences)
        {
            _preferences = preferences;
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

            return await OdwiertController.GetWktWithId(_searchedSet);
        }

        private void SearchByStatus()
        {
            if (_preferences.Status != null && !_preferences.Status.Equals(""))
                _searchedSet = _searchedSet
                    .Where(item => item.Status.Equals(_preferences.Status));
        }

        private void SearchByFilteringClass()
        {
            if (_preferences.KlasaFiltracji != null && !_preferences.KlasaFiltracji.Equals(""))
                _searchedSet = _searchedSet
                    .Where(item => item.NazwaKlasy.Equals(_preferences.KlasaFiltracji));
        }

        private void SearchByDistrict()
        {
            if (_preferences.Lokalizacja != null && !_preferences.Lokalizacja.Equals(""))
                _searchedSet = _searchedSet
                    .Where(item => item.DzielnicaId.ToString().Equals(_preferences.Lokalizacja));
        }

        private void SearchByGreeneryArea()
        {
            var area1 = _preferences.PowierzchniaZieleni1;
            var area2 = _preferences.PowierzchniaZieleni2;
            if (area1.HasValue || area2.HasValue)
            {
                var greeneryCollection = _zielenController.GetSpecifiedArea(area1.Value, area2.Value);
                Geometry specifiedGreenery = UnaryUnionOp.Union(greeneryCollection.Result);
                var drillingsCollection = _searchedSet.ToListAsync();
                var drillingIds = new List<short?>();
                foreach(var drilling in drillingsCollection.Result)
                {
                    if (Within(drilling, specifiedGreenery))
                        drillingIds.Add(drilling.Id);
                }
                _searchedSet = _searchedSet.Where(item => drillingIds.Contains(item.Id));
            }
        }

        private bool Within(Odwiert drilling, Geometry geometry)
        {
            Point point = new Point(drilling.EurefY.Value, drilling.EurefX.Value);
            return point.Within(geometry);
        }

        private void SearchByDepth()
        {
            var glebokoscZwierciadla1 = _preferences.GlebokoscZwierciadla1;
            var glebokoscZwierciadla2 = _preferences.GlebokoscZwierciadla2;
            if (glebokoscZwierciadla1.HasValue || glebokoscZwierciadla2.HasValue)
            {
                List<int?> nrRbdhList = _zwierciadloGlController
                    .GetMirrorWithinRange(glebokoscZwierciadla1.Value, glebokoscZwierciadla2.Value).Result;
                _searchedSet = _searchedSet
                    .Where(item => nrRbdhList.Contains(item.NrRbdh));
            }
        }

        private void SearchByFiltering()
        {
            var filter1 = _preferences.Filtracja1;
            var filter2 = _preferences.Filtracja2;
            if (filter1.HasValue || filter2.HasValue)
            {
                if (filter1 > filter2)
                {
                    decimal? temp = filter1;
                    filter1 = filter2;
                    filter2 = temp;
                }
                _searchedSet = _searchedSet
                    .Where(item => item.WspFiltracji > filter1)
                    .Where(item => item.WspFiltracji < filter2);
            }
        }

        private void SearchByY()
        {
            if (_preferences.EurefY1.HasValue || _preferences.EurefY2.HasValue)
                _searchedSet = _searchedSet
                    .Where(item => item.EurefY > _preferences.EurefY1)
                    .Where(item => item.EurefY < _preferences.EurefY2);
        }

        private void SearchByX()
        {
            if (_preferences.EurefX1.HasValue || _preferences.EurefX2.HasValue)
                _searchedSet = _searchedSet
                    .Where(item => item.EurefX > _preferences.EurefX1)
                    .Where(item => item.EurefX < _preferences.EurefX2);
        }

        private void SearchByRbdh()
        {
            if (_preferences.NrRbdh.HasValue)
                _searchedSet = _searchedSet
                    .Where(item => item.NrRbdh.Equals(_preferences.NrRbdh));
        }

        private void SearchByName()
        {
            if (_preferences.NazwaObiektu != null && !_preferences.NazwaObiektu.Equals(""))
                _searchedSet = _searchedSet
                    .Where(item => item.NazwaObiektu.Equals(_preferences.NazwaObiektu));
        }
    }
}
