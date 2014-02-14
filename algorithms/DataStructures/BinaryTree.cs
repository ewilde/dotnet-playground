using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace algorithms.DataStructures
{
    /// <summary>
    /// http://en.wikipedia.org/wiki/Binary_tree
    /// </summary>
    public class BinaryTree<T>
    {
        public Node<T> Root { get; set; }

        public void DepthOrderTraversal(Action<Node<T>> callback)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(this.Root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                callback(node);

                if (node.Right != null)
                    stack.Push(node.Right);

                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }
    }

    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }

        public T Value { get; set; } 
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
    }

    [TestFixture]
    public class BinaryTreeTest
    {
        [Test]
        public void DepthOrderTraversal()
        {
            var traversed = new List<int>();
            this.CreateTree().DepthOrderTraversal(node=>traversed.Add(node.Value));

            Assert.That(traversed, Is.EquivalentTo(new[] { 1,2,4,10,3,5}));

        }

        public BinaryTree<int> CreateTree()
        {
            var tree = new BinaryTree<int>
            {
                Root =
                    new Node<int>(1, 
                        new Node<int>(2, 
                            new Node<int>(4, 
                                    null, 
                                    null), 
                            new Node<int>(10)),
                        new Node<int>(3, 
                            new Node<int>(5), null))
            };
            return tree;
        }
    }
}