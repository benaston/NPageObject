using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace NPageObject.Extensions
{
    public static class ColorStringExtensions
    {
        public static string RgbaToHexColor(this string colour)
        {
            if (Regex.IsMatch(colour, @"^\#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$"))
            {
                return colour;
            }

            if (colour.StartsWith("rgb(") || colour.StartsWith("rgba("))
            {
                colour = colour.Replace("rgb(", "").Replace("rgba(", "").Replace(")", "");
                var rgbValues = colour.Split(',');
                return ColorTranslator.ToHtml(Color.FromArgb(Int32.Parse(rgbValues[0]),
                                                             Int32.Parse(rgbValues[1]),
                                                             Int32.Parse(rgbValues[2])));
            }

            return "";
        }
    }
}