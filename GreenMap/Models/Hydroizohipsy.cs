using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class Hydroizohipsy
    {
        public string Recnum { get; set; }
        public int? ZwWody { get; set; }
        public LineString Geom { get; set; }
    }
}
