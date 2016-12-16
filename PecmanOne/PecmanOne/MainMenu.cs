using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PecmanOne
{
    class MainMenu
    {
        public int Start()
        {
            
            StreamReader file = new StreamReader(@"Menu.txt");
            int i = 0;
            while (!file.EndOfStream)
            {
                Console.SetCursorPosition(30, 10+i);
                Console.WriteLine(file.ReadLine());
                i++;
            }
            Console.SetCursorPosition(30, 10+i);
            Console.WriteLine("Press any key to start, Escape to exit.");
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
