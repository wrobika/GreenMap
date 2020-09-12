using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DzielniceController : ControllerBase
    {
        private readonly epionierContext _context;

        public DzielniceController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/Dzielnice
        [HttpGet]
        public async Task<Dictionary<string, string>> GetDzielnice()
        {
            var wktWithName = await _context.Dzielnice
                .Where(item => item.Geom != null)
                .ToDictionaryAsync(item => item.Geom.ToText(),
                    item => item.NazwaPelna);
            return wktWithName;
        }

        public async Task<ActionResult<string>> GetName(int? id)
        {
            if (!id.HasValue)
                return new EmptyResult();
            var dzielnice = await _context.Dzielnice.FindAsync(id.Value);
            if (dzielnice == null)
                return new EmptyResult();
            return dzielnice.NazwaPelna;
        }

        public async Task<SelectList> GetSelectList()
        {
            var possibleDistricts = await _context.Dzielnice
                .Select(item => new SelectListItem 
                { 
                    Text=item.Nazwa, 
                    Value=item.Objectid.ToString()
                })
                .ToListAsync();
            List<SelectListItem> districtList = new List<SelectListItem>();
            districtList.Add(new SelectListItem { Selected=true, Text = "", Value = "" });
            districtList.AddRange(possibleDistricts);
            return new SelectList(districtList, "Value", "Text");
        }
    }
}
