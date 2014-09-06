using System;
using System.Globalization;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Utilities
{
    public static class ParseUtility
    {
        /// <summary>
        /// Parses dates from strings with a mix of characters i.e. ewilde_201401_stuff would return a value of January 1st 2014
        /// </summary>
        /// <param name="value">String representation of a value</param>
        /// <returns>Parsed value</returns>
        public static DateTime ParseDate(string value)
        {
            value = ParseInt(value).ToString(CultureInfo.CurrentCulture);

            var formatter = string.Empty;

            switch (value.Length)
            {
                case 4:
                    formatter = "yyyy";
                    break;
                case 6:
                    formatter = "yyyyMM";
                    break;
                case 8:
                    formatter = "yyyyMMdd";
                    break;
            }

            return DateTime.ParseExact(value, formatter, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static int ParseInt(string value)
        {
            string digits = string.Empty;
            foreach (var character in value)
            {
                if (Char.IsNumber(character))
                {
                    digits += character;
                }
            }

            return int.Parse(digits);
        }
    }

    [TestFixture]
    public class ParseUtilityTests
    {
        [Test]
        public void parse_date_character_start_and_end()
        {
            var date = ParseUtility.ParseDate("ewilde_20140103_foobar");
            Assert.That(date.Year, Is.EqualTo(2014));
            Assert.That(date.Month, Is.EqualTo(1));
            Assert.That(date.Day, Is.EqualTo(3));
        }
    }
}