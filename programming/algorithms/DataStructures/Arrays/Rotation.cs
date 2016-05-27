using System;
using NUnit.Framework;

namespace algorithms.Arrays
{
    public class Rotation
    {
        public int[,] Rotate(int[,] values)
        {
            var size = values.GetLength(0);
            for (var layer = 0; layer < size / 2; layer++)
            {
                int first = layer;
                int last = size - 1 - layer;

                for (var i = first; i < last; i++)
                {
                    int offset = i - first;
                    int top = values[first, i]; // save top

                    // left -> top
                    values[first, i] = values[last - offset, first];

                    // bottom -> left
                    values[last - offset, first] = values[last, last - offset];

                    // right -> bottom
                    values[last, last - offset] = values[i, last];

                    // top -> right
                    values[i,last] = top; // right <- saved top
                }
            }

            return values;
        }


        [Test]
        public void Test()
        {
            Assert.That(1/2, Is.EqualTo(1));
        }
        [Test]
        public void TestRotate()
        {
            // 1 , 2
            // 3,  4

            // 3 , 1
            // 4 , 2

            var input = new int[,] {{1, 2}, {3, 4} };
            var expected = new int[,] { { 3, 1 }, { 4, 2 } };
            Rotate(input);
            Assert.That(input, Is.EqualTo(expected));
            // 1 , 2 , 3
            // 4 , 5 , 6
            // 7 , 8 , 9

            // 7 , 4, 1 
            // 8 , 5, 2
            // 9 , 6, 3

            input = new int[,] {{1, 2, 3}, {4, 5, 6}, {7, 8 ,9} };
            expected = new int[,] { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };
            Rotate(input);
            Assert.That(input, Is.EqualTo(expected));
           
            // 1 , 2 , 3, 10
            // 4 , 5 , 6, 11
            // 7 , 8 , 9, 12
            // 13, 14, 15,16

            // 13, 7, 4, 1 
            // 14, 8 , 5, 2
            // 15, 9, 6, 3
            // 16, 12, 11, 10

            input = new int[,] {{1, 2, 3, 10}, {4, 5, 6, 11}, {7, 8, 9, 12}, {13, 14, 15, 16}};
            expected = new int[,] { { 13, 7, 4, 1 }, { 14, 8, 5, 2 }, { 15, 9, 6, 3 }, { 16, 12, 11, 10 } };
            Rotate(input);

            Assert.That(input, Is.EqualTo(expected));
        }
    }
}