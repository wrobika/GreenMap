using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenMap.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HydroizohipsyController : ControllerBase
    {
        private readonly epionierContext _context;

        public HydroizohipsyController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/Hydro
        [HttpGet]
        public async Task<Dictionary<string, int>> GetHydroizohipsy()
        {
            var wktWithValue = await _context.Hydroizohipsy
                .Where(item => item.ZwWody.HasValue)
                .ToDictionaryAsync(item => item.Geom.ToText(),
                    item => item.ZwWody.Value);
            return wktWithValue;
        }
    }
}