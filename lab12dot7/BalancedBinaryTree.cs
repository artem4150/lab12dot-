using ClassLibrary1;
using System;
using System.Collections.Generic;

namespace ClassLibraryLab10
{
    
    public class Node<T> where T : IComparable, ICloneable
    {
        public T Data { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }

        public Node()
        {
            Data = default(T);
            Left = null;
            Right = null;
        }

        public Node(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        

        public int CompareTo(T other)
        {
            return other == null ? 1 : Data.CompareTo(other);
        }

        public override bool Equals(object obj)
        {
            return obj is Node<T> node &&
                   EqualityComparer<T>.Default.Equals(Data, node.Data) &&
                   EqualityComparer<Node<T>>.Default.Equals(Left, node.Left) &&
                   EqualityComparer<Node<T>>.Default.Equals(Right, node.Right);
        }

        
    }

    
    public class BinaryTree<T> where T : IInit, IComparable, ICloneable, new()
    {
        private Node<T> _root = null;
        private int _count = 0;

        public Node<T> Root => _root;
        public int Count => _count;

        public BinaryTree() { }

        public BinaryTree(int size)
        {
            _count = size;
            _root = CreateBalancedTree(size);
        }

        private Node<T> CreateBalancedTree(int size)
        {
            if (size == 0) return null;

            T data = new T();
            data.RandomInit();
            var node = new Node<T>(data);

            int leftSize = size / 2;
            int rightSize = size - leftSize - 1;

            node.Left = CreateBalancedTree(leftSize);
            node.Right = CreateBalancedTree(rightSize);

            return node;
        }

        private void Display(Node<T> node, int style, int indent = 5)
        {
            if (node != null)
            {
                Display(node.Left, 1, indent + 5);
                Console.Write(new string(' ', indent));
                if (style == 1) Console.Write("┌-->");
                else if (style == 2) Console.Write("└-->");
                Console.WriteLine(node.Data);
                Display(node.Right, 2, indent + 5);
            }
        }

        public void BalanceTree()
        {
            if (_root == null) return;

            List<T> elements = new List<T>();
            InOrderTraversal(_root, elements);
            _root = BuildBalancedTree(elements, 0, elements.Count - 1);
        }

        private void InOrderTraversal(Node<T> node, List<T> elements)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, elements);
            elements.Add(node.Data);
            InOrderTraversal(node.Right, elements);
        }

        private Node<T> BuildBalancedTree(List<T> elements, int start, int end)
        {
            if (start > end) return null;

            int mid = (start + end) / 2;
            Node<T> node = new Node<T>(elements[mid])
            {
                Left = BuildBalancedTree(elements, start, mid - 1),
                Right = BuildBalancedTree(elements, mid + 1, end)
            };

            return node;
        }

        public void PrintTree()
        {
            Display(_root, 0);
        }

        public void AddNode(T data)
        {
            Node<T> current = _root;
            Node<T> parent = null;
            bool found = false;

            while (current != null && !found)
            {
                parent = current;
                int result = current.CompareTo(data);
                if (result == 0)
                {
                    found = true;
                }
                else if (result > 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            if (!found)
            {
                var newNode = new Node<T>(data);
                if (parent == null)
                {
                    _root = newNode;
                }
                else if (parent.CompareTo(data) > 0)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
                _count++;
            }
        }



        public T FindMaxNode()
        {
            if (_root == null)
            {
                throw new InvalidOperationException("Дерево пустое.");
            }

            Node<T> current = _root;
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Data;
        }

        private void ConvertToArray(Node<T> node, T[] array, ref int index)
        {
            if (node != null)
            {
                ConvertToArray(node.Left, array, ref index);
                array[index++] = node.Data;
                ConvertToArray(node.Right, array, ref index);
            }
        }

        public void TransformToFindTree()
        {
            T[] elements = new T[_count];
            int index = 0;
            ConvertToArray(_root, elements, ref index);

            _root = new Node<T>(elements[0]);
            _count = 1;
            for (int i = 1; i < elements.Length; i++)
            {
                AddNode(elements[i]);
            }
        }
        public int GetElementCount()
        {
            return _count;
        }
        private Node<T> RemoveNode(Node<T> node, T key)
        {
            if (node == null) return null;

            if (node.CompareTo(key) > 0)
            {
                node.Left = RemoveNode(node.Left, key);
            }
            else if (node.CompareTo(key) < 0)
            {
                node.Right = RemoveNode(node.Right, key);//
            }
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;

                node.Data = FindMin(node.Right);
                node.Right = RemoveNode(node.Right, node.Data);
            }
            return node;
        }

        private T FindMin(Node<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node.Data;
        }

        public void DeleteNode(T key)
        {
            _root = RemoveNode(_root, key);
            _count--;
        }

        

        private bool SearchNode(Node<T> node, T key)
        {
            if (node == null) return false;

            int result = node.CompareTo(key);
            if (result == 0) return true;
            else if (result > 0) return SearchNode(node.Left, key);
            else return SearchNode(node.Right, key);
        }

        

        public void CopyTree(BinaryTree<T> source)
        {
            if (source == null || source.Root == null)
            {
                _root = null;
                _count = 0;
                return;
            }

            _root = CopyNodes(source.Root);
            _count = source.Count;
        }
        public void DeepDelete()
        {
            _root = DeepDeleteNode(_root);
            _count = 0;
        }

        private Node<T> DeepDeleteNode(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            
            node.Left = DeepDeleteNode(node.Left);
            node.Right = DeepDeleteNode(node.Right);

            
            node = null;

            return node;
        }
        private Node<T> CopyNodes(Node<T> node)
        {
            if (node == null) return null;

            var newNode = new Node<T>((T)node.Data.Clone())
            {
                Left = CopyNodes(node.Left),
                Right = CopyNodes(node.Right)
            };
            return newNode;
        }
    }
}
