using System;
using System.Drawing;
using System.Globalization;

namespace Zelenium.Core.Utils
{
    public static class ColorUtil
    {
        // https://www.w3.org/TR/WCAG20/#contrast-ratiodef
        // WCAG AA  Normal text: 4.5:1
        // WCAG AAA Normal text: 7:1
        // WCAG AA  Large  text: 3:1
        // WCAG AAA Large  text: 4.5:1

        public static double GetReadability(string color1, string color2)
        {
            return GetReadability(ParseColor(color1), ParseColor(color2));
        }

        public static double GetReadability(Color color1, Color color2)
        {
            var b1 = GetLuminance(color1);
            var b2 = GetLuminance(color2);
            return Parse.Round((Math.Max(b1, b2) + 0.05) /
                   (Math.Min(b1, b2) + 0.05));
        }

        public static double GetLuminance(string color)
        {
            return GetLuminance(ParseColor(color));
        }

        public static double GetLuminance(Color color)
        {
            double rs = (double)color.R / 255;
            double gs = (double)color.G / 255;
            double bs = (double)color.B / 255;
            var r = (rs <= 0.03928) ? rs / 12.92 : Math.Pow((rs + 0.055) / 1.055, 2.4);
            var g = (gs <= 0.03928) ? gs / 12.92 : Math.Pow((gs + 0.055) / 1.055, 2.4);
            var b = (bs <= 0.03928) ? bs / 12.92 : Math.Pow((bs + 0.055) / 1.055, 2.4);

            return 0.2126 * r + 0.7152 * g + 0.0722 * b;
        }

        public static bool IsReadable(string color1, string color2)
        {
            return IsReadable(ParseColor(color1), ParseColor(color2));
        }

        public static bool IsReadable(Color color1, Color color2, double readabilityLevel = Config.ContrastConfig.NORMAL_AAA)
        {
            return GetReadability(color1, color2) >= readabilityLevel;
        }

        public static Color ParseColor(string cssColor)
        {
            cssColor = cssColor.Trim();

            if (cssColor.StartsWith("#"))
            {
                return ColorTranslator.FromHtml(cssColor);
            }

            if (cssColor.StartsWith("rgb")) //rgb or rgba
            {
                var left = cssColor.IndexOf('(');
                var right = cssColor.IndexOf(')');

                if (left < 0 || right < 0)
                {
                    throw new FormatException("rgba format error");
                }

                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                string[] parts = noBrackets.Split(',');

                var r = int.Parse(parts[0], CultureInfo.InvariantCulture);
                var g = int.Parse(parts[1], CultureInfo.InvariantCulture);
                var b = int.Parse(parts[2], CultureInfo.InvariantCulture);

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                if (parts.Length == 4)
                {
                    var a = float.Parse(parts[3], CultureInfo.InvariantCulture);
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }
            throw new FormatException("Not rgb, rgba or hex color string");
        }

        public static Color Blend(Color color1, Color color2)
        {
            static decimal F(decimal channel1, decimal channel2, decimal alpha1, decimal alpha2, decimal unionAlpha)
            {
                return decimal.Round(((channel1 * alpha1) + (channel2 * alpha2) * (1 - alpha1)) / unionAlpha, 0, MidpointRounding.AwayFromZero);
            }

            var a1 = (decimal)color1.A / 255;
            var a2 = (decimal)color2.A / 255;
            var a = a1 + a2 * (1 - a1);

            var r = (int)F(color1.R, color2.R, a1, a2, a);
            var g = (int)F(color1.G, color2.G, a1, a2, a);
            var b = (int)F(color1.B, color2.B, a1, a2, a);
            return Color.FromArgb((int)(a * 255), r, g, b);
        }

        public static string ToHexString(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public static string ToRgbString(Color color)
        {
            return "RGB(" + color.R + "," + color.G + "," + color.B + ")";
        }
    }
}
