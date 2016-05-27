using System.Collections.Generic;
using NUnit.Framework;

namespace algorithms.DataStructures
{
    public class LinkedListExamples
    {
        public void RemoveDuplicate<T>(LinkedList<T> list)
        {
            var items = new HashSet<T>();
            var node = list.First;

            while (node != null)
            {
                var current = node;
                node = node.Next;

                if (items.Contains(current.Value))
                    list.Remove(current);
                else
                    items.Add(current.Value);                
            }
        }
        
        [Test()]
        public void TestRemoveDuplicates()
        {
            var input = new LinkedList<int>(new[] {1, 2, 3, 1, 1, 3});

            RemoveDuplicate(input);

            Assert.That(input, Is.EqualTo(new LinkedList<int>(new [] {1,2,3})));
        }

        public bool IsPalindrome<T>(LinkedList<T> value)
        {
            var forward = value.First;
            var backward = value.Last;

            while (forward != null)
            {
                if (forward.Value.ToString() != backward.Value.ToString())
                    return false;

                if (forward.Next == backward.Previous) // at mid-point
                    break;

                forward = forward.Next;
                backward = backward.Previous;
            }

            return true;
        }

        [Test]
        public void TestIsPalindrome()
        {
            //Assert.That(IsPalindrome(new LinkedList<int>(new [] {1,2,3,2,1})), Is.True);
            //Assert.That(IsPalindrome(new LinkedList<int>(new [] {1,2,3,2})), Is.False);
           // Assert.That(IsPalindrome(new LinkedList<char>(new [] {'f', 'o', 'o'})), Is.False);
            Assert.That(IsPalindrome(new LinkedList<char>(new [] {'f', 'o', 'z', 'o', 'f'})), Is.True);
            Assert.That(IsPalindrome(new LinkedList<char>(new[] {'s', 't', 'e', 'p', ' ', 'o', 'n', ' ', 'n', 'o', ' ', 'p', 'e', 't', 's'})), Is.True);
        }
    }
}