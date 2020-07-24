using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class ZanieczyszczenieGleb
    {
        public short? Lp { get; set; }
        
        [Display(Name = "Symbol punktu")]
        public string Symbol { get; set; }
        
        [Display(Name = "Współrzędne")]
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }
        
        [Display(Name = "Grupa gruntów")]
        public string GrupaGruntow { get; set; }

        [Display(Name = "Data opróbowania")]
        public DateTime? DataOprobowania { get; set; }

        [Display(Name = "Zanieczyszczenie gleby (0-0,25 m ppt)")]
        public string ZanieczyszczenieGleby0025 { get; set; }

        [Display(Name = "Substancje stwarzające ryzyko")]
        public string SubstancjeStwarzajaceRyzyko0025 { get; set; }

        [Display(Name = "Zanieczyszczenie gruntu (0,25-1,0 m ppt)")]
        public string ZanieczyszczenieGruntu025100 { get; set; }

        [Display(Name = "Substancje stwarzające ryzyko")]
        public string SubstancjeStwarzajaceRyzyko025100 { get; set; }

        [Display(Name = "Zanieczyszczenie gruntu (>1,0 m ppt)")]
        public string ZanieczyszczenieGruntu100 { get; set; }

        [Display(Name = "Substancje stwarzające ryzyko")]
        public string SubstancjeStwarzajaceRyzyko100 { get; set; }
        
        public Point Geom { get; set; }
    }
}
