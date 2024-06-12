using Microsoft.VisualBasic.FileIO;
using System;
using System.Drawing;
using System.Net.Cache;
using ClassLibraryLab10;
using лаба10;




namespace Lab12Programm
{
    class lab_12_3_Programm
    {
        static void Main(string[] args)
        {
            string[] menu = { "1. Создать новое ИСД дерево (случайное заполнение элементами)", "2.Печать дерева", "3. Найти количество элементов с заданным ключом ", "4. Преобразовать в дерево поиска", "5.Удаление элемента из дерева поиска", "6. Удалить дерево из памяти", "7. Завершить работу программы" };
            MyTree<MusicalInstrument> tree = new MyTree<MusicalInstrument>();
            MyTree<MusicalInstrument> findTree = new MyTree<MusicalInstrument>();
            bool flag = true;
            do
            {
                TreeHelper.PrintMenu(menu);
                switch (TreeHelper.GetInt(1, 7, ""))
                {
                    case 1:
                        {
                            try
                            {
                                int num = TreeHelper.GetInt(0, 20000, "Введите количество элементов в дереве: ");
                                tree = TreeHelper.TreeFormer(num);
                                Console.WriteLine("Дерево было создано");
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }

                        }
                    case 2:
                        {
                            string[] extra = { "1. Печать ИСД дерева", "2. Печать дерева поиска" };
                            TreeHelper.PrintMenu(extra);
                            switch (TreeHelper.GetInt(1, 2, ""))
                            {
                                case 1:
                                    {
                                        if (tree.Count != 0)
                                        {
                                            Console.WriteLine("Дерево: ");
                                            TreeHelper.TreePrinter(tree);
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Дерево пустое");
                                            break;
                                        }
                                    }
                                case 2:
                                    {
                                        if (findTree.Count != 0)
                                        {
                                            Console.WriteLine("Дерево: ");
                                            TreeHelper.TreePrinter(findTree);
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Дерево пустое");
                                            break;
                                        }
                                    }
                            }
                            break;


                        }
                    case 3:
                        {
                            MusicalInstrument key = new MusicalInstrument();
                            key.Init();

                            int result = TreeHelper.TreeCountKey(key, tree);
                            Console.WriteLine($"Количество элементов дерева с объектом {key}: {result}");
                            break;
                        }
                    case 4:
                        {
                            if (tree.Count != 0)
                            {
                                Console.WriteLine("Дерево до преобразования: ");
                                TreeHelper.TreePrinter(tree);
                                Console.WriteLine("Дерево после преобразования: ");
                                findTree = TreeHelper.TreeTransform(tree);
                                TreeHelper.TreePrinter(findTree);
                                Console.WriteLine("Дерево было преобразовано в дерево поиска");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Дерево пустое, преобразовывать нечего");
                                break;
                            }

                        }
                    case 5:
                        {
                            if (findTree.Count != 0)
                            {
                                MusicalInstrument clock = new MusicalInstrument();
                                clock.Init();

                                if (findTree.Search(clock))
                                {
                                    TreeHelper.TreeDeleter(clock, findTree);
                                    if (!findTree.Search(clock))
                                    {
                                        Console.WriteLine("Элемент был удален, дерево сейчас выглядит так:");
                                        if (findTree.Count != 0)
                                        {
                                            TreeHelper.TreePrinter(findTree);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Дерево теперь пустое");
                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("Элемент не был удален, ошибка((( дерево:");
                                        TreeHelper.TreePrinter(findTree);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Элемент не найден, вот исходное дерево:");
                                    TreeHelper.TreePrinter(findTree);
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Дерево поиска пустое");
                                break;
                            }
                        }
                    case 6:
                        {
                            TreeHelper.TreeClear(tree);
                            TreeHelper.TreeClear(findTree);
                            Console.WriteLine("Дерево было удалено из памяти");
                            break;
                        }
                    case 7:
                        {
                            flag = false;
                            break;
                        }
                }
            } while (flag);

        }
    }
}