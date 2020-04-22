using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace GreenMap
{
    public partial class Hydro
    {
        public double Objectid { get; set; }
        public int? Counter { get; set; }
        public string Kod { get; set; }
        public DateTime? Dtm { get; set; }
        public string KodNz { get; set; }
        public string ObrNr { get; set; }
        public string ObrNazwa { get; set; }
        public string JeNr { get; set; }
        public string JeNazwa { get; set; }
        public string TypKd { get; set; }
        public string TypNz { get; set; }
        public string ZrdKd { get; set; }
        public string ZrdNz { get; set; }
        public DateTime? Aktualnosc { get; set; }
        public string IdIip { get; set; }
        public int? EsriOid { get; set; }
        public int? DzielnicaId { get; set; }
        public MultiPolygon Geom { get; set; }
    }
}
