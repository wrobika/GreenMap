using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var objects = await _context.CbdhObj.ToListAsync();
            var wkt = objects.ConvertAll(new Converter<CbdhObj, string>(ToWkt));
            return wkt;
        }

        private static string ToWkt(CbdhObj cbdh)
        {
            if (cbdh.Geom != null)
                return cbdh.Geom.ToString();
            else
                return "GEOMETRY EMPTY";
        }



        // GET: api/CbdhObj/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CbdhObj>> GetCbdhObj(decimal id)
        {
            var cbdhObj = await _context.CbdhObj.FindAsync(id);

            if (cbdhObj == null)
            {
                return NotFound();
            }

            return cbdhObj;
        }
    }
}
