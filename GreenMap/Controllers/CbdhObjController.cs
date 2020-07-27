using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap.Models;
using Microsoft.AspNetCore.Authorization;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CbdhObjController : ControllerBase
    {
        private readonly epionierContext _context;

        public CbdhObjController(epionierContext context)
        {
            _context = context;
        }

        // GET: api/CbdhObj
        [HttpGet]
        public async Task<List<string>> GetCbdhObj()
        {
            var wkt = await _context.CbdhObj
                .Where(item => item.Geom != null)
                .Select(item => item.Geom.ToString())
                .ToListAsync();
            return wkt;
        }
    }
}
