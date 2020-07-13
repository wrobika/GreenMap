using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace GreenMap.Models
{
    public partial class Zielen
    {
        public long Objectid { get; set; }
        public int? OgrFid { get; set; }
        public int? Id { get; set; }
        public int? Mslink { get; set; }
        public int? Mapid { get; set; }
        public string IdWydz { get; set; }
        public string IdFito { get; set; }
        public double? Powierzchn { get; set; }
        public string Part { get; set; }
        public double? Numer { get; set; }
        public string Arkusz { get; set; }
        public string Lokalizacj { get; set; }
        public string SzGeograf { get; set; }
        public string DlGeograf { get; set; }
        public string Opis { get; set; }
        public string Uwagi { get; set; }
        public string Forma { get; set; }
        public string Uzasd { get; set; }
        public string Gatunek { get; set; }
        public int? TypAgreg { get; set; }
        public string TypAgreg1 { get; set; }
        public int? TypNr { get; set; }
        public string Typ { get; set; }
        public int? WalorNr { get; set; }
        public string Walor { get; set; }
        public string PodsPraw { get; set; }
        public string DataAktua { get; set; }
        public int? DzielnicaId { get; set; }
        public MultiPolygon Geom { get; set; }
    }
}
