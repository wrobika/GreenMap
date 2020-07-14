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
        public async Task<Dictionary<string, string>> GetHydroizohipsy()
        {
            int maxDepth = await _context.Hydroizohipsy
                .MaxAsync(item => item.ZwWody.Value);
            int minDepth = await _context.Hydroizohipsy
                .MinAsync(item => item.ZwWody.Value);
            var wktWithColor = await _context.Hydroizohipsy
                .Where(item => item.ZwWody.HasValue)
                .ToDictionaryAsync(item => item.Geom.ToText(),
                    item => DepthColor.GetColor(maxDepth, minDepth, item.ZwWody.Value));
            return wktWithColor;
        }
    }
}