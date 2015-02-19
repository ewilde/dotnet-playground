using System;
using System.Diagnostics;
using Edward.Wilde.CSharp.Features.Utilities;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Diagnostics
{
    public static class PerformanceCounterExamples
    {
        private static readonly Process CurrentProcess = System.Diagnostics.Process.GetCurrentProcess();
        public static void MeasurePrivateBytesConsumed()
        {
            using (var pc = new PerformanceCounter(
                    "Process", 
                    "Private Bytes",
                    CurrentProcess.ProcessName))
            {
                Console.Write(pc.NextValue().ToMegabytes().ToString("n2") + " MB");
            }
        }
    }

    [TestFixture]
    public class PerformanceCounterExamplesTests
    {
        [Test]
        public void MeasurePrivateBytesConsumedTest()
        {
            PerformanceCounterExamples.MeasurePrivateBytesConsumed();
        }
    }

}