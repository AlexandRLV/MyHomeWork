using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(Func1);
            Thread t1 = new Thread(Func2);
            t.Start();
            t1.Start();
            int i = 0;
            while (i<500)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("x");
                i++;
            }

            Task t2 = new Task(Func1);
            Task t3 = new Task(Func);
        }

        static void Func()
        {

        }

        static void Func1()
        {
            int i = 0;
            while (i<500)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write("y");
                i++;
            }
        }

        static void Func2()
        {
            int i = 0;
            while (i < 500)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write("z");
                i++;
            }
        }
    }
}
