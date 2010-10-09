using System.Text.RegularExpressions;

namespace AutoBuild.Core
{
    public static class StringExtensions
    {
        public static string With(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool Matches(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
    }
}