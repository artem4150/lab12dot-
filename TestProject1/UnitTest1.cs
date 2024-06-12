using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryLab10;
using System;
using System.Collections.Generic;

namespace BinaryTreeTests
{
    [TestClass]
    public class BinaryTreeTests
    {
        private class TestData : IInit, IComparable, ICloneable
        {
            public int Value { get; set; }

            public void Init()
            {
                // Инициализация данных
                Value = 0;
            }

            public void RandomInit()
            {
                Random rand = new Random();
                Value = rand.Next(100);
            }

            public int CompareTo(object obj)
            {
                if (obj is TestData other)
                {
                    return Value.CompareTo(other.Value);
                }
                throw new ArgumentException("Object is not a TestData");
            }

            public object Clone()
            {
                return new TestData { Value = this.Value };
            }

            public override string ToString()
            {
                return Value.ToString();
            }

            public override bool Equals(object obj)
            {
                return obj is TestData data &&
                       Value == data.Value;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Value);
            }
        }

        [TestMethod]
        public void AddNode_ShouldIncreaseCount()
        {
            var tree = new BinaryTree<TestData>();
            var data = new TestData { Value = 5 };

            tree.AddNode(data);

            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void FindMaxNode_ShouldReturnMaxValue()
        {
            var tree = new BinaryTree<TestData>();
            tree.AddNode(new TestData { Value = 3 });
            tree.AddNode(new TestData { Value = 7 });
            tree.AddNode(new TestData { Value = 5 });

            var maxNode = tree.FindMaxNode();

            Assert.AreEqual(7, maxNode.Value);
        }

        [TestMethod]
        public void DeleteNode_ShouldDecreaseCount()
        {
            var tree = new BinaryTree<TestData>();
            var data1 = new TestData { Value = 3 };
            var data2 = new TestData { Value = 7 };
            tree.AddNode(data1);
            tree.AddNode(data2);

            tree.DeleteNode(data1);

            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void BalanceTree_ShouldBalanceNodes()
        {
            var tree = new BinaryTree<TestData>(7);

            tree.BalanceTree();

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(7, tree.Count);
        }

        [TestMethod]
        public void TransformToFindTree_ShouldRebuildTree()
        {
            var tree = new BinaryTree<TestData>();
            tree.AddNode(new TestData { Value = 5 });
            tree.AddNode(new TestData { Value = 2 });
            tree.AddNode(new TestData { Value = 8 });

            tree.TransformToFindTree();

            Assert.AreEqual(3, tree.Count);
        }

        [TestMethod]
        public void DeepDelete_ShouldRemoveAllNodes()
        {
            var tree = new BinaryTree<TestData>(5);

            tree.DeepDelete();

            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);
        }

        [TestMethod]
        public void CopyTree_ShouldDuplicateTree()
        {
            var tree1 = new BinaryTree<TestData>();
            tree1.AddNode(new TestData { Value = 1 });
            tree1.AddNode(new TestData { Value = 3 });

            var tree2 = new BinaryTree<TestData>();
            tree2.CopyTree(tree1);

            Assert.AreEqual(tree1.Count, tree2.Count);
            Assert.AreEqual(tree1.Root.Data, tree2.Root.Data);
        }

        [TestMethod]
        public void CreateBalancedTree_ShouldReturnBalancedTree()
        {
            var tree = new BinaryTree<TestData>(5);

            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(5, tree.Count);
        }

        [TestMethod]
        public void CompareTo_ShouldReturnCorrectComparison()
        {
            var node1 = new ClassLibraryLab10.Node<TestData>(new TestData { Value = 3 });
            var data2 = new TestData { Value = 5 };

            var result = node1.CompareTo(data2);

            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void Equals_ShouldReturnTrueForEqualNodes()
        {
            var node1 = new ClassLibraryLab10.Node<TestData>(new TestData { Value = 3 });
            var node2 = new ClassLibraryLab10.Node<TestData>(new TestData { Value = 3 });

            Assert.IsTrue(node1.Equals(node2));
        }

        [TestMethod]
        public void PrintTree_ShouldNotThrowException()
        {
            var tree = new BinaryTree<TestData>();
            tree.AddNode(new TestData { Value = 5 });
            tree.AddNode(new TestData { Value = 3 });
            tree.AddNode(new TestData { Value = 7 });

            tree.PrintTree();
        }

        [TestMethod]
        public void GetElementCount_ShouldReturnCorrectCount()
        {
            var tree = new BinaryTree<TestData>();
            tree.AddNode(new TestData { Value = 1 });
            tree.AddNode(new TestData { Value = 2 });
            tree.AddNode(new TestData { Value = 3 });

            var count = tree.GetElementCount();

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void SearchNode_ShouldFindExistingNode()
        {
            var tree = new BinaryTree<TestData>();
            var data = new TestData { Value = 5 };
            tree.AddNode(data);

            bool result = tree.GetType().GetMethod("SearchNode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(tree, new object[] { tree.Root, data }) as bool? ?? false;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SearchNode_ShouldNotFindNonExistingNode()
        {
            var tree = new BinaryTree<TestData>();
            var data = new TestData { Value = 5 };
            tree.AddNode(data);

            var nonExistingData = new TestData { Value = 10 };

            bool result = tree.GetType().GetMethod("SearchNode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(tree, new object[] { tree.Root, nonExistingData }) as bool? ?? false;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveNode_ShouldRemoveNodeCorrectly()
        {
            var tree = new BinaryTree<TestData>();
            tree.AddNode(new TestData { Value = 3 });
            tree.AddNode(new TestData { Value = 7 });
            tree.AddNode(new TestData { Value = 5 });

            tree.DeleteNode(new TestData { Value = 7 });

            var result = tree.GetType().GetMethod("SearchNode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(tree, new object[] { tree.Root, new TestData { Value = 7 } }) as bool? ?? false;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BuildBalancedTree_ShouldCreateBalancedTree()
        {
            var tree = new BinaryTree<TestData>();
            var elements = new List<TestData>
            {
                new TestData { Value = 1 },
                new TestData { Value = 2 },
                new TestData { Value = 3 },
                new TestData { Value = 4 },
                new TestData { Value = 5 },
                new TestData { Value = 6 },
                new TestData { Value = 7 }
            };

            var root = tree.GetType().GetMethod("BuildBalancedTree", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(tree, new object[] { elements, 0, elements.Count - 1 }) as ClassLibraryLab10.Node<TestData>;

            Assert.IsNotNull(root);
            Assert.AreEqual(4, root.Data.Value); // Root should be the middle element
        }

        [TestMethod]
        public void ConvertToArray_ShouldConvertTreeToArray()
        {
            var tree = new BinaryTree<TestData>();
            tree.AddNode(new TestData { Value = 5 });
            tree.AddNode(new TestData { Value = 3 });
            tree.AddNode(new TestData { Value = 7 });

            var array = new TestData[tree.Count];
            int index = 0;

            tree.GetType().GetMethod("ConvertToArray", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(tree, new object[] { tree.Root, array, index });

            CollectionAssert.AreEqual(new int[] { 3, 5, 7 }, Array.ConvertAll(array, item => item.Value));
        }
    }
}
