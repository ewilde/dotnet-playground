// -----------------------------------------------------------------------
// <copyright file="StandardQueue.cs">
// Copyright (c) 2014.
// </copyright>
// -----------------------------------------------------------------------
namespace algorithms.Queue
{
    using System.Collections.Generic;

    using NUnit.Framework;

    /// <summary>
    /// A queue represents a FIFO data structure. A common way to implement a queue is using a linked list
    /// </summary>
    public class StandardQueue<T>
    {
        private readonly LinkedList<T> data;

        public StandardQueue()
        {
            this.data = new LinkedList<T>();
        }

        public StandardQueue<T> Enque(T item)
        {
            this.data.AddLast(item);
            return this;
        }

        public T Deque()
        {
            var item = this.data.First;
            this.data.RemoveFirst();
            return item.Value;
        }
    }

    #region Tests

    [TestFixture]
    public class StandardQueueTests
    {
        [Test]
        public void Enqueuing_returns_queue()
        {
            var queue = new StandardQueue<int>();
            Assert.That(queue.Enque(3), Is.EqualTo(queue));
        }

        [Test]
        public void Dequeuing_returns_enqueued_item_in_fifo_order()
        {
            var queue = new StandardQueue<int>();
            queue.Enque(3).Enque(2).Enque(1);

            Assert.That(queue.Deque(), Is.EqualTo(3));
            Assert.That(queue.Deque(), Is.EqualTo(2));
            Assert.That(queue.Deque(), Is.EqualTo(1));
        }
    }

    #endregion

}