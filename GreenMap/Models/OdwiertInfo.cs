using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMap.Models
{
    public class OdwiertInfo
    {
        [Display(Name = "Nazwa obiektu")]
        public string NazwaObiektu { get; set; }

        [Display(Name = "Numer wg RBDH")]
        public int? NrRbdh { get; set; }

        public string Lokalizacja { get; set; }

        [Display(Name = "Status otworu")]
        public string Status { get; set; }

        public double? EurefX { get; set; }

        public double? EurefY { get; set; }
    }
}
