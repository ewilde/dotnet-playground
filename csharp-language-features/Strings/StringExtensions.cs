using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Edward.Wilde.CSharp.Features.Strings
{
    public static class StringExtensions
    {
        public static bool StartsWithNumber(this string source)
        {
            return Char.IsDigit(source.ToCharArray()[0]);
        }

        public static int ToInt(this string source)
        {
            string a = source;
            string b = string.Empty;

            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            if (b.Length > 0)
                return int.Parse(b);

            return 0;
        }

        public static string PascalCaseToWords(this string source)
        {
            var sb = new StringBuilder();
            var firstWord = true;

            foreach (var match in Regex.Matches(source, "([A-Z][a-z]+)|[0-9]+"))
            {
                if (firstWord)
                {
                    sb.Append(match);
                    firstWord = false;
                }
                else
                {
                    sb.Append(" ");
                    sb.Append(match.ToString().ToLower());
                }
            }

            return sb.ToString();
        }

        public static string ExtractVersion(this string source)
        {
            var result = new StringBuilder();

            for (int i = 0; i < source.Length; i++)
            {
                var value = source[i];

                if ((!Char.IsDigit(value) && value != '.') && result.Length > 0)
                    break;

                if (Char.IsDigit(value) || (value == '.' && result.Length > 0))
                {
                    result.Append(value);
                }
            }

            return result.ToString().TrimEnd('.');
        }

        public static bool Contains(this string source, string value, StringComparison comparison)
        {
            switch (comparison)
            {
            case StringComparison.CurrentCultureIgnoreCase:
            case StringComparison.InvariantCultureIgnoreCase:
            case StringComparison.OrdinalIgnoreCase:
                if (source == null)
                {
                    return false;
                }

                if (value == null)
                {
                    return false;
                }

                return source.ToLower().Contains(value.ToLower());
            default:
                return source.Contains(value);
            }
        }
    }
}