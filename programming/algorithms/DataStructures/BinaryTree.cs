using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net;
using NUnit.Framework;

namespace algorithms.DataStructures
{
    /// <summary>
    /// http://en.wikipedia.org/wiki/Binary_tree
    /// </summary>
    public class BinaryTree<T>
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(BinaryTree<T>));

        public Node<T> Root { get; set; }

        /// <summary>
        /// 1. Display the data part of the root (or current node).
        /// 2. Traverse the left subtree by recursively calling the pre-order function.
        /// 3. Traverse the right subtree by recursively calling the pre-order function.
        /// </summary>
        public void DepthFirst_PreOrder_Traversal_Iterative(Action<Node<T>> visitNode)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(this.Root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                visitNode(node);

                if (node.Right != null)
                    stack.Push(node.Right);

                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }

        /// <summary>
        /// 1. Traverse the left subtree by recursively calling the in-order function.
        /// 2. Display the data part of the root(or current node).
        /// 3. Traverse the right subtree by recursively calling the in-order function.
        /// </summary>
        public void DepthFirst_InOrder_Traversal_Iterative(Action<Node<T>> callback)
        {
            var stack = new Stack<Node<T>>();
            var node = Root;
            while (stack.Count > 0 || node != null)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }
                else
                {
                    node = stack.Pop();
                    callback(node);
                    node = node.Right;
                }
            }
        }

        public void Root_to_leaf_traversal(Action<Tuple<string, Node<T>>> callback)
        {
            var root = Root;
            if (root == null) {
                return;
            }

            var q = new Queue<Tuple<string, Node<T>>>();
            q.Enqueue(new Tuple<string, Node<T>>(root.Value.ToString(),root));
            
            while (q.Count > 0)
            {
                var value =q.Dequeue();
                var head = value.Item2;
                var headPath = value.Item1;

                if (head.Leaf)
                {
                    callback(value);
                    continue;
                }

                if (head.Left != null)
                {
                    String leftStr = headPath + "->" + head.Left.Value;
                    q.Enqueue(new Tuple<string, Node<T>>(leftStr, head.Left));                    
                }

                if (head.Right != null)
                {
                    String rightStr = headPath + "->" + head.Right.Value;
                    q.Enqueue(new Tuple<string, Node<T>>(rightStr, head.Right));                    
                }
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

        public bool Leaf
        {
            get
            {
                return Left == null && Right == null;
            }
        }

        public override string ToString()
        {
            return string.Format("Value: {0}, Left: {1}, Right: {2}, Leaf: {3}", Value, Left, Right, Leaf);
        }
    }

    [TestFixture]
    public class BinaryTreeTest
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(BinaryTreeTest));

        /// <summary>
        ///             1
        ///          /    \
        ///         2      3
        ///        / \     /
        ///       4   10  5
        /// </summary>
        /// <see cref="http://en.wikipedia.org/wiki/Tree_traversal#Types"/>
        [Test]
        public void DepthFirst_PreOrder_Traversal_Iterative()
        {
            var traversed = new List<int>();
            this.CreateTree().DepthFirst_PreOrder_Traversal_Iterative(node=>traversed.Add(node.Value));

            Assert.That(traversed, Is.EquivalentTo(new[] {1, 2, 4, 10, 3, 5}));

        }

        /// <summary>
        ///             1
        ///          /    \
        ///         2      3
        ///        / \     /
        ///       4   10  5
        /// </summary>
        /// <see cref="http://en.wikipedia.org/wiki/Tree_traversal#Types"/>
        [Test]
        public void DepthFirst_InOrder_Traversal_Iterative()
        {
            var traversed = new List<int>();
            this.CreateTree().DepthFirst_InOrder_Traversal_Iterative(node => traversed.Add(node.Value));

            Assert.That(traversed.ToArray(), Is.EqualTo(new int[] { 4, 2, 10, 1, 5, 3}));

        }

        /// <summary>
        ///             1
        ///          /    \
        ///         2      3
        ///        / \     /
        ///       4   10  5
        /// </summary>
        /// <see cref="http://en.wikipedia.org/wiki/Tree_traversal#Types"/>
        [Test]
        public void Print_all_Paths_from_root_to_leaf()
        {
            var traversed = new List<int>();
            this.CreateTree().Root_to_leaf_traversal(tuple => _log.DebugFormat("{0} - {1}", tuple.Item1, tuple.Item2.Value));
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