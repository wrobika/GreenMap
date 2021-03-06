﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenMap.Models;
using Microsoft.AspNetCore.Authorization;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Hosting;

namespace GreenMap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonitoringController : ControllerBase
    {
        private readonly epionierContext _context;
        private readonly IWebHostEnvironment _env;

        public MonitoringController(epionierContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Monitoring
        [HttpGet]
        public async Task<Dictionary<string, string>> GetMonitoring()
        {
            var wktWithPdfName = await _context.Monitoring
                .Where(item => item.X != null)
                .Where(item => item.Y != null)
                .ToDictionaryAsync(item => new Point(item.X.Value, item.Y.Value).ToText(),
                    item => item.Nazwa);
            return wktWithPdfName;
        }

        [HttpGet("{name}")]
        public IActionResult OpenPdf(string name)
        {
            string path = _env.WebRootPath + "/monitoringPdf/" + name + ".pdf";
            if (System.IO.File.Exists(path))
                return new PhysicalFileResult(path, "application/pdf");
            return Content("Brak informacji z monitoringu dla wybranego punktu");
        }
    }
}