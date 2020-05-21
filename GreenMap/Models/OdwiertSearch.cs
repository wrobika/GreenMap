using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Display(Name = "Status otworu")]
        public string Status { get; set; }

        [Display(Name = "Współrzędna X")]
        [Range(233811, 251375, ErrorMessage = "Współrzędna X musi mieścić się w granicach miasta (przedział 233811-251375)")]
        public double? EurefX1 { get; set; }
        [Range(233811, 251375, ErrorMessage = "Współrzędna X musi mieścić się w granicach miasta (przedział 233811-251375)")]
        public double? EurefX2 { get; set; }

        [Display(Name = "Współrzędna Y")]
        [Range(556746, 587120, ErrorMessage = "Współrzędna Y musi mieścić się w granicach miasta (przedział 556746-587120)")]
        public double? EurefY1 { get; set; }
        [Range(556746, 587120, ErrorMessage = "Współrzędna Y musi mieścić się w granicach miasta (przedział 556746-587120)")]
        public double? EurefY2 { get; set; }

        [Display(Name = "Głębokość do zwierciadła [m]")]
        [Range(-1000, 0, ErrorMessage = "Głębokość musi mieścić się w zakresie od -1000 do 0")]
        public decimal? GlebokoscZwierciadla1 { get; set; }
        [Range(-1000, 0, ErrorMessage = "Głębokość musi mieścić się w zakresie od -1000 do 0")]
        public decimal? GlebokoscZwierciadla2 { get; set; }

        [Display(Name = "Współczynnik filtracji [m/s]")]
        [Range(0, 1000, ErrorMessage = "Współczynnik filtracji musi mieścić się z zakresie od 0 do 1000")]
        public decimal? Filtracja1 { get; set; }
        public decimal? Filtracja2 { get; set; }

        [Display(Name = "Klasyfikacja hydrogeologiczna gleby/gruntu")]
        public string HydroGleby { get; set; }

        [Display(Name = "Zanieczyszczenie gleby/gruntu")]
        public string ZanieczyszczenieGleby { get; set; }

        [Display(Name = "Klasa jakości wody")]
        public string JakoscWody { get; set; }

        [Display(Name = "Przydatnosć do nawodnienia")]
        public string Nawodnienie { get; set; }
    }
}
