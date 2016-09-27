using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace algorithms.DataStructures.Stack
{
    public class StackUsingArray<T>
    {
        private int head;
        private T[] data;

        public StackUsingArray()
        {
            head = -1;
            data = new T[4];
        }

        public void Push(T item)
        {
            head++;
            EnsureCapacity();

            data[head] = item;
        }

        public T Pop()
        {
            var item = data[head];
            data[head] = default(T); // deference incase T is a reference type?
            head--;

            return item;
        }

        public T Peek()
        {
            return data[head];
        }

        private void EnsureCapacity()
        {
            if (data.Length == head)
                Array.Resize(ref data, data.Length * 2);
        }
    }

    public class StackUsingArrayTests
    {
        [Test]
        public void Push()
        {
            var stack = new StackUsingArray<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.That(stack.Peek(), Is.EqualTo(3));
        }

        [Test]
        public void Pop()
        {
            var stack = new StackUsingArray<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            Assert.That(stack.Pop(), Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(2));
        }

        [Test]
        public void Resize()
        {
            var stack = new StackUsingArray<int>();

            for(int i = 1; i <= 100; i++)
            {
                stack.Push(i);                
            }

            Assert.That(stack.Peek(), Is.EqualTo(100));
        }
    }
}