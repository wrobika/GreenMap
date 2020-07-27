using System;
using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class GraniceMiasta
    {
        public int Objectid { get; set; }
        public int? Mslink { get; set; }
        public int? Mapid { get; set; }
        public string Nazwa { get; set; }
        public DateTime? DataAktualna { get; set; }
        public Polygon Geom { get; set; }
    }
}
