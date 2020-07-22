using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMap.Models
{
    public class LayersProperties
    {
        private static readonly string urlWMS = "http://40.69.47.110//geoserver/wms";

        public static Dictionary<string, Layer> Layers = new Dictionary<string, Layer>
        {
            { "background", new Layer{ FullName = "mapa bazowa", Visible = true, Color = "Gray", Opacity = 1, Text="White"} },
            { "city", new Layer{ FullName = "granice miasta", Visible = true, Color = "OrangeRed", Opacity = 0, Stroke="OrangeRed", Text="White"} },
            { "district", new Layer{ FullName = "dzielnice", Visible = true, Color = "Gold", Opacity = 0.5, Text="Black"} },
            { "soil", new Layer{ FullName = "gleby", Visible = false, Color = "MediumSlateBlue", Opacity = 1, Text="White", 
                UrlWMS = urlWMS, LayersWMS = "gleby:gleby_Biezanow_Prokocim,gleby:gleby_Bieñczyce,gleby:gleby_Czyżyny,gleby:gleby_NHuta,gleby:gleby_Podgorze"} },
            { "greenery", new Layer{ FullName = "tereny zielone", Visible = true, Color = "LimeGreen", Opacity = 0.7, Text="Black"} },
            { "hydro", new Layer{ FullName = "cieki/zbiorniki wodne", Visible = true, Color = "Aquamarine", Opacity = 1, Text="Black"} },
            { "filter", new Layer{ FullName = "filtracja", Visible = false, Color = "Silver", Opacity = 1, Text="Black", Cluster=true, Radius=11} },
            { "hydroizohypse", new Layer{ FullName = "zwierciadło wód podziemnych", Visible = false, Color = "DarkBlue", Opacity = 1, Text="White", Cluster=false} },
            { "rbdh", new Layer{ FullName = "baza otworów badawczych", Visible = true, Color = "DarkGoldenRod", Opacity = 1, Text="White", Cluster=true, Radius=10} },
            { "drilling", new Layer{ FullName = "typ otworu", Visible = true, Color = "Red", Opacity = 1, Text="White", Cluster=true, Radius=9} },
        };
    }
}
