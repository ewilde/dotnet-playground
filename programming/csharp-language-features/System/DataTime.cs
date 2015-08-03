using System;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features
{
    [TestFixture]
    public class DataTimeExamples
    {
        [Test]
        public void Milliseconds()
        {
            Console.Write(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fffffff"));
        }
    }
}