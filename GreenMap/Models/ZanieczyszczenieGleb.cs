using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class ZanieczyszczenieGleb
    {
        public short? Lp { get; set; }
        public string Symbol { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }
        public string GrupaGruntow { get; set; }
        public DateTime? DataOprobowania { get; set; }
        public string ZanieczyszczenieGleby0025 { get; set; }
        public string SubstancjeStwarzajaceRyzyko0025 { get; set; }
        public string ZanieczyszczenieGruntu025100 { get; set; }
        public string SubstancjeStwarzajaceRyzyko025100 { get; set; }
        public string ZanieczyszczenieGruntu100 { get; set; }
        public string SubstancjeStwarzajaceRyzyko100 { get; set; }
        public Point Geom { get; set; }
    }
}
