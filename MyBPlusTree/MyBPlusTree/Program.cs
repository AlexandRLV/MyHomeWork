using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBPlusTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BPlusTree<char> tree = new BPlusTree<char>(2);
            tree.Add(new BPlusTree<char>.Keys<char>(1, 'a'));
            tree.Add(new BPlusTree<char>.Keys<char>(2, 'b'));
            tree.Add(new BPlusTree<char>.Keys<char>(3, 'c'));
            tree.PrintLevels();
            Console.WriteLine();
            Console.WriteLine("Add 4");
            tree.Add(new BPlusTree<char>.Keys<char>(4, 'd'));
            tree.PrintLevels();
            Console.WriteLine();
            Console.WriteLine("Add 5");
            tree.Add(new BPlusTree<char>.Keys<char>(5, 'e'));
            tree.PrintLevels();
            Console.WriteLine();
            Console.WriteLine("Add 6");
            tree.Add(new BPlusTree<char>.Keys<char>(6, 'f'));
            tree.PrintLevels();
            tree.Delete(5);
            Console.WriteLine();
            Console.WriteLine("Delete 5");
            tree.PrintLevels();
            Console.WriteLine();
            Console.WriteLine("Find 6");
            Console.WriteLine(tree.Find(6));
        }
    }
}
