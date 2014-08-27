using System;
using System.Diagnostics;
using NUnit.Framework;

namespace algorithms.Sorting
{
    /// <summary>
    /// Conceptually, a merge sort works as follows
    /// Divide the unsorted list into n sublists, each containing 1 element (a list of 1 element is considered sorted).
    /// Repeatedly merge sublists to produce new sorted sublists until there is only 1 sublist remaining. This will be the sorted list.
    /// </summary>
    /// <code>
    /// Psuedo code: where A is an array to be sorted
    /// 
    /// Sort(Array A) {
    ///     halfLen ← A.size
    ///     left ← new Array[halfLen]
    ///     right ← new Array[A.size - halfLen]
    /// 
    ///     Array.Copy(A, left, left.size)
    ///     Array.Copy(A, sourceIndex: halfLen, destArray: right, length: right.size)
    /// 
    ///     left = Sort(left)
    ///     right = Sort(right)
    ///     
    ///     return Merge(left, right);
    /// }
    /// 
    /// Merge(Array A, Array B) {
    ///   result = new Array[A.size + B.size]
    /// 
    ///   aPos, bPos = 0
    /// 
    ///   for i = 0 to A.size {
    ///      ....
    ///   }
    /// }
    /// </code>
    /// <typeparam name="T"></typeparam>
    public class MergeSort<T>  where T : IComparable
    {
        private int length;

        public T[] Sort(T[] values)
        {
            if (values.Length <= 1)
            {
                return values;
            }

            length = values.Length/2;
            var left = new T[length]; var right = new T[values.Length - length];
            Array.Copy(values, left, length);
            Array.Copy(values, length, right, 0, values.Length - length);

            left = Sort(left);
            right = Sort(right);

            return Merge(left, right);
        }

        private T[] Merge(T[] left, T[] right)
        {
            var result = new T[left.Length + right.Length];

            var k = 0; var j = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if ((j >= right.Length) ||
                    (k < left.Length &&
                    (left[k].CompareTo(right[j]) <= 0)))
                {
                    result[i] = left[k];
                    k = k + 1;
                }
                else if (j < right.Length)
                {
                    result[i] = right[j];
                    j = j + 1;
                }
                else
                {
                    Debug.Assert(false);
                }
            }

            return result;
        }
    }

    [TestFixture]
    public class MergeSortTest
    {
        [Test]
        public void Sorted_case()
        {
            int[] values = Data.GetSortedInteger(600000, @ascending: true);

            var time = new Stopwatch();
            time.Start();
            var sortedValues = new MergeSort<int>().Sort(values);

            time.Stop();
            Console.WriteLine("Best case: {0} ms", time.ElapsedMilliseconds);

            // Assert.That(sortedValues, Is.Ordered);
            // Assert.That(sortedValues, Is.EquivalentTo(values));
        }
        [Test]
        public void Unsorted_case()
        {
            int[] values = Data.GetSortedInteger(600000, @ascending: false);

            var time = new Stopwatch();
            time.Start();
            var sortedValues = new MergeSort<int>().Sort(values);

            time.Stop();
            Console.WriteLine("Best case: {0} ms", time.ElapsedMilliseconds);

            // Assert.That(sortedValues, Is.Ordered);
            // Assert.That(sortedValues, Is.EquivalentTo(values));
        }
    }
}