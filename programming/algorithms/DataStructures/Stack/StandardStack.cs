using System.Collections.Generic;
using NUnit.Framework;

namespace algorithms.DataStructures.Stack
{
    /// <summary>
    /// A queue represents a FILO data structure. A common way to implement a queue is using a linked list
    /// </summary>
    public class StandardStack<T>
    {
        private readonly LinkedList<T> items;

        public StandardStack()
        {
            items = new LinkedList<T>();
        }

        public void Push(T item)
        {
            items.AddFirst(item);
        }

        public T Pop()
        {
            var item = items.First;
            items.RemoveFirst();
            return item.Value;
        }

        public T Peek()
        {
            return items.First.Value;
        }
    }

    public class StandardStackTests
    {
        [Test]
        public void Push()
        {
            var stack = new StandardStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.That(stack.Peek(), Is.EqualTo(3));
        }

        [Test]
        public void Pop()
        {
            var stack = new StandardStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.That(stack.Pop(), Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(2));
        }
    }
}