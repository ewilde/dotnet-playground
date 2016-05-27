using System;
using NUnit.Framework;

namespace algorithms.Arrays
{
    public class General
    {
        [Test]
        public void JaggedArray()
        {
            // Jagged, is a multi-dimentional array where each element can have a different length
            int[][] jagged = new int[3][];  // number of first dimensions
            jagged[0] = new int[4] { 1, 2, 3, 4 };
            jagged[1] = new int[2] { 11, 12 };
            jagged[2] = new int[3] { 21, 22, 23 };

            Assert.That(jagged.GetLength(0), Is.EqualTo(3));
            Assert.That(jagged[0].GetLength(0), Is.EqualTo(4));
            Assert.That(jagged[1].GetLength(0), Is.EqualTo(2));
            Assert.That(jagged[2].GetLength(0), Is.EqualTo(3));
        }

        [Test]
        public void MultiDimensionalArray()
        {
            // All elements are of similar size
            int[,] data = new int[2, 2];
            data[0, 0] = 1;
            data[0, 1] = 2;
            data[1, 0] = 3;
            data[1, 1] = 4;

            Assert.That(data.GetLength(0), Is.EqualTo(2));
            Assert.That(data.GetLength(1), Is.EqualTo(2));

        }
    }
}