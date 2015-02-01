using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace algorithms.DataStructures
{
    public class CircularBuffer<T> : IEnumerable<T>, IEnumerable
    {
        public const int DefaultCapacity = 512;

        private readonly T[] buffer;
        private int head;
        private bool capacityReached;

        public CircularBuffer() : this(DefaultCapacity)
        {
        }

        public CircularBuffer(int capacity)
        {
            this.Capacity = capacity;
            this.buffer = new T[(this.Capacity)];
            this.head = 0;
        }

        public int Capacity { get; set; }

        public int Count
        {
            get { return this.capacityReached ? this.Capacity : this.head; }
        }

        public void Add(T item)
        {
            if (this.head >= this.Capacity)
            {
                this.head = 0;
                this.capacityReached = true;
            }

            this.buffer[head] = item;
            this.head = this.head + 1;
        }

        public T this[int index]
        {
            get
            {
                return this.buffer[index];
            }

            set { this.buffer[index] = value; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new CircularBufferEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new CircularBufferEnumerator<T>(this);
        }

        [Serializable]
        private sealed class CircularBufferEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
        {
            private T[] _array;
            private int _start;
            private int _end;

            internal CircularBufferEnumerator(CircularBuffer<T> circularBuffer)
            {
                this._array = circularBuffer.buffer;
                this._start = 0;
                this._end = circularBuffer.Count;
                this._current = this._start - 1;
            }

            private int _current;

            public T Current
            {
                get
                {
                    if (this._current < this._start)
                        throw new InvalidOperationException("InvalidOperation_EnumNotStarted");
                    if (this._current >= this._end)
                        throw new InvalidOperationException("InvalidOperation_EnumEnded");
                    else
                        return this._array[this._current];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return (object)this.Current;
                }
            }

            public bool MoveNext()
            {
                if (this._current >= this._end)
                    return false;
                ++this._current;
                return this._current < this._end;
            }

            void IEnumerator.Reset()
            {
                this._current = this._start - 1;
            }

            public void Dispose()
            {
            }
        }
    }

    [TestFixture]
    public class CircularBufferTest
    {
        [Test]
        public void can_instantiate_buffer_with_default_fixed_capacity()
        {
            var buffer = new CircularBuffer<int>();
            Assert.That(buffer.Capacity, Is.EqualTo(CircularBuffer<int>.DefaultCapacity));
        }

        [Test]
        public void can_instantiate_buffer_with_fixed_capacity()
        {
            var buffer = new CircularBuffer<int>(128);
            Assert.That(buffer.Capacity, Is.EqualTo(128));
        }

        [Test]
        public void after_instantiation_count_of_buffer_is_zero()
        {
            var buffer = new CircularBuffer<int>();
            Assert.That(buffer.Count, Is.EqualTo(0));
        }

        [Test]
        public void after_adding_an_item_count_is_incremented()
        {
            var buffer = new CircularBuffer<int>();
            var initialCount = buffer.Count;

            buffer.Add(10);

            Assert.That(buffer.Count, Is.EqualTo(initialCount + 1));
        }

        [Test]
        public void can_retreive_an_item_by_index()
        {
            var buffer = new CircularBuffer<int>();
            var firstItem = 10;
            var secondItem = 12;
            var secondIndex = 1;
            buffer.Add(firstItem);
            buffer.Add(secondItem);

            Assert.That(buffer[secondIndex], Is.EqualTo(secondItem));
        }

        [Test]
        public void can_update_an_item_by_index()
        {
            var buffer = new CircularBuffer<int>();
           
            var firstItem = 10;
            var secondItem = 12;
            var updatedSecondItem = 33;
            var secondIndex = 1;
            buffer.Add(firstItem);
            buffer.Add(secondItem);

            buffer[secondIndex] = updatedSecondItem;

            Assert.That(buffer[secondIndex], Is.EqualTo(updatedSecondItem));
        }

        [Test]
        public void can_enumerate_the_buffer()
        {
            var buffer = new CircularBuffer<string> { "A", "B", "C", "D", "E", "F", "G" };
            int charValue = "A".ToCharArray()[0];
            foreach (var item in buffer)
            {
                Assert.That((int)item.ToCharArray()[0], Is.EqualTo(charValue));
                charValue = charValue + 1;
            }
        }

        [Test]
        public void when_capacity_is_reached_adds_to_beginning_of_buffer()
        {
            var buffer = new CircularBuffer<int>(10) { 1, 2, 3, 4, 5, 6, 7, 8 };

            buffer.Add(9);
            buffer.Add(10);
            buffer.Add(11);
            buffer.Add(12);

            Assert.That(buffer[0], Is.EqualTo(11));
            Assert.That(buffer[1], Is.EqualTo(12));
        }
    }
}
