using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMap.Models
{
    public class DepthColor
    {
        public static string GetColor(decimal depth)
        {
            if (depth > -0.5M)
                return "rgb(0,255,255)";
            if (depth > -1M)
                return "rgb(0,192,255)";
            if (depth > -2M)
                return "rgb(0,128,255)";
            if (depth > -5M)
                return "rgb(0,64,255)";
            if (depth > -10M)
                return "rgb(0,0,255)";
            if (depth > -15M)
                return "rgb(0,0,192)";
            if (depth > -20M)
                return "rgb(0,0,128)";
            if (depth > -30M)
                return "rgb(0,0,64)";
            else
                return "rgb(0,0,0)";
        }
        public static Dictionary<decimal, string> Ranges = new Dictionary<decimal, string>
        {
            { -0.5M, "rgb(0,255,255)" },
            { -1M, "rgb(0,192,255)" },
            { -2M, "rgb(0,128,255)" },
            { -5M, "rgb(0,64,255)" },
            { -10M, "rgb(0,0,255)" },
            { -15M, "rgb(0,0,192)" },
            { -20M, "rgb(0,0,128)" },
            { -30M, "rgb(0,0,64)" },
            { -1000M , "rgb(0,0,0)"},
        };
    }
}
