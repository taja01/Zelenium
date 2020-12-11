using System;
using System.Globalization;

namespace ZeleniumFramework.Utils
{
    public static class Parse
    {
        private const int ROUNDING_PRECISION = 2;
        public static decimal Round(decimal input) => Math.Round(input, ROUNDING_PRECISION, MidpointRounding.AwayFromZero);
        public static double Round(double input) => Math.Round(input, ROUNDING_PRECISION, MidpointRounding.AwayFromZero);

        public static decimal ToDecimal(string text, bool invariantCulture = false)
        {
            var ci = invariantCulture
                ? CultureInfo.InvariantCulture
                : CultureInfo.CurrentCulture;

            if (text.Length == 0)
            {
                return 0m;
            }

            if (decimal.TryParse(text, NumberStyles.Number, ci, out var result))
            {
                return result;
            }

            throw new FormatException($"Parse failed: '{text}' is not possible to parse as decimal");
        }

        public static int ToInt(string text)
        {
            if (int.TryParse(text, out var result))
            {
                return result;
            }

            throw new FormatException($"Parse failed: '{text}' is not possible to parse as int");
        }

        public static DateTime ToDateTime(string text)
        {
            if (DateTime.TryParse(text, out var result))
            {
                return result;
            }

            throw new FormatException($"Parse failed: '{text}' is not possible to parse as DateTime");
        }
    }
}
