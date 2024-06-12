using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12dot7
{

    // Обобщенная коллекция с хеш-таблицей (метод цепочек)
    public class MyCollection<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const int DefaultCapacity = 10;
        private LinkedList<KeyValuePair<TKey, TValue>>[] _items;

        // Конструктор для создания пустой коллекции
        public MyCollection()
        {
            _items = new LinkedList<KeyValuePair<TKey, TValue>>[DefaultCapacity];
        }

        // Конструктор для создания коллекции из length элементов, сформированных с помощью ДСЧ
        public MyCollection(int length)
        {
            _items = new LinkedList<KeyValuePair<TKey, TValue>>[length];
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                Add(GetRandomKey(), default(TValue));
            }
        }
        // Метод для генерации случайного ключа
        private TKey GetRandomKey()
        {
            Random rand = new Random();
            return (TKey)Convert.ChangeType(rand.Next().ToString(), typeof(TKey));
        }
        // Конструктор для создания коллекции, которая инициализируется элементами и емкостью коллекции
        public MyCollection(MyCollection<TKey, TValue> c)
        {
            _items = new LinkedList<KeyValuePair<TKey, TValue>>[c._items.Length];
            foreach (var item in c)
            {
                Add(item.Key, item.Value);
            }
        }

        // Метод добавления элемента в коллекцию
        public void Add(TKey key, TValue value)
        {
            int index = GetIndex(key);
            if (_items[index] == null)
            {
                _items[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
            _items[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
        }

        // Метод удаления элемента из коллекции по ключу
        public void Remove(TKey key)
        {
            int index = GetIndex(key);
            if (_items[index] == null) return;

            var currentNode = _items[index].First;
            while (currentNode != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(currentNode.Value.Key, key))
                {
                    _items[index].Remove(currentNode);
                    return;
                }
                currentNode = currentNode.Next;
            }
        }

        // Метод получения индекса для элемента по ключу
        private int GetIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode() % _items.Length);
        }

        // Реализация интерфейса IEnumerable<KeyValuePair<TKey, TValue>>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var list in _items)
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        yield return item;
                    }
                }
            }
        }

        // Реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Демонстрационный метод, выводящий содержимое коллекции
        public void DisplayContents()
        {
            Console.WriteLine("Содержимое коллекции:");
            foreach (var item in this)
            {
                Console.WriteLine($"Ключ: {item.Key}, Значение: {item.Value}");
            }
        }
    }
}
