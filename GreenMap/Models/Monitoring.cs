using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class Monitoring
    {
        public int? Objectid { get; set; }
        public int? Id { get; set; }
        public string Nazwa { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public Point Geom { get; set; }
    }
}
