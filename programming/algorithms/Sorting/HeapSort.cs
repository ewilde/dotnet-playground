using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace algorithms.Sorting
{
    /// <summary>
    /// Heaps have the property of every node being either less than it's parent (max-heap) or greater than is parent (min-heap)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HeapSort<T> : IEnumerable<T> where T : IComparable<T>
    {
        private T[] data;

        public HeapSort(IEnumerable<T> items)
        {
            this.Create(items.ToArray());
        }

        private void Create(T[] items)
        {
            data = items;
            int lastNodeWithChild = (data.Length - 2)/2;

            while (lastNodeWithChild >=0)
            {
                this.FilterDown(lastNodeWithChild);
                lastNodeWithChild--;
            }            
        }

        private void FilterDown(int StartIndex)
        {
            int currentPosition = StartIndex;
            int childPosition = LeftChild(currentPosition);
            T target = this.data[StartIndex];

            while (childPosition < this.data.Length)
            {
                int rightChildPosition = childPosition + 1;
                //  Set ChildPosition to index of smaller of right, left children:
                if ((rightChildPosition < this.data.Length) &&
                  (this.data[rightChildPosition].CompareTo(this.data[childPosition]) <= 0))
                    childPosition = rightChildPosition;

                if (target.CompareTo(this.data[childPosition])<= 0)
                    break;  // Get out of loop, heap OK with Target at CurrentPosition

                // Move value of the smaller child to the parent node:
                this.data[currentPosition] = this.data[childPosition];
                // Move down the tree:
                currentPosition = childPosition;
                childPosition = LeftChild(currentPosition);
            }

            // Put Target into the correct location:
            this.data[currentPosition] = target;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.data.Length; i++)
            {
                yield return this.data[i];                
            }
        }   

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int Parent(int child)
        {
            return (child - 1) / 2;
        }

        private int RightChild(int parent)
        {
            return (2 * parent) + 2;
        }

        private  int LeftChild(int parent)
        {
            return (2 * parent) + 1;
        }
    }

    [TestFixture]
    public class HeapSortTests
    {
        [Test]
        public void Unsorted_array_is_sorted()
        {
            var items = new string[] {"p", "s", "c", "k", "m", "l", "a", "x", "e"};
            var heapSort = new HeapSort<string>(items);

            Assert.That(heapSort.ToArray(), Is.EqualTo(new[] { "a", "e", "c", "k", "m", "l", "p", "x", "s" }));
        }
    }
}