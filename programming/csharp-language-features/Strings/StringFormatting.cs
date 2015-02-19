using System;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Strings
{
    [TestFixture]
    public class StringFormatting
    {
        [Test]
        public void Numbers()
        {
            Console.WriteLine(string.Format("{0:n}", 6123 / 2.0));      // decimals
            Console.WriteLine(string.Format("{0:n0}", 6123 / 2.0));     // no decimals
        }
    }
}