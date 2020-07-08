using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMap.Models
{
    public class LayersProperties
    {
        public static Dictionary<string, Layer> Layers = new Dictionary<string, Layer>
        {
            { "background", new Layer{ FullName = "mapa bazowa", Visible = true, Color = "Gray", Opacity = 1, Text="White"} },
            { "city", new Layer{ FullName = "granice miasta", Visible = true, Color = "OrangeRed", Opacity = 0, Stroke="OrangeRed", Text="White"} },
            { "district", new Layer{ FullName = "dzielnice", Visible = true, Color = "Gold", Opacity = 0.5, Stroke="Gold", Text="Black"} },
            { "greenery", new Layer{ FullName = "tereny zielone", Visible = true, Color = "LimeGreen", Opacity = 0.7, Stroke="Green", Text="Black"} },
            { "hydro", new Layer{ FullName = "cieki/zbiorniki wodne", Visible = true, Color = "Aquamarine", Opacity = 1, Text="Black"} },
            { "mirror", new Layer{ FullName = "zwierciadło wód podziemnych", Visible = false, Color = "DarkBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoI", new Layer{ Color = "PowderBlue", Opacity = 1, Text="Black", Cluster=true, Radius=11} },
            //{ "zwierciadłoII", new Layer{ Color = "Cyan", Opacity = 1, Text="Black", Cluster=true, Radius=11} },
            //{ "zwierciadłoIII", new Layer{ Color = "DarkTurquoise", Opacity = 1, Text="Black", Cluster=true, Radius=11} },
            //{ "zwierciadłoIV", new Layer{ Color = "DodgerBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoV", new Layer{ Color = "DarkCyan", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoVI", new Layer{ Color = "Blue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoVII", new Layer{ Color = "MediumSlateBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoVIII", new Layer{ Color = "DarkBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoIX", new Layer{ Color = "Black", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            { "rbdh", new Layer{ FullName = "baza otworów badawczych", Visible = true, Color = "DarkGoldenRod", Opacity = 1, Text="White", Cluster=true, Radius=10} },
            { "drilling", new Layer{ FullName = "typ otworu", Visible = true, Color = "Red", Opacity = 1, Text="White", Cluster=true, Radius=9} },
        };
    }
}
