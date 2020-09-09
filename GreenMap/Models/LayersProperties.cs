using System.Collections.Generic;

namespace GreenMap.Models
{
    public class LayersProperties
    {
        private static readonly string urlWMS = "http://40.69.47.110//geoserver/wms";

        public static Dictionary<string, Layer> Layers = new Dictionary<string, Layer>
        {
            { "background", new Layer{ FullName = "podkład mapowy", Visible = true, Color = "Gray", Opacity = 1, Text="White"} },
            { "city", new Layer{ FullName = "granice miasta", Visible = true, Color = "OrangeRed", Opacity = 0, Stroke="OrangeRed", Text="White"} },
            { "district", new Layer{ FullName = "granice dzielnic", Visible = true, Color = "Gold", Opacity = 0.5, Text="Black"} },
            { "NMT", new Layer{ FullName = "numeryczny model terenu (NMT)", Visible = false, Color = "LawnGreen", Opacity = 1, Text = "Black",
                UrlWMS = urlWMS, LayersWMS = "NMT_KRK:NMT_KRK_2000"} },
            { "hydro", new Layer{ FullName = "cieki/zbiorniki wodne", Visible = true, Color = "Aquamarine", Opacity = 1, Text="Black"} },
            { "greenery", new Layer{ FullName = "tereny zielone", Visible = true, Color = "LimeGreen", Opacity = 0.7, Text="Black"} },
            { "soil", new Layer{ FullName = "typy gleb", Visible = false, Color = "MediumSlateBlue", Opacity = 1, Text="White", 
                UrlWMS = urlWMS, LayersWMS = "gleby:gleby_Biezanow_Prokocim,gleby:gleby_Bieñczyce,gleby:gleby_Czyżyny,gleby:gleby_NHuta,gleby:gleby_Podgorze"} },
            { "drilling", new Layer{ FullName = "baza otworów badawczych", Visible = true, Color = "Red", Opacity = 1, Text="White", Cluster=true, Radius=10, IconShapePoints=0} },
            { "hydroizohypse", new Layer{ FullName = "zwierciadło wód podziemnych", Visible = false, Color = "DarkBlue", Opacity = 1, Text="White", Cluster=false} },
            { "filter", new Layer{ FullName = "klasa wodoprzepuszczalności gruntów", Visible = false, Color = "Silver", Opacity = 1, Text="Black", Cluster=true, Radius=11, IconShapePoints=0} },
            { "monitoring", new Layer{ FullName = "monitoring środowiska", Visible = false, Color = "Violet", Opacity = 1, Text="Black", Cluster=true, Radius = 10, IconShapePoints=0} },
            { "soilPollution", new Layer{ FullName="zanieczyszczenie gleb i gruntów", Visible = false, Color="Salmon", Opacity=1, Text="White", Cluster=true, Radius = 14, IconShapePoints=3} },
            { "groundwaterChemistry", new Layer{ FullName="skład chemiczny wód podziemnych", Visible=false, Color="Yellow", Opacity=1, Text="Black", Cluster=true, Radius = 11, IconShapePoints=4 } },   
        };
    }
}
