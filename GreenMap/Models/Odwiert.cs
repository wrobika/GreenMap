namespace GreenMap.Models
{
    public partial class Odwiert
    {
        public short? Id { get; set; }
        public int? NrUjecia { get; set; }
        public int? NrRbdh { get; set; }
        public string NazwaObiektu { get; set; }
        public string Lokalizacja { get; set; }
        public string OkresSpagu { get; set; }
        public string Status { get; set; }
        public double? EurefX { get; set; }
        public double? EurefY { get; set; }
        public long? Objectid { get; set; }
        public int? DzielnicaId { get; set; }
        public decimal? WspFiltracji { get; set; }
        public string NazwaKlasy { get; set; }
        public string NrKlasy { get; set; }
    }
}
