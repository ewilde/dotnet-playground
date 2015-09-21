using System;
using System.Diagnostics;
using System.Threading;
using Edward.Wilde.CSharp.Features.Utilities;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Diagnostics
{
    public static class PerformanceCounterExamples
    {
        private static readonly Process CurrentProcess = System.Diagnostics.Process.GetCurrentProcess();
       
        public static void MeasureTotalCpu()
        {
            using (var pc = new PerformanceCounter(
                    "Processor Information",
                    "% Processor Time",
                    "_Total"))
            {
                pc.NextValue();
                Thread.Sleep(1000);
                var nextValue = pc.NextValue();
                
                Console.WriteLine(nextValue.ToPercentage());
            }
        }
        public static void MeasureProcessCpu()
        {
            using (var pc = new PerformanceCounter(
                    "Process",
                    "% Processor Time",
                    CurrentProcess.ProcessName))
            {
                pc.NextValue();
                Thread.Sleep(1000);
                var nextValue = pc.NextValue();
                Console.WriteLine(nextValue.ToPercentage());
            }
        }

        public static void MeasurePrivateBytesConsumed()
        {
            using (var pc = new PerformanceCounter(
                    "Process", 
                    "Private Bytes",
                    CurrentProcess.ProcessName))
            {
                Console.WriteLine(pc.NextValue().ToMegabytes().ToString("n2") + " MB");
            }
        }

        public static void MeasureGen0Collections()
        {
            using (var pc = new PerformanceCounter(
                    ".NET CLR Memory",
                    "# Gen 0 Collections",
                    CurrentProcess.ProcessName))
            {
                Console.WriteLine(pc.NextValue());
            }
        }
    }

    [TestFixture]
    public class PerformanceCounterExamplesTests
    {
        [Test]
        public void MeasureCountersTest()
        {
            PerformanceCounterExamples.MeasureTotalCpu();
            PerformanceCounterExamples.MeasureProcessCpu();
            PerformanceCounterExamples.MeasurePrivateBytesConsumed();
            PerformanceCounterExamples.MeasureGen0Collections();
        }
    }

}