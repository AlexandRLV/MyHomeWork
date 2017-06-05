using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHash
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable table = new HashTable(100);
            int[] x = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            HashTree tree = new HashTree(x);
            tree.PrintLevels();
            table.Add(3);
            table.Add(4);
            table.Add(100);
            Console.WriteLine(table);
            table.Delete(4);
            Console.WriteLine(table);
        }
    }
}
