// -----------------------------------------------------------------------
// <copyright file="StackQuestions.cs">
// Copyright (c) 2014.
// </copyright>
// -----------------------------------------------------------------------
namespace Edward.Wilde.CSharp.Features.InterviewQuestions
{
    using System;
    using System.Collections.Generic;

    using NUnit.Framework;

    /// <summary>
    /// Write an extended stack class that has a function getLargest() for returning the largest element in the stack.
    /// </summary>
    /// <remarks>
    /// What if we push several items in increasing numeric order (e.g. 1,2,3,4...), so that there is a new largest 
    /// after each push()? What if we then pop() each of these items off, so that there is a new largest after each pop()? 
    /// Your algorithm shouldn't pay a steep cost in these edge cases.
    /// 
    /// You should be able to get a runtime of O(1) for push(), pop(), and getLargest().
    /// </remarks>
    public class StackQuestions1<T> where T : IComparable<T>
    {
        private readonly Stack<T> data;
        private readonly Stack<T> largest;

        public StackQuestions1() : this(1)
        {            
        }

        public StackQuestions1(int capacity)            
        {
            this.largest = new Stack<T>();
            this.data = new Stack<T>(capacity);
        }

        public StackQuestions1(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                this.Push(item);
            }
        }

        public StackQuestions1<T> Push(T item)
        {
            if (this.largest.Count == 0 || item.CompareTo(this.largest.Peek()) > 0)
            {
                this.largest.Push(item);
            }

            this.data.Push(item);

            return this;
        }

        public T Pop()
        {
            var item = this.data.Pop();

            if (this.largest.Peek().Equals(item))
            {
                this.largest.Pop();
            }

            return item;
        }

        public T Largest
        {
            get
            {
               return largest.Peek();
            }
        }
    }

    #region Tests
    [TestFixture]
    public class StackQuestions1Tests
    {
        [Test]
        public void Should_return_largest_item()
        {
            var stack = new StackQuestions1<int>(new[] { 30, 3, 20, 99, 4 });

            Assert.That(stack.Largest, Is.EqualTo(99));

            stack.Pop();
            stack.Pop();
            Assert.That(stack.Largest, Is.EqualTo(30));
        }
    }
    #endregion
}