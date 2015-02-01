using System;
using NUnit.Framework;

namespace algorithms.Maths
{
    public class Fibonacci
    {
        public int CalcuateRecursively(int n)
        {
            return (n < 2) ? n : CalcuateRecursively(n - 1) + CalcuateRecursively(n - 2);
        }

        public int CalculateIteratively(int n)
        {
            int fibPrevious = 0, fibNext = 1;
            for (int i = 2; i <= n; i++)
            {
                int tmp = fibPrevious;
                fibPrevious = fibNext;
                fibNext = tmp + fibNext;
            }
            return (n > 0 ? fibNext : 0);
        }

        public int CalculateUsingBinet(int n)
        {
            double sqrt5 = Math.Sqrt(5.0);
            double phi = (1 + sqrt5) / 2;
            return (int)((Math.Pow(phi, n) - Math.Pow(1 - phi, n)) / sqrt5);
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
            Assert.That(new Fibonacci().CalculateIteratively(8), Is.EqualTo(21));
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
    }
}