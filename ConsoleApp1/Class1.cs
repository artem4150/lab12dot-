using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using лаба10;
using static System.Net.Mime.MediaTypeNames;

namespace ClassLibraryLab10
{
    public class Node<T> where T : IComparable, ICloneable
    {
        /// <summary>
        /// Данные узла.
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// Левый потомок узла.
        /// </summary>
        public Node<T>? Left { get; set; }
        /// <summary>
        /// Правый потомок узла.
        /// </summary>
        public Node<T>? Right { get; set; }

        /// <summary>
        /// Конструктор по умолчанию класса Node.
        /// </summary>
        public Node()
        {
            this.Data = default(T);
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Конструктор класса Node с параметром данных.
        /// </summary>
        /// <param name="data">Данные для установки в узел.</param>
        public Node(T data)
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Переопределение метода ToString для класса Node.
        /// </summary>
        /// <returns>Строковое представление данных узла.</returns>
        public override string? ToString()
        {
            return Data == null ? "" : Data.ToString();
        }

        /// <summary>
        /// Метод сравнения текущего узла с другим узлом по данным.
        /// </summary>
        /// <param name="other">Другой узел для сравнения.</param>
        /// <returns>Целочисленное значение, указывающее на относительное положение данных текущего узла и другого узла.</returns>
        public int CompareTo(T other)
        {
            if (other == null) return 1;

            // Сравниваем узлы по значению
            return this.Data.CompareTo(other);
        }
        /// <summary>
        /// Определяет, равен ли указанный объект текущему объекту.
        /// </summary>
        /// <param name="obj">Объект для сравнения с текущим объектом.</param>
        /// <returns>True, если объекты равны, в противном случае - false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Node<T> s)
                return s.Data.Equals(this.Data) && s.Left == this.Left && this.Right == s.Right;
            return false;
        }
    }
}
