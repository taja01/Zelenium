using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZeleniumFramework.Utils
{
    public static class StringExtensions
    {
        private const CompareOptions COMPARE_OPTIONS = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;
        private const string REG_EX = "^[A-Za-z-.]+\\.[A-Za-z-.]+$";
        public static bool HasInterpolation(this string input)
        {
            return input.Contains("{") || input.Contains("}");
        }

        public static bool IsCrsKeyLike(this string input)
        {
            var trimmed = input.Trim();
            return new Regex(REG_EX).Match(trimmed).Success;
        }

        public static bool HasHtmlTag(this string input)
        {
            var tagRegex = new Regex(@"<[^>]+>");
            return tagRegex.IsMatch(input);
        }

        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            var ci = CultureInfo.CurrentCulture.CompareInfo;
            return ci.IndexOf(source, toCheck, COMPARE_OPTIONS) != -1;
        }

        public static bool HasLowerLetter(this string text) => text.Any(char.IsLower);
    }
}
