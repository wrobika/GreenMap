using System;
using System.ComponentModel.DataAnnotations;

namespace GreenMap.Models
{
    public class OdwiertSearch
    {
        [Display(Name = "Nazwa obiektu")]
        public string NazwaObiektu { get; set; }

        [Display(Name = "Numer wg RBDH")]
        public int? NrRbdh { get; set; }

        [Display(Name = "Lokalizacja")]
        public string Lokalizacja { get; set; }

        [Display(Name = "Powierzchnia terenu zielonego")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Powierzchnia terenu zielonego musi być większa od 0")]
        public double? PowierzchniaZieleni1 { get; set; }
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Powierzchnia terenu zielonego musi być większa od 0")]
        public double? PowierzchniaZieleni2 { get; set; }

        [Display(Name = "Status otworu")]
        public string Status { get; set; }

        [Display(Name = "Współrzędna X EUREF")]
        [Range(233811, 251375, ErrorMessage = "Współrzędna X musi mieścić się w granicach miasta (przedział 233811-251375)")]
        public double? EurefX1 { get; set; }
        [Range(233811, 251375, ErrorMessage = "Współrzędna X musi mieścić się w granicach miasta (przedział 233811-251375)")]
        public double? EurefX2 { get; set; }

        [Display(Name = "Współrzędna Y EUREF")]
        [Range(556746, 587120, ErrorMessage = "Współrzędna Y musi mieścić się w granicach miasta (przedział 556746-587120)")]
        public double? EurefY1 { get; set; }
        [Range(556746, 587120, ErrorMessage = "Współrzędna Y musi mieścić się w granicach miasta (przedział 556746-587120)")]
        public double? EurefY2 { get; set; }

        [Display(Name = "Głębokość do zwierciadła [m]")]
        [Range(-1000, 0, ErrorMessage = "Głębokość musi mieścić się w zakresie od -1000 do 0")]
        public decimal? GlebokoscZwierciadla1 { get; set; }
        [Range(-1000, 0, ErrorMessage = "Głębokość musi mieścić się w zakresie od -1000 do 0")]
        public decimal? GlebokoscZwierciadla2 { get; set; }

        [Display(Name = "Klasa filtracji")]
        public string KlasaFiltracji { get; set; }

        [Display(Name = "Współczynnik filtracji [m/s]")]
        [Range(0, 1, ErrorMessage = "Współczynnik filtracji musi mieścić się z zakresie od 0 do 1")]
        public decimal? Filtracja1 { get; set; }
        [Range(0, 1, ErrorMessage = "Współczynnik filtracji musi mieścić się z zakresie od 0 do 1")]
        public decimal? Filtracja2 { get; set; }
    }
}
