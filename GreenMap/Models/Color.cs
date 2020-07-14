using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenMap.Models
{
    public class Color
    {
        public static string GetDepthColor(int maxDepth, int minDepth, int actualDepth)
        {
            int range = maxDepth - minDepth;
            int value = actualDepth - minDepth;

            int green = (255 * value) / range; // calculate green (the closer the value is to max, the greener it gets)
            int blue = 255 - green; // set red as inverse of green

            return "rgb(0," + green + "," + blue + ")";
        }

        public static string GetFilteringColor(string filteringClass)
        {
            if (filteringClass.Equals("A")) return "DarkBlue";
            if (filteringClass.Equals("B")) return "Blue";
            if (filteringClass.Equals("C")) return "Yellow";
            if (filteringClass.Equals("D")) return "Orange";
            if (filteringClass.Equals("E")) return "Red";
            else return "Black";
        }
        public static string GetDepthPointColor(decimal depth)
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

        public static Dictionary<decimal, string> DepthRanges = new Dictionary<decimal, string>
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
