using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace GreenMap
{
    public partial class Dzielnice
    {
        public int Objectid { get; set; }
        public int? Mslink { get; set; }
        public int? Mapid { get; set; }
        public int? IdDzielnicy { get; set; }
        public string NrDzielnicy { get; set; }
        public double? Powierzchnia { get; set; }
        public string Nazwa { get; set; }
        public string NazwaPelna { get; set; }
        public string Opis { get; set; }
        public DateTime? DataAktualna { get; set; }
        public int? MiastoId { get; set; }
        public Polygon Geom { get; set; }
    }
}
