using System;
using System.Diagnostics;
using algorithms.DataStructures;
using log4net;
using NUnit.Framework;

namespace algorithms.Maths
{
    public class Fibonacci
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Fibonacci));

        public Fibonacci()
        {
        }

        public int CalcuateRecursively(int n)
        {
            return (n < 2) ? n : CalcuateRecursively(n - 1) + CalcuateRecursively(n - 2);
        }

        public int CalculateIteratively(int n)
        {
            var watch = new Stopwatch();
            watch.Start();
            int fibPrevious = 0, fibNext = 1;
            for (int i = 2; i <= n; i++)
            {
                int tmp = fibPrevious;
                fibPrevious = fibNext;
                fibNext = tmp + fibNext;
                //Log.DebugFormat("{0} ticks. [CalculateIteratively]", watch.ElapsedTicks);
            }

            var result = (n > 0 ? fibNext : 0);
            Log.DebugFormat("Completed in {0} ticks. [CalculateIteratively]", watch.ElapsedTicks);
            return result;
        }

        public long CalculateUsingBinet(int n)
        {
            var watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            double sqrt5 = Math.Sqrt(5.0);
            double phi = (1 + sqrt5)/2;
            var result = (long) ((Math.Pow(phi, n) - Math.Pow(1 - phi, n))/sqrt5);

            Log.DebugFormat("Completed in {0} ticks. [CalculateUsingBinet]", watch.ElapsedTicks);
            return result;
        }
    }

    [TestFixture]
    public class FibonacciTests
    {
        [Test]
        public void iterative_8th_fib()
        {
            Assert.That(new Fibonacci().CalculateIteratively(8), Is.EqualTo(21));
        }

        [Test]
        public void iterative_38th_fib()
        {
            Assert.That(new Fibonacci().CalculateIteratively(38), Is.EqualTo(39088169));
        }

        [Test]
        public void recursive_8th_fib()
        {
            Assert.That(new Fibonacci().CalcuateRecursively(8), Is.EqualTo(21));
        }

        [Test]
        public void binet_8th_fib()
        {
            Assert.That(new Fibonacci().CalculateUsingBinet(8), Is.EqualTo(21));
        }

        [Test]
        public void binet_38th_fib()
        {
            Assert.That(new Fibonacci().CalculateUsingBinet(38), Is.EqualTo(39088169));
        }

        [Test]
        public void binet_1000th_fib()
        {
            Assert.That(new Fibonacci().CalculateUsingBinet(1000), Is.GreaterThan(39088169));
        }

        [Test]
        public void iterative_1000th_fib()
        {
            Assert.That(new Fibonacci().CalculateIteratively(1000), Is.GreaterThan(39088169));
        }
    }
}