using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace GreenMap
{
    public partial class CbdhObj
    {
        public decimal Objectid { get; set; }
        public decimal? OtworId { get; set; }
        public decimal? NumerUjecia { get; set; }
        public decimal? NumerWRbdh { get; set; }
        public decimal? DzielnicaId { get; set; }
        public Geometry Geom { get; set; }
    }
}
