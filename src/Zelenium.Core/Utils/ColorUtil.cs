using System;
using System.Drawing;
using System.Globalization;
using System.Linq;

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
            double lum1 = GetLuminance(color1); double lum2 = GetLuminance(color2);

            double lighter = Math.Max(lum1, lum2);
            double darker = Math.Min(lum1, lum2);

            double contrastRatio = (lighter + 0.05) / (darker + 0.05);

            return Math.Round(contrastRatio, 2, MidpointRounding.AwayFromZero);
        }

        public static double GetLuminance(string color)
        {
            return GetLuminance(ParseColor(color));
        }

        public static double GetLuminance(Color color, Color? background = null)
        {
            Color bgColor = background ?? Color.White;

            if (color.A < 255)
            {
                double alpha = color.A / 255.0;

                int compositeR = (int)Math.Round((alpha * color.R) + ((1 - alpha) * bgColor.R));
                int compositeG = (int)Math.Round((alpha * color.G) + ((1 - alpha) * bgColor.G));
                int compositeB = (int)Math.Round((alpha * color.B) + ((1 - alpha) * bgColor.B));

                color = Color.FromArgb(255, compositeR, compositeG, compositeB);
            }

            double rs = color.R / 255.0;
            double gs = color.G / 255.0;
            double bs = color.B / 255.0;

            double r = (rs <= 0.03928) ? rs / 12.92 : Math.Pow((rs + 0.055) / 1.055, 2.4);
            double g = (gs <= 0.03928) ? gs / 12.92 : Math.Pow((gs + 0.055) / 1.055, 2.4);
            double b = (bs <= 0.03928) ? bs / 12.92 : Math.Pow((bs + 0.055) / 1.055, 2.4);

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
            if (string.IsNullOrWhiteSpace(cssColor))
                throw new ArgumentNullException(nameof(cssColor));

            cssColor = cssColor.Trim();

            if (cssColor.StartsWith('#'))
            {
                return ColorTranslator.FromHtml(cssColor);
            }
            else if (cssColor.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
            {
                int left = cssColor.IndexOf('(');
                int right = cssColor.IndexOf(')');

                if (left < 0 || right < 0 || right <= left)
                {
                    throw new FormatException("Invalid rgb/rgba format: Missing or misordered parentheses.");
                }

                string noBrackets = cssColor.Substring(left + 1, right - left - 1);

                var parts = noBrackets
                                .Split([','], StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToArray();

                if (parts.Length < 3)
                {
                    throw new FormatException("Invalid rgb/rgba format: Not enough color components.");
                }

                if (!int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int r) ||
                    !int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int g) ||
                    !int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int b))
                {
                    throw new FormatException("Invalid rgb values.");
                }

                if (parts.Length == 3)
                {
                    return Color.FromArgb(r, g, b);
                }
                else if (parts.Length >= 4)
                {
                    if (!float.TryParse(parts[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float a))
                    {
                        throw new FormatException("Invalid alpha value.");
                    }
                    return Color.FromArgb((int)(a * 255), r, g, b);
                }
            }

            throw new FormatException("Color string must be in hex, rgb, or rgba format.");
        }

        public static Color Blend(Color color1, Color color2)
        {
            decimal a1 = color1.A / 255m; decimal a2 = color2.A / 255m;
            decimal aOut = a1 + a2 * (1 - a1);

            if (aOut == 0)
            {
                return Color.FromArgb(0, 0, 0, 0);
            }

            int r = (int)Math.Round(((color1.R * a1) + (color2.R * a2 * (1 - a1))) / aOut, MidpointRounding.AwayFromZero);
            int g = (int)Math.Round(((color1.G * a1) + (color2.G * a2 * (1 - a1))) / aOut, MidpointRounding.AwayFromZero);
            int b = (int)Math.Round(((color1.B * a1) + (color2.B * a2 * (1 - a1))) / aOut, MidpointRounding.AwayFromZero);
            int aFinal = (int)Math.Round(aOut * 255, MidpointRounding.AwayFromZero);

            return Color.FromArgb(aFinal, r, g, b);
        }

        public static string ToHexString(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
        }

        public static string ToRgbString(Color color)
        {
            return $"RGBA({color.R},{color.G},{color.B},{color.A})";
        }
    }
}
