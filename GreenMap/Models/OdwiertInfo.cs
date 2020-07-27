using System.ComponentModel.DataAnnotations;

namespace GreenMap.Models
{
    public class OdwiertInfo
    {
        [Display(Name = "Nazwa obiektu")]
        public string NazwaObiektu { get; set; }

        [Display(Name = "Numer wg RBDH")]
        public int? NrRbdh { get; set; }

        [Display(Name = "Lokalizacja")]
        public string Lokalizacja { get; set; }

        [Display(Name = "Status otworu")]
        public string Status { get; set; }
        
        [Display(Name = "Współrzędne geograficzne EUREF")]
        public string Wspolrzedne { get; set; }

        [Display(Name = "Głębokość do zwierciadła [m]")]
        public decimal? GlebokoscZwierciadla { get; set; }

        [Display(Name = "Współczynnik filtracji [m/s]")]
        public decimal? Filtracja { get; set; }

        [Display(Name = "Klasa filtracji")]
        public string KlasaFiltracji { get; set; }

        [Display(Name = "Klasyfikacja hydrogeologiczna gleby/gruntu")]
        public string HydroGleby { get; set; }

        [Display(Name = "Zanieczyszczenie gleby/gruntu")]
        public string ZanieczyszczenieGleby { get; set; }

        [Display(Name = "Klasa jakości wody")]
        public string JakoscWody { get; set; }

        [Display(Name = "Przydatnosć do nawodnienia")]
        public string Nawodnienie { get; set; }

        public string Profil { get; set; }
    }
}
