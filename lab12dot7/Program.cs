
using laba12;
using System.ComponentModel.Design;
using лаба10;
using lab12dot7;
using System.Xml.Linq;

using ClassLibraryLab10;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace laba12
{
    
        internal class Program
    {

        static void Main(string[] args)
        {
            DoublyLinkedList<MusicalInstrument> instrumentsList = new DoublyLinkedList<MusicalInstrument>();
            Random rand = new Random();
            //MusicalInstrument[] instruments = new MusicalInstrument[10]; // Замените размер массива и инициализацию элементов в соответствии с вашими объектами
            
            int arraySize = 20; // Размер массива

            MusicalInstrument[] instrumentsArray = new MusicalInstrument[arraySize];
            HashTable<MusicalInstrument, MusicalInstrument> instrumentsTable = new HashTable<MusicalInstrument, MusicalInstrument>();
            BinaryTree<MusicalInstrument> tree = new BinaryTree<MusicalInstrument>();
            // Заполнение массива рандомными объектами MusicalInstrument
            for (int i = 0; i < arraySize; i++)
            {
                switch (rand.Next(3)) // Выбор случайного инструмента
                {
                    case 0:
                        instrumentsArray[i] = new Guitar();
                        break;
                    case 1:
                        instrumentsArray[i] = new Piano();
                        break;
                    case 2:
                        instrumentsArray[i] = new ElectricGuitar();
                        break;
                }

                // Инициализация объекта
                instrumentsArray[i].RandomInit();
            }
            

            while (true)
            {
                int pointsMainMenu = 4;

                Console.WriteLine("\nМеню приложения:");

                Console.WriteLine("1 - Меню работы с двунаправленным списком");
                Console.WriteLine("2 - хеш-таблица");
                Console.WriteLine("3 - Меню работы с идеально сбалансированным деревом");
                Console.WriteLine("4 - Меню работы с коллекцией");
                Console.WriteLine("0 - Выйти из приложения");

                int choiceMainMenu = InputInt(0, pointsMainMenu);

                if (choiceMainMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из приложения");
                    break;
                }

                switch (choiceMainMenu)
                {
                    case 1:
                        BiCaseMenu(ref instrumentsList);
                        break;
                    case 2:
                        HashTable(ref instrumentsTable);
                        break;
                    case 3:
                        TreeCaseMenu(ref tree);
                        break;
                    case 4:
                        MyCollectionTest();
                        break;



                }
            }
        }
       
        private static int InputInt(int min, int max)
        {
            int number;
            bool inputCheck;
            do
            {
                Console.Write("Ввод: ");
                inputCheck = int.TryParse(Console.ReadLine(), out number) && number >= min && number <= max;
                if (!inputCheck) Console.WriteLine("Ошибка ввода! Введите целое число в пределах от {0} до {1} (включительно)", min, max);
            } while (!inputCheck);
            return number;
        }
        public static MusicalInstrument[] GenerateInstrumentsArray(int arraySize)
        {
            Random rand = new Random();
            MusicalInstrument[] instrumentsArray = new MusicalInstrument[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                switch (2) // Выбор случайного инструмента
                {
                    case 0:
                        instrumentsArray[i] = new Guitar();
                        break;
                    case 1:
                        instrumentsArray[i] = new Piano();
                        break;
                    case 2:
                        instrumentsArray[i] = new MusicalInstrument();
                        break;
                }

                // Инициализация объекта
                instrumentsArray[i].RandomInit();
                
            }
            return instrumentsArray;
        }

        private static void MyCollectionTest()
        {
            // Создаем коллекцию и заполняем ее объектами MusicalInstrument
            MyCollection<string, MusicalInstrument> collection = new MyCollection<string, MusicalInstrument>(10);

            // Добавляем случайные объекты MusicalInstrument в коллекцию
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                MusicalInstrument instrument;
                switch (rand.Next(3))
                {
                    case 0:
                        instrument = new Guitar();
                        break;
                    case 1:
                        instrument = new Piano();
                        break;
                    case 2:
                        instrument = new ElectricGuitar();
                        break;
                    default:
                        instrument = new MusicalInstrument();
                        break;
                }
                instrument.RandomInit();
                collection.Add(instrument.Name, instrument);
            }

            // Выводим содержимое коллекции
            collection.DisplayContents();

            // Демонстрация работы всех реализованных методов
            // Удаление элемента по ключу
            collection.Remove("YAMAHA C40");

            // Выводим содержимое коллекции после изменений
            Console.WriteLine("\nСодержимое коллекции после изменений:");
            collection.DisplayContents();

        }

        private static void TreeCaseMenu(ref BinaryTree<MusicalInstrument> tree)
        {

            BinaryTree<MusicalInstrument> searchTree = new BinaryTree<MusicalInstrument>();
            while (true)
            {
                int pointsCaseMenu = 9;

                Console.WriteLine("\nМеню работы с идеально сбалансированным деревом:");
                Console.WriteLine("1 - Формирование дерева");
                Console.WriteLine("2 - Распечатать полученное дерево");
                Console.WriteLine("3 - Найти максимальный элемент в дереве");
                Console.WriteLine("4 - Преобразовать идеально сбалансированное дерево в дерево поиска");
                Console.WriteLine("5 - Распечатать полученное дерево (полученное)");
                Console.WriteLine("6 - Удалить из дерева поиска элемент с заданным ключом");
                Console.WriteLine("7 - Удалить дерево из памяти");
                Console.WriteLine("8 - Очистка истории");
                Console.WriteLine("9 - Ручное добавление элементов в дерево");
                Console.WriteLine("0 - Выход из меню");

                int choiceCaseMenu = InputInt(0, pointsCaseMenu);

                if (choiceCaseMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из меню");
                    break;
                }

                switch (choiceCaseMenu)
                {
                    case 1:
                        {
                            Console.WriteLine("Введите количество элементов в дереве: ");
                            int num = InputInt(1, 100);
                            tree = new BinaryTree<MusicalInstrument>(num);
                            Console.WriteLine("Дерево было создано");
                        }
                        break;
                    case 2:
                        {
                            if (tree.GetElementCount() == 0)
                            {
                                Console.WriteLine("Дерево пустое.");
                                break;
                            }
                            Console.WriteLine("Исходное идеально сбалансированное бинарное дерево:");
                            tree.PrintTree();
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        {
                            if (searchTree.GetElementCount() == 0)
                            {
                                Console.WriteLine("Дерево поиска пустое.");
                                break;
                            }
                            MusicalInstrument maxInstrument = searchTree.FindMaxNode();
                            Console.WriteLine("Максимальный элемент в дереве: " + maxInstrument);
                        }
                        break;
                    case 4:
                        {
                            if (tree.GetElementCount() == 0)
                            {
                                Console.WriteLine("Дерево пустое.");
                                break;
                            }
                            searchTree = new BinaryTree<MusicalInstrument>();
                            searchTree.CopyTree(tree);
                            searchTree.TransformToFindTree();
                            Console.WriteLine("Дерево успешно преобразовано");
                        }
                        break;
                    case 5:
                        {
                            if (searchTree.GetElementCount() == 0)
                            {
                                Console.WriteLine("Дерево поиска пустое.");
                                break;
                            }
                            Console.WriteLine("Преобразованное дерево поиска:");
                            searchTree.PrintTree();
                            Console.WriteLine();
                        }
                        break;
                    case 6:
                        {
                            if (searchTree.GetElementCount() == 0)
                            {
                                Console.WriteLine("Дерево поиска пустое.");
                                break;
                            }
                            Console.WriteLine("Удаление элемента по ключу:");

                            Console.WriteLine("1 - Электрогитара");
                            Console.WriteLine("2 - Пианино");
                            Console.WriteLine("3 - Гитара");
                            Console.WriteLine("4 - Музыкальный инструмент");

                            int z = InputInt(1, 4);
                            MusicalInstrument toRemove = null;
                            switch (z)
                            {
                                case 1:
                                    {
                                        toRemove = new ElectricGuitar();
                                        toRemove.Init();
                                    }
                                    break;
                                case 2:
                                    {
                                        toRemove = new Piano();
                                        toRemove.Init();
                                    }
                                    break;
                                case 3:
                                    {
                                        toRemove = new Guitar();
                                        toRemove.Init();
                                    }
                                    break;
                                case 4:
                                    {
                                        toRemove = new MusicalInstrument();
                                        toRemove.Init();
                                    }
                                    break;
                            }
                            searchTree.DeleteNode(toRemove);
                            Console.WriteLine($"Элемент удален из таблицы: {toRemove}");
                        }
                        break;
                    case 7:
                        {
                            if (searchTree.GetElementCount() == 0 && tree.GetElementCount() == 0)
                            {
                                Console.WriteLine("Оба дерева пустые.");
                                break;
                            }
                            Console.WriteLine("1 - удалить дерево поиска");
                            Console.WriteLine("2 - удалить идеально сбалансированное дерево");
                            int i = InputInt(1, 2);
                            switch (i)
                            {
                                case 1:
                                    {
                                        if (searchTree.GetElementCount() == 0)
                                        {
                                            Console.WriteLine("Дерево пустое.");
                                            break;
                                        }
                                        searchTree.DeepDelete();
                                    }
                                    break;
                                case 2:
                                    {
                                        if (tree.GetElementCount() == 0)
                                        {
                                            Console.WriteLine("Дерево пустое.");
                                            break;
                                        }
                                        tree.DeepDelete();
                                    }
                                    break;
                            }
                        }
                        break;
                    case 8:
                        {
                            Console.Clear();
                            Console.WriteLine("История была очищена");
                        }
                        break;
                    case 9:
                        {
                            Console.WriteLine("Добавление нового элемента в дерево:");
                            Console.WriteLine("1 - Электрогитара");
                            Console.WriteLine("2 - Пианино");
                            Console.WriteLine("3 - Гитара");
                            Console.WriteLine("4 - Музыкальный инструмент");

                            int z = InputInt(1, 4);
                            MusicalInstrument newInstrument = null;
                            switch (z)
                            {
                                case 1:
                                    {
                                        newInstrument = new ElectricGuitar();
                                        newInstrument.Init();
                                    }
                                    break;
                                case 2:
                                    {
                                        newInstrument = new Piano();
                                        newInstrument.Init();
                                    }
                                    break;
                                case 3:
                                    {
                                        newInstrument = new Guitar();
                                        newInstrument.Init();
                                    }
                                    break;
                                case 4:
                                    {
                                        newInstrument = new MusicalInstrument();
                                        newInstrument.Init();
                                    }
                                    break;
                            }
                            if (tree.GetElementCount() == 0)
                            {
                                tree = new BinaryTree<MusicalInstrument>();
                            }
                            tree.AddNode(newInstrument);
                            tree.BalanceTree();
                            Console.WriteLine("Элемент добавлен в дерево");
                        }
                        break;
                }
            }
        }



        private static void HashTable(ref HashTable<MusicalInstrument, MusicalInstrument> instrumentsTable)
        {
            while (true)
            {
                int pointsCaseMenu = 8;

                Console.WriteLine("\nМеню работы с хеш-таблицей:");
                Console.WriteLine("1 - Формирование таблицы");
                Console.WriteLine("2 - Поиск элемента");
                Console.WriteLine("3 - Удаление элемента");
                Console.WriteLine("4 - Печать таблицы");
                Console.WriteLine("5 - Показать, что будет при добавлении элемента в хеш-таблицу, если в таблице уже находится максимальное число элементов");
                Console.WriteLine("6 - Очистка истории");
                Console.WriteLine("7 - Добавить вручную");
                Console.WriteLine("8 - Удалить таблицу");
                Console.WriteLine("0 - Выход из меню");

                int choiceCaseMenu = InputInt(0, pointsCaseMenu);

                if (choiceCaseMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из меню");
                    break;
                }

                switch (choiceCaseMenu)
                {
                    case 1:
                        {
                            instrumentsTable.Clear();
                            // Заполнение таблицы
                            for (int i = 0; i < 10; i++)
                            {
                                MusicalInstrument instrument = GenerateRandomInstrument();
                                instrumentsTable.Add(instrument, instrument);
                            }
                        }
                        break;

                    case 2:
                        {
                            if (instrumentsTable.GetCount() == 0)
                            {
                                Console.WriteLine("Хеш-таблица пустая.");
                                break;
                            }
                            // Поиск элемента в таблице
                            try
                            {
                                Console.WriteLine("Поиск элемента по ключу:");

                                Console.WriteLine("1 - Электрогитара");
                                Console.WriteLine("2 - Пианино");
                                Console.WriteLine("3 - Гитара");
                                Console.WriteLine("4 - Музыкальный инструмент");

                                int z = InputInt(0, 4);
                                switch (z)
                                {
                                    case 1:
                                        {
                                            ElectricGuitar e = new ElectricGuitar();
                                            e.Init();
                                            MusicalInstrument foundInstrument = instrumentsTable.Find(e);
                                            Console.WriteLine($"Найденный инструмент: {foundInstrument}");
                                        }
                                        break;
                                    case 2:
                                        {
                                            Piano p = new Piano();
                                            p.Init();
                                            MusicalInstrument foundInstrument = instrumentsTable.Find(p);
                                            Console.WriteLine($"Найденный инструмент: {foundInstrument}");
                                        }
                                        break;
                                    case 3:
                                        {
                                            Guitar g = new Guitar();
                                            g.Init();
                                            MusicalInstrument foundInstrument = instrumentsTable.Find(g);
                                            Console.WriteLine($"Найденный инструмент: {foundInstrument}");

                                        }
                                        break;
                                    case 4:
                                        {
                                            MusicalInstrument m = new MusicalInstrument();
                                            m.Init();
                                            MusicalInstrument foundInstrument = instrumentsTable.Find(m);
                                            Console.WriteLine($"Найденный инструмент: {foundInstrument}");
                                        }
                                        break;
                                }
                            }


                            catch (KeyNotFoundException)
                            {
                                Console.WriteLine("Инструмент не найден.");
                            }
                        }
                        break;

                    case 3:
                        {
                            if (instrumentsTable.GetCount() == 0)
                            {
                                Console.WriteLine("Хеш-таблица пустая.");
                                break;
                            }
                            // Удаление элемента из таблицы
                            Console.WriteLine("Удаление элемента по ключу:");
                            
                            
                            Console.WriteLine("1 - Электрогитара");
                            Console.WriteLine("2 - Пианино");
                            Console.WriteLine("3 - Гитара");
                            Console.WriteLine("4 - Музыкальный инструмент");

                            int z = InputInt(0, 4);
                            switch (z)
                            {
                                case 1:
                                    {
                                        ElectricGuitar e = new ElectricGuitar();
                                        e.Init();
                                        bool removed = instrumentsTable.Remove(e);
                                        Console.WriteLine($"Элемент удален из таблицы: {removed}");
                                    }
                                    break;
                                case 2:
                                    {
                                        Piano p = new Piano();
                                        p.Init();
                                        bool removed = instrumentsTable.Remove(p);
                                        Console.WriteLine($"Элемент удален из таблицы: {removed}");
                                    }
                                    break;
                                case 3:
                                    {
                                        Guitar g = new Guitar();
                                        g.Init();
                                        bool removed = instrumentsTable.Remove(g);
                                        Console.WriteLine($"Элемент удален из таблицы: {removed}");

                                    }
                                    break;
                                case 4:
                                    {
                                        MusicalInstrument m = new MusicalInstrument();
                                        m.Init();
                                        bool removed = instrumentsTable.Remove(m);
                                        Console.WriteLine($"Элемент удален из таблицы: {removed}");
                                    }
                                    break;
                            }
                           
                        }
                        break;

                    case 4:
                        {
                            if (instrumentsTable.GetCount() == 0)
                            {
                                Console.WriteLine("Хеш-таблица пустая.");
                                break;
                            }
                            // Печать таблицы
                            Console.WriteLine("Хеш-таблица:");
                            instrumentsTable.Print();
                        }
                        break;

                    case 5:
                        {
                            int count = instrumentsTable.GetCount();
                            Random rand = new Random();
                            // xДобавление элемента в полностью заполненную таблицу
                            Console.WriteLine("Попытка добавления элемента в полностью заполненную таблицу:");
                            try
                            {
                                
                                MusicalInstrument newInstrument = GenerateRandomInstrument();
                                Console.WriteLine("Добавляемый элемент");
                                newInstrument.Show();
                                instrumentsTable.Add(newInstrument, newInstrument);
                                if (count == instrumentsTable.GetCount())
                                {
                                    Console.WriteLine("Элемент не добавлен");
                                }
                                else { Console.WriteLine("Элемент добавлен"); }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        break;

                    case 6:
                        {
                            Console.Clear();
                            Console.WriteLine("История была очищена");
                        }
                        break;
                    case 7:
                        {

                            int count = instrumentsTable.GetCount();
                            Random rand = new Random();
                            // Добавление элемента в полностью заполненную таблицу
                            Console.WriteLine("введите элемент:");
                            try
                            {

                                MusicalInstrument newInstrument = new MusicalInstrument();
                                newInstrument.Init();
                                Console.WriteLine("Добавляемый элемент");
                                newInstrument.Show();
                                instrumentsTable.Add(newInstrument, newInstrument);
                                if (count == instrumentsTable.GetCount())
                                {
                                    Console.WriteLine("Элемент не добавлен");
                                }
                                else { Console.WriteLine("Элемент добавлен"); }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка: {ex.Message}");
                            }
                        }
                        break;
                    case 8:
                        {
                            instrumentsTable.Clear();
                            Console.WriteLine("Таблица была очищена");
                        }
                        break;
                }
            }
        }



        static MusicalInstrument GenerateRandomInstrument() // Принимаем индекс в GenerateRandomInstrument
        {
            Random rand = new Random();
            switch (rand.Next(3))
            {
                case 0:
                    Guitar g = new Guitar();
                    g.RandomInit();
                    
                    return g;
                case 1:
                    Piano p = new Piano();
                    p.RandomInit();
                    
                    return p;
                case 2:
                    ElectricGuitar e = new ElectricGuitar();
                    e.RandomInit();
                    
                    return e;
                default:
                    MusicalInstrument m = new MusicalInstrument();
                    m.RandomInit();
                   
                    return m;
            }
        }
        private static void BiCaseMenu(ref DoublyLinkedList<MusicalInstrument> instrumentsList)
        {

            Random rand = new Random();
            while (true)
            {
                int pointsCaseMenu = 8;

                Console.WriteLine("\nМеню работы с двунаправленным списком");
                Console.WriteLine("1 - Формирование двунаправленного списка");
                Console.WriteLine("2 - Добавление элемента в список");
                Console.WriteLine("3 - Удаление элемента из списка");
                Console.WriteLine("4 - Печать списка");
                Console.WriteLine("5 - Добавление в список элементов с номерами 1, 3, 5 и т.д.");
                Console.WriteLine("6 - Удаление списка из памяти");
                Console.WriteLine("7 - Очистка истории");
                Console.WriteLine("8 - Клонирование списка");
                Console.WriteLine("0 - Выход из меню");

                int choiceCaseMenu = InputInt(0, pointsCaseMenu);

                if (choiceCaseMenu == 0)
                {
                    Console.WriteLine("\n0 - Выход из меню");
                    break;
                }

                switch (choiceCaseMenu)
                {
                    case 1:
                        {
                            instrumentsList.DeepClear();
                            Console.WriteLine("\n1 - Формирование двунаправленного списка");
                            Console.WriteLine("Введите количество объектов для формирования списка:");
                            int number = InputInt(1, 100);

                            Console.WriteLine("Список:");
                            for (int i = 0; i < number; i++)
                            {
                                int r = rand.Next(1, 4);
                                switch (r)
                                {
                                    case 1:
                                        {
                                            ElectricGuitar e = new ElectricGuitar();
                                            e.RandomInit();
                                            instrumentsList.AddLast(e);
                                        }
                                        break;
                                    case 2:
                                        {
                                            Piano p = new Piano();
                                            p.RandomInit();
                                            instrumentsList.AddLast(p);
                                        }
                                        break;
                                    case 3:
                                        {
                                            Guitar g = new Guitar();
                                            g.RandomInit();
                                            instrumentsList.AddLast(g);
                                        }
                                        break;
                                    case 4:
                                        {
                                            MusicalInstrument m = new MusicalInstrument();
                                            m.RandomInit();
                                            instrumentsList.AddLast(m);
                                        }
                                        break;
                                }
                            }
                            Console.WriteLine("Формирование двунаправленного списка завершено");
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("\n- Добавление элемента в список");
                            Console.WriteLine("1 - Электрогитара");
                            Console.WriteLine("2 - Пианино");
                            Console.WriteLine("3 - Гитара");
                            Console.WriteLine("4 - Музыкальный инструмент");

                            int z = InputInt(0, 4);
                            switch (z)
                            {
                                case 1:
                                    {
                                        ElectricGuitar e = new ElectricGuitar();
                                        e.Init();
                                        instrumentsList.AddLast(e);
                                    }
                                    break;
                                case 2:
                                    {
                                        Piano p = new Piano();
                                        p.Init();
                                        instrumentsList.AddLast(p);
                                    }
                                    break;
                                case 3:
                                    {
                                        Guitar g = new Guitar();
                                        g.Init();
                                        instrumentsList.AddLast(g);
                                    }
                                    break;
                                case 4:
                                    {
                                        MusicalInstrument m = new MusicalInstrument();
                                        m.Init();
                                        instrumentsList.AddLast(m);
                                    }
                                    break;
                            }


                            Console.WriteLine("Добавление элемента в список завершено");
                        }
                        break;
                    case 3:
                        {



                            Console.WriteLine("\n3 - Удаление элементов из списка");
                            if (instrumentsList.GetCount() == 0)
                            {
                                Console.WriteLine("Список пустой.");
                                break;
                            }
                            // Запрашиваем у пользователя имя элемента для удаления
                            Console.WriteLine("Введите имя элемента для удаления:");
                            string nameToRemove = Console.ReadLine();

                            // Проверяем, есть ли элемент с заданным именем в списке и удаляем его со всеми последующими элементами
                            bool removed = instrumentsList.RemoveFrom(nameToRemove);

                            // Если удаление прошло успешно, сообщаем об этом пользователю
                            if (removed)
                            {
                                Console.WriteLine("Элементы, начиная с элемента с именем '{0}', были успешно удалены из списка.", nameToRemove);
                            }
                            else
                            {
                                Console.WriteLine("Не удалось удалить элементы, начиная с элемента с именем '{0}'.", nameToRemove);
                            }
                        }
                        break;
                    case 4:
                        {
                            Console.WriteLine("\n4 - Печать списка");
                            if (instrumentsList.GetCount() == 0)
                            {
                                Console.WriteLine("Список пустой.");
                                break;
                            }
                            instrumentsList.Print();
                            Console.WriteLine("Печать списка завершена");
                        }
                        break;
                    case 5:
                        {
                            Console.WriteLine("\n5 - Добавление в список элементов с номерами 1, 3, 5 и т.д.");
                            instrumentsList = instrumentsList.AddRandomElementsToOddPositions(instrumentsList);
                            Console.WriteLine("Добавление в список элементов с номерами 1, 3, 5 и т.д. завершено");
                        }
                        break;
                    case 6:
                        {
                            Console.WriteLine("\n6 - Удаление списка из памяти");
                            instrumentsList.DeepClear();
                            Console.WriteLine("Удаление списка из памяти завершено");
                        }
                        break;
                    case 7:
                        {
                            Console.Clear();
                            Console.WriteLine("История была очищена");
                        }
                        break;
                    case 8:
                        {
                            Console.WriteLine("\nВывод клонированного списка:");
                            DoublyLinkedList<MusicalInstrument> clonedList = instrumentsList.DeepClone();
                            clonedList.Print();
                            Console.WriteLine("\nВывод оригинального списка списка:");
                            instrumentsList.Print();
                            break;
                        }
                }
            }

        }
    }
}
