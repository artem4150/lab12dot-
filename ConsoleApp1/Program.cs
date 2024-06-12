using ClassLibrary1;
using lab12dot7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using лаба10;
namespace ClassLibraryLab10
{
    public class MyTree<T> where T : IInit, IComparable, ICloneable, new()
    {
        public Node<T> root = null;
        public Node<T> Root => root;
        int count = 0;


        /// <summary>
        /// Свойство, возвращающее количество элементов в дереве.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Конструктор по умолчанию класса MyTree.
        /// </summary>
        public MyTree()
        {
            root = null;
        }

        /// <summary>
        /// Конструктор класса MyTree с указанием длины дерева.
        /// </summary>
        /// <param name="length">Длина дерева.</param>
        public MyTree(int length)
        {
            count = length;
            root = MakeTree(length);
        }

        //ИСД
        /// <summary>
        /// Рекурсивный метод создания идеально сбалансированного дерева заданной длины.
        /// </summary>
        /// <param name="length">Длина дерева.</param>
        /// <param name="root">Корень дерева.</param>
        /// <returns>Корень нового дерева.</returns>
        private Node<T>? MakeTree(int length)
        {
            T data = new T();
            data.RandomInit(); // Предполагается, что у типа T есть метод RandomInit().
            Node<T> newItem = new Node<T>(data);

            if (length == 0) return null;

            int nl = length / 2;
            int nr = length - nl - 1;

            newItem.Left = MakeTree(nl);
            newItem.Right = MakeTree(nr);
            return newItem;
        }

        /// <summary>
        /// Метод отображения дерева с заданным узлом в определенном стиле.
        /// </summary>
        /// <param name="node">Узел для отображения.</param>
        /// <param name="style">Стиль отображения (0 - корень дерева, нету стрелки, 1 - левое поддерево, соотв. стрелка, 2 - правое поддерево, соотв. стрелка).</param>
        /// <param name="spaces">Отступы между уровнями узлов.</param>
        void Show(Node<T> node, int style, int spaces = 5)
        {
            if (node != null)
            {
                Show(node.Left, 1, spaces + 5);
                for (int i = 0; i < spaces; i++)
                {
                    Console.Write(" ");
                }
                if (style == 1)
                {
                    Console.Write("┌──");
                }
                else
                {
                    if (style == 2)
                    {
                        Console.Write("└──");
                    }
                }
                Console.WriteLine(node.Data);
                Show(node.Right, 2, spaces + 5);
            }
        }

        /// <summary>
        /// Метод для вывода дерева на экран.
        /// </summary>
        public void PrintTree()
        {
            Show(this.root, 0);
        }

        /// <summary>
        /// Метод добавления нового узла в дерево поиска.
        /// </summary>
        /// <param name="data">Данные нового узла.</param>
        void AddNode(T data)
        {
            Node<T>? node = root;
            Node<T>? current = null;
            bool isExist = false;
            while (node != null && !isExist)
            {
                current = node;
                if (node.CompareTo(data) == 0) // Уже есть
                {
                    isExist = true;
                }
                else // Поиск места
                {
                    if (node.CompareTo(data) < 0)
                    {
                        node = node.Left;
                    }
                    else
                    {
                        node = node.Right;
                    }
                }
            }

            // Место найдено
            if (isExist)
            {
                return; // Элемент уже есть, ничего не добавляем
            }

            Node<T> newNode = new Node<T>(data);
            if (current.CompareTo(data) < 0)
            {
                current.Left = newNode;
            }
            else
            {
                current.Right = newNode;
            }

            count++;
        }
        /// <summary>
        /// Рекурсивно преобразует дерево в массив.
        /// </summary>
        void TransformToArray(Node<T>? node, T[] arr, ref int current)
        {
            if (node != null)
            {
                TransformToArray(node.Left, arr, ref current);
                arr[current] = node.Data;
                current++;
                count++;
                TransformToArray(node.Right, arr, ref current);
            }

        }
        /// <summary>
        /// Преобразует дерево в массив и затем строит новое дерево поиска из этого массива.
        /// </summary>
        public void TransformToFindTree()
        {
            T[] arr = new T[count];
            int current = 0;
            TransformToArray(root, arr, ref current);

            root = new Node<T>(arr[0]);
            count = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                AddNode(arr[i]);

            }
        }
        /// <summary>
        /// Рекурсивно подсчитывает количество узлов в дереве с заданным ключом.
        /// </summary>
        int CountNodesWithKey(Node<T> node, T key)
        {
            if (node == null)
                return 0;
            int keyCounter = 0;
            if (node.CompareTo(key) == 0)
                keyCounter++;

            keyCounter += CountNodesWithKey(node.Left, key);
            keyCounter += CountNodesWithKey(node.Right, key);

            return keyCounter;
        }
        /// <summary>
        /// Возвращает количество узлов в дереве с заданным ключом.
        /// </summary>
        public int TreeCountNodes(T key)
        {
            return CountNodesWithKey(Root, key);

        }

        /// <summary>
        /// Метод для удаления узла с заданным ключом из дерева.
        /// </summary>
        /// <param name="key">Ключ узла для удаления.</param>
        public void DeleteNode(T key)
        {
            root = DeleteRec(root, key);
            count--;
        }

        /// <summary>
        /// Рекурсивный метод для удаления узла с заданным ключом из дерева.
        /// </summary>
        /// <param name="root">Корень поддерева, из которого нужно удалить узел.</param>
        /// <param name="key">Ключ узла для удаления.</param>
        /// <returns>Новый корень поддерева после удаления.</returns>
        Node<T> DeleteRec(Node<T> root, T key)
        {
            if (root == null)
                return root;

            if (root.CompareTo(key) < 0)
                root.Left = DeleteRec(root.Left, key);
            else if (root.CompareTo(key) > 0)
                root.Right = DeleteRec(root.Right, key);
            else // Обход дерева завершен, нужный узел найден
            {
                if (root.Left == null && root.Right == null) // Если нет узлов после элемента
                {
                    return null;
                }
                else if (root.Left == null && root.Right != null) // Если нет левого узла
                    return root.Right;
                else if (root.Right == null && root.Left != null) // Если нет правого узла
                    return root.Left;

                root.Data = MinValue(root.Right); // Заменяем узел для удаления минимальным значением на правой ветке

                root.Right = DeleteRec(root.Right, root.Data); // Удаляем узел, который мы перенесли
            }

            return root;
        }

        /// <summary>
        /// Находит минимальное значение в дереве, начиная с указанного корня.
        /// </summary>
        /// <param name="root">Корень дерева для поиска минимального значения.</param>
        /// <returns>Минимальное значение в дереве.</returns>
        T MinValue(Node<T> root)
        {
            T minValue = root.Data;
            while (root.Left != null) // Обход левой подветки у правой ветки (поиск минимального узла на правой ветке)
            {
                minValue = root.Left.Data; // Фиксация минимального значения на левой ветке
                root = root.Left;
            }
            return minValue;
        }

        /// <summary>
        /// Метод для поиска узла с заданным ключом в дереве.
        /// </summary>
        /// <param name="key">Ключ для поиска.</param>
        /// <returns>True, если узел найден, иначе false.</returns>
        public bool Search(T key)
        {
            return SearchRec(root, key);
        }

        /// <summary>
        /// Рекурсивный метод для поиска узла с заданным ключом в дереве.
        /// </summary>
        /// <param name="node">Текущий узел для проверки.</param>
        /// <param name="key">Ключ для поиска.</param>
        /// <returns>True, если узел найден, иначе false.</returns>
        private bool SearchRec(Node<T> node, T key)
        {
            if (node == null)
            {
                return false;
            }

            if (node.CompareTo(key) == 0)
            {
                return true;
            }

            if (node.CompareTo(key) < 0)
            {
                return SearchRec(node.Left, key);
            }
            else
            {
                return SearchRec(node.Right, key);
            }
        }

        /// <summary>
        /// Метод для удаления всего дерева.
        /// </summary>
        public void DeleteTree()
        {
            root = DeleteTreeRec(root);
            count = 0;
        }

        /// <summary>
        /// Рекурсивный метод для удаления всего дерева.
        /// </summary>
        /// <param name="node">Текущий узел для удаления.</param>
        /// <returns>Null после удаления всего дерева.</returns>
        private Node<T> DeleteTreeRec(Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            node.Left = DeleteTreeRec(node.Left);
            node.Right = DeleteTreeRec(node.Right);

            // Очистка памяти для текущего узла
            node = null;

            return node;
        }

        /// <summary>
        /// Метод для копирования дерева.
        /// </summary>
        /// <param name="original">Исходное дерево для копирования.</param>
        /// <returns>Копия дерева.</returns>
        public Node<T> CopyTreeRec(Node<T> original)
        {
            if (original == null)
            {
                return null;
            }

            Node<T> newNode = new Node<T>((T)original.Data.Clone());
            newNode.Left = CopyTreeRec(original.Left);
            newNode.Right = CopyTreeRec(original.Right);

            return newNode;
        }
        public void CopyTree(MyTree<T> original)
        {
            count = original.Count;
            root = CopyTreeRec(original.Root);

        }
    }
}
