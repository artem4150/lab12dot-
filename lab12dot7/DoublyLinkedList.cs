using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using лаба10;
using static System.Net.Mime.MediaTypeNames;

namespace lab12dot7
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
           
        }
        


    }

    public class DoublyLinkedList<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }
        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
            GetNameHandler = DefaultGetNameHandler;
        }
        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
                Tail = newNode;
            }
        }
        // Определение делегата для получения имени из объекта типа T
        public delegate string GetNameDelegate(T item);

        // Поле для хранения делегата
        public GetNameDelegate GetNameHandler { get; set; }

        
        

        // Метод для получения имени по узлу
        public string GetNameFromNode(Node<T> node)
        {
            if (node == null)
            {
                Console.WriteLine("Ошибка: Узел пуст.");
                return null;
            }

            // Вызываем делегат для получения имени из объекта типа T
            return GetNameHandler(node.Data);
        }

        // Метод по умолчанию для работы с объектами MusicalInstrument
        private string DefaultGetNameHandler(T item)
        {
            if (item is MusicalInstrument)
            {
                return ((MusicalInstrument)(object)item).Name;
            }
            else
            {
                Console.WriteLine("Ошибка: Объект не является музыкальным инструментом.");
                return null;
            }
        }
        public bool RemoveFrom(string name)
        {
            
            try
            {
                // Проверяем, есть ли элемент с заданным именем в списке
                Node<T> elementToRemove = FindByName(name);
                if (elementToRemove == null)
                {
                    Console.WriteLine("Элемент с именем '{0}' не найден в списке.", name);
                    return false;
                }

                // Удаление всех элементов, начиная с элемента с заданным именем, и до конца списка
                while (elementToRemove != null)
                {
                    Node<T> next = elementToRemove.Next;
                    Remove(elementToRemove);
                    elementToRemove = next;
                }

                Console.WriteLine("Элементы, начиная с элемента с именем '{0}', были удалены из списка.", name);
                return true;
            }
            catch { Console.WriteLine("Ошибка"); return false; }
        }

        public void Print()
        {
            
                Node<T> current = Head;
                

                    while (current != null)
                    {
                        Console.WriteLine(current.Data.ToString());
                        current = current.Next;
                    }
                
            
        }

        public int Count
        {
            get
            {
                int count = 0;
                Node<T> current = Head;
                while (current != null)
                {
                    count++;
                    current = current.Next;
                }
                return count;
            }
        }
        //
        public Node<T> FindByName(string name)
        {
            
            Node<T> current = Head;

            while (current != null)
            {
                if (GetNameFromNode(current) == name)
                    return current;

                current = current.Next;
            }
            return null;
        }

        public void Remove(Node<T> node)
        {
            if (node == null)
                return;

            if (node.Previous != null)
                node.Previous.Next = node.Next;
            else
                Head = node.Next;

            if (node.Next != null)
                node.Next.Previous = node.Previous;
            else
                Tail = node.Previous;
        }
        
        public int GetCount()
        {
            int i = 0;
            Node<T> p = Head;
            while (p != null)
            {
                i++;
                p = p.Next; 
            }
            return i;
        }
        public void Clear()
        {
            Head = null;
            Tail = null;
        }
        

        public  DoublyLinkedList<T> AddElementAtIndex(DoublyLinkedList<T> list, T data, int index)
        {
            // Создаем новый узел с данными
            Node<T> newNode = new Node<T>(data);

            // Если список пустой или индекс равен 1, добавляем в начало списка
            if (list == null || index == 1)
            {
                newNode.Next = list.Head;
                if (list.Head != null)
                    list.Head.Previous = newNode;
                list.Head = newNode;

                // Если список был пуст, новый узел становится и концом списка
                if (list.Tail == null)
                    list.Tail = newNode;

                return list;
            }

            // Иначе ищем место для вставки
            Node<T> current = list.Head;
            int currentIndex = 1;
            while (current != null && currentIndex < index - 1)
            {
                current = current.Next;
                currentIndex++;
            }

            // Если достигнут конец списка, добавляем в конец
            if (current == null || current.Next == null)
            {
                list.Tail.Next = newNode;
                newNode.Previous = list.Tail;
                list.Tail = newNode;
            }
            else
            {
                // Вставляем новый узел между текущим и следующим узлом
                newNode.Next = current.Next;
                newNode.Previous = current;
                current.Next.Previous = newNode; 
                current.Next = newNode;
            }

            return list;
        }

        


        public static Node<T> MakeRandomNode()
        {
            
            MusicalInstrument randomInstrument = new MusicalInstrument();
            randomInstrument.RandomInit();

            // Создаем новый узел с рандомными данными
            Node<T> newNode = new Node<T>((T)Convert.ChangeType(randomInstrument, typeof(T)));

            return newNode;
        }

        public DoublyLinkedList<T> AddRandomElementsToOddPositions(DoublyLinkedList<T> list)
        {
            if ( Count == 0)
            {
                Console.WriteLine("успешно");
                list.Head = MakeRandomNode();
                return list;
            }

            Random random = new Random();
            Node<T> current = list.Head;
            int index = -1;

            while (current != null)
            {
                // Если индекс нечетный, добавляем новый рандомный элемент перед текущим узлом
                if (index % 2 != 0 )
                {
                    Node<T> newNode = MakeRandomNode(); // Создаем новый рандомный узел

                    // Если текущий узел не является головой списка, обновляем ссылки предыдущего узла
                    if (current.Previous != null)
                    {
                        newNode.Previous = current.Previous;
                        current.Previous.Next = newNode;
                    }
                    else
                    {
                        // Если текущий узел является головой списка, обновляем голову списка
                        list.Head = newNode;
                    }

                    // Устанавливаем связи для нового узла
                    newNode.Next = current;
                    current.Previous = newNode;

                    // Перемещаем указатель текущего узла на следующий узел
                    current = newNode.Next;
                }
                else
                {
                    // Если индекс четный, просто переходим к следующему узлу
                    current = current.Next;
                }
                
                index++;
            }
            // Добавляем новый рандомный элемент на следующую нечетную позицию после последнего узла
            if (index % 2 != 0)
            {
                Node<T> newNode = MakeRandomNode(); 

                if (list.Tail != null)
                {
                    list.Tail.Next = newNode;
                    newNode.Previous = list.Tail;
                    list.Tail = newNode;
                }
                else
                {
                    // Если список пустой, новый узел становится головой и хвостом
                    list.Head = newNode;
                    list.Tail = newNode;
                }
            }

                return list;
        }
        public DoublyLinkedList<T> AddOddObjects(DoublyLinkedList<T> list)
        {
            if (list == null)
            {
                Console.WriteLine("Ошибка! Передан пустой список.");
                return null;
            }
            if (list.GetCount() >= 1000)
            {
                Console.WriteLine("Ошибка! Список имеет не меньше 100 элементов");
                Console.WriteLine("Добавление в список элементов с номерами 1, 3, 5 и т.д. не завершено");
                return list;
            }

            int count = list.GetCount();
            for (int i = count + 1; i <= count * 2; i += 2)
            {
                list = list.ShiftRightFromNode(list.FindByIndex(i));
                list = list.AddElementAtIndex(list, GenerateRandomData(), i);
            }

            Console.WriteLine("Добавление в список элементов с номерами 1, 3, 5 и т.д. завершено");
            return list;
        }
        public   Node<T> FindByIndex(  int index)
        {

            // Проверяем, что индекс не отрицателен
            if (index < 0)
            {
                Console.WriteLine("Индекс должен быть неотрицательным числом.");
                return null;
            }

            // Проверяем, что список не пустой
            if (Head == null)
            {
                Console.WriteLine("Список пуст.");
                return null;
            }

            // Начинаем с головы списка
            Node<T> currentNode = Head;
            int currentIndex = 0;

            // Переходим по списку, пока не достигнем нужного индекса или не кончится список
            while (currentNode != null && currentIndex < index)
            {
                currentNode = currentNode.Next;
                currentIndex++;
            }



            // Если достигнут нужный индекс, возвращаем текущий узел
            if (currentIndex == index)
            {
                return currentNode;
            }
            else
            {
                // Если индекс находится за пределами списка, выводим сообщение об ошибке
                Console.WriteLine("Индекс находится за пределами списка.");
                return null;
            }
        }
        public DoublyLinkedList<T> DeepClone()
        {
            // Создаем новый пустой список для клонированных элементов
            DoublyLinkedList<T> clonedList = new DoublyLinkedList<T>();

            // Проходим по всем узлам исходного списка
            Node<T> current = Head;
            while (current != null)
            {
                // Клонируем данные из текущего узла
                T clonedData;

                // Проверяем, реализует ли объект в текущем узле интерфейс ICloneable
                if (current.Data is ICloneable)
                {
                    // Если данные реализуют интерфейс ICloneable, используем метод Clone для создания их копии
                    clonedData = (T)((ICloneable)current.Data).Clone();
                }
                else
                {
                    // Если данные не реализуют ICloneable, предполагаем, что они неизменяемы и просто копируем их
                    clonedData = current.Data;
                }

                // Создаем новый узел с клонированными данными
                Node<T> clonedNode = new Node<T>(clonedData);

                // Добавляем новый узел в конец клонированного списка
                if (clonedList.Head == null)
                {
                    clonedList.Head = clonedNode;
                    clonedList.Tail = clonedNode;
                }
                else
                {
                    clonedList.Tail.Next = clonedNode;
                    clonedNode.Previous = clonedList.Tail;
                    clonedList.Tail = clonedNode;
                }

                // Переходим к следующему узлу в исходном списке
                current = current.Next;
            }

            // Возвращаем клонированный список
            return clonedList;
        }
        public DoublyLinkedList<T> ShiftRightFromNode(Node<T> startNode)
        {
            if (startNode == null)
            {
                Console.WriteLine("Ошибка! Начальный узел равен null.");
                return null;
            }

            // Создаем новый список для хранения сдвинутых элементов
            DoublyLinkedList<T> shiftedList = new DoublyLinkedList<T>();

            // Проверяем, что начальный узел не является последним элементом списка
            if (startNode != Tail && startNode.Next != null) // Добавлено условие startNode.Next != null
            {
                // Начинаем сдвиг с узла, следующего за начальным узлом
                Node<T> currentNode = startNode.Next;

                // Перемещаем все узлы, начиная с указанного узла, в новый список
                while (currentNode != null)
                {
                    shiftedList.AddLast(currentNode.Data);
                    currentNode = currentNode.Next;
                }

                // После перемещения всех элементов, добавляем начальный узел в конец списка
                shiftedList.AddLast(startNode.Data);
            }

            // Возвращаем сдвинутый список
            return shiftedList;
        }
        public void DeepClear()
        {
            // Начинаем с головы списка
            Node<T> current = Head;

            // Переходим по списку, пока не достигнем конца
            while (current != null)
            {
                // Сохраняем ссылку на следующий узел перед удалением текущего
                Node<T> next = current.Next;

                // Освобождаем память, занимаемую текущим узлом
                current.Data = default(T); // Опционально, для объектов можно вызывать Dispose() или реализовывать IDisposable
                current.Previous = null;
                current.Next = null;

                // Переходим к следующему узлу
                current = next;
            }

            // Удаляем ссылки на голову и хвост списка
            Head = null;
            Tail = null;
        }
        public Node<T> MakeRandomNoden()
{
    
    MusicalInstrument randomInstrument = new MusicalInstrument();
    randomInstrument.RandomInit();

    // Создаем новый узел с рандомными данными
    Node<T> newNode = new Node<T>((T)Convert.ChangeType(randomInstrument, typeof(T)));

    return newNode;
}
        public static T GenerateRandomData()
        {
            MusicalInstrument randomInstrument = new MusicalInstrument();
            randomInstrument.RandomInit();

            // Преобразуем объект в тип T и возвращаем
            return (T)Convert.ChangeType(randomInstrument, typeof(T));
        }

    }
}
