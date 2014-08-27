// -----------------------------------------------------------------------
// <copyright file="QueueFromStack.cs">
// Copyright (c) 2014.
// </copyright>
// -----------------------------------------------------------------------
namespace algorithms.Queue
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    /// <summary>
    /// Implement a FIFO queue only using two stacks
    /// </summary>
    public class QueueFromStack<T>
    {
        private readonly Stack<T> enqueStack;
        private readonly Stack<T> dequeStack;

        public QueueFromStack()
        {
            this.enqueStack = new Stack<T>();
            this.dequeStack = new Stack<T>();
        }

        public QueueFromStack<T> Enque(T item)
        {
            this.enqueStack.Push(item);
            return this;
        }

        public T Deque()
        {
            if (this.dequeStack.Count == 0)
            {
                foreach (var item in this.enqueStack)
                {
                    this.dequeStack.Push(item);
                }
            }

            return this.dequeStack.Pop();
        }
    }

    #region Tests

    [TestFixture]
    public class QueueFromStackTests
    {
        [Test]
        public void Enqueuing_returns_queue()
        {
            var queue = new QueueFromStack<int>();
            Assert.That(queue.Enque(3), Is.EqualTo(queue));
        }

        [Test]
        public void Dequeuing_returns_enqueued_item_in_fifo_order()
        {
            var queue = new QueueFromStack<int>();
            queue.Enque(3).Enque(2).Enque(1);

            Assert.That(queue.Deque(), Is.EqualTo(3));
            Assert.That(queue.Deque(), Is.EqualTo(2));
            Assert.That(queue.Deque(), Is.EqualTo(1));
        }
    }

    #endregion
    
}