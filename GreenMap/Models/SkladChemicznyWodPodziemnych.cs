using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class SkladChemicznyWodPodziemnych
    {
        [Display(Name = "Symbol punktu")]
        public string SymbolPunktu { get; set; }

        [Display(Name = "Współrzędne")]
        public double? X { get; set; }
        public double? Y { get; set; }
        public decimal? RzednaTerenu { get; set; }

        [Display(Name = "Data badania")]
        public DateTime? DataBadania { get; set; }

        [Display(Name = "pH")]
        public decimal? Ph { get; set; }

        [Display(Name = "PEW")]
        public decimal? Pew { get; set; }

        [Display(Name = "SAR")]
        public decimal? Sar { get; set; }

        [Display(Name = "Klasa jakości")]
        public string KlasaJakosci { get; set; }

        [Display(Name = "Przydatność do nawadniania")]
        public string PrzydatnoscDoNawadniania { get; set; }
        
        public string K { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public Point Geom { get; set; }
    }
}
