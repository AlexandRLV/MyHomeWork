using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PecmanOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You are playing in the best game!");
            Console.WriteLine("Enter your level.");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Controller c = new Controller(x);
            c.Start();
            Console.Clear();
            Console.WriteLine("Thank you for playing!");
            Console.ReadKey();
        }
    }
}