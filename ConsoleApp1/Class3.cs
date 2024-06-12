using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;
using лаба10;
namespace Lab12Programm
{
    public class TreeHelper
    {
        public static int GetInt(int min, int max, string msg)
        {
            bool isCorrectAnsw;
            int answer;
            Console.WriteLine(msg);
            do
            {
                isCorrectAnsw = int.TryParse(Console.ReadLine(), out answer);
                if (!isCorrectAnsw || answer > max || answer < min)
                {
                    Console.WriteLine("Неправильно введено число, попробуйте еще раз.");
                }
            } while (!isCorrectAnsw || (answer > max) || (answer < min));
            return answer;
        }

        public static void PrintMenu(string[] menu)
        {
            foreach (string item in menu)
            {
                Console.WriteLine(item);
            }
        }

        public static MyTree<MusicalInstrument> TreeFormer(int number)
        {
            MyTree<MusicalInstrument> tree = new MyTree<MusicalInstrument>(number);
            return tree;
        }

        public static void TreePrinter(MyTree<MusicalInstrument> tree)
        {
            Console.WriteLine("П");
            tree.PrintTree();
            Console.WriteLine("Л");
        }


        public static MyTree<MusicalInstrument> TreeTransform(MyTree<MusicalInstrument> tree)
        {
            MyTree<MusicalInstrument> findTree = new MyTree<MusicalInstrument>();
            findTree.CopyTree(tree);
            findTree.TransformToFindTree();
            return findTree;
        }

        public static int TreeCountKey(MusicalInstrument clocks, MyTree<MusicalInstrument> tree)
        {
            return tree.TreeCountNodes(clocks);
        }

        public static void TreeDeleter(MusicalInstrument clocks, MyTree<MusicalInstrument> tree)
        {
            tree.DeleteNode(clocks);
        }

        public static void TreeClear(MyTree<MusicalInstrument> tree)
        {
            tree.DeleteTree();
        }
    }
}
