using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using NUnit.Framework;

namespace Edward.Wilde.CSharp.Features.Timers
{

    public class HighResolution
    {

        public static void DisplayTimerProperties()
        {
            // Display the timer frequency and resolution. 
            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Operations timed using the system's high-resolution performance counter.");
            }
            else
            {
                Console.WriteLine("Operations timed using the DateTime class.");
            }

            long frequency = Stopwatch.Frequency;
            Console.WriteLine("  Timer frequency in ticks per second = {0}",
                frequency);
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;
            Console.WriteLine("  Timer is accurate within {0} nanoseconds",
                nanosecPerTick);
        }

        public static void ShowDateTimeHasResolutionOf15Milliseconds()
        {
            String output = "";
            for (int ctr = 0; ctr <= 20; ctr++)
            {
                output += String.Format("{0}\n", DateTime.Now.Millisecond);
                // Introduce a delay loop. 
                for (int delay = 0; delay <= 1000; delay++)
                { }

                if (ctr == 10)
                {
                    output += "Thread.Sleep called...\n";
                    Thread.Sleep(5);
                }
            }
            Console.WriteLine(output);
        }

        public static void ShowQueryPerformanceCounterHasBetterResolutionThanDateTimeNow()
        {
            for (int i = 0; i < 100; ++i)
            {
                var t1 = DateTime.UtcNow;
                Thread.Sleep(0);
                var t2 = DateTime.UtcNow;

                Console.WriteLine("UtcNow elapsed = " + (t2 - t1).Ticks);
            }

            for (int i = 0; i < 100; ++i)
            {
                long q1, q2;

                QueryPerformanceCounter(out q1);
                Thread.Sleep(0);
                QueryPerformanceCounter(out q2);

                Console.WriteLine("QPC elapsed = " + (q2 - q1));
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
    }

    [TestFixture]
    public class HighResolutionTests
    {
        [Test]
        public void DisplayTimerProperties()
        {
            HighResolution.DisplayTimerProperties();
        }

        [Test]
        public void ShowDateTimeHasResolutionOf15Milliseconds()
        {
            HighResolution.ShowDateTimeHasResolutionOf15Milliseconds();
        }

        [Test]
        public void ShowQueryPerformanceCounterHasBetterResolutionThanDateTimeNow()
        {
            HighResolution.ShowQueryPerformanceCounterHasBetterResolutionThanDateTimeNow();
        }
    }
}