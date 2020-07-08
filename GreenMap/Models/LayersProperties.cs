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
            { "mapa bazowa", new Layer{ Color = "Gray", Opacity = 1, Text="White"} },
            { "granice miasta", new Layer{ Color = "OrangeRed", Opacity = 0, Stroke="OrangeRed", Text="White"} },
            { "dzielnice", new Layer{ Color = "Gold", Opacity = 0.5, Stroke="Gold", Text="Black"} },
            { "tereny zielone", new Layer{ Color = "LimeGreen", Opacity = 0.7, Stroke="Green", Text="Black"} },
            { "cieki/zbiorniki wodne", new Layer{ Color = "Aquamarine", Opacity = 1, Text="Black"} },
            { "zwierciadło wód podziemnych", new Layer{ Color = "DarkBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoI", new Layer{ Color = "PowderBlue", Opacity = 1, Text="Black", Cluster=true, Radius=11} },
            //{ "zwierciadłoII", new Layer{ Color = "Cyan", Opacity = 1, Text="Black", Cluster=true, Radius=11} },
            //{ "zwierciadłoIII", new Layer{ Color = "DarkTurquoise", Opacity = 1, Text="Black", Cluster=true, Radius=11} },
            //{ "zwierciadłoIV", new Layer{ Color = "DodgerBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoV", new Layer{ Color = "DarkCyan", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoVI", new Layer{ Color = "Blue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoVII", new Layer{ Color = "MediumSlateBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoVIII", new Layer{ Color = "DarkBlue", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            //{ "zwierciadłoIX", new Layer{ Color = "Black", Opacity = 1, Text="White", Cluster=true, Radius=11} },
            { "obiekty RBDH", new Layer{ Color = "DarkGoldenRod", Opacity = 1, Text="White", Cluster=true, Radius=10} },
            { "otwory hydrogeologiczne", new Layer{ Color = "Red", Opacity = 1, Text="White", Cluster=true, Radius=9} },
        };
    }
}
