using System;
using System.Collections.Generic;

namespace GreenMap.Models
{
    public partial class ZwierciadloGl
    {
        public long? Objectid { get; set; }
        public int? DzielnicaId { get; set; }
        public int? NrRbdh { get; set; }
        public decimal? GlNawiercona { get; set; }
        public decimal? GlUstabilizowana { get; set; }
        public double? EurefX { get; set; }
        public double? EurefY { get; set; }
        public decimal? WspFiltracji { get; set; }
        public string NrKlasy { get; set; }
        public string NazwaKlasy { get; set; }
    }
}
