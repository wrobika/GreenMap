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
            { "mapa", new Layer{ Color = "Gray", Opacity = 1, Text="White"} },
            { "miasto", new Layer{ Color = "OrangeRed", Opacity = 0.7, Text="White"} },
            { "dzielnice", new Layer{ Color = "Gold", Opacity = 0.5, Stroke="Gold", Text="Black"} },
            { "zieleń", new Layer{ Color = "LimeGreen", Opacity = 0.7, Stroke="Green", Text="Black"} },
            { "cieki/zbiorniki", new Layer{ Color = "Aquamarine", Opacity = 1, Text="Black"} },
            { "zwierciadło", new Layer{ Color = "DarkBlue", Opacity = 1, Text="White"} },
            { "obiekty", new Layer{ Color = "DarkGoldenRod", Opacity = 1, Text="White"} },
            { "odwierty", new Layer{ Color = "Red", Opacity = 1, Text="White"} },
        };
    }
}
