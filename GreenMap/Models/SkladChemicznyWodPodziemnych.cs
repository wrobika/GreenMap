using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class SkladChemicznyWodPodziemnych
    {
        public string SymbolPunktu { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public decimal? RzednaTerenu { get; set; }
        public DateTime? DataBadania { get; set; }
        public decimal? Ph { get; set; }
        public decimal? Pew { get; set; }
        public decimal? Sar { get; set; }
        public string KlasaJakosci { get; set; }
        public string PrzydatnoscDoNawadniania { get; set; }
        public string K { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public Point Geom { get; set; }
    }
}
