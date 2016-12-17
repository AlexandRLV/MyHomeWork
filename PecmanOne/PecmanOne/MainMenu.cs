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
            // Считываем рисунок в главном меню и выводим его построчно.
            StreamReader file = new StreamReader(@"Menu.txt");
            int i = 0;
            while (!file.EndOfStream)
            {
                Console.SetCursorPosition(30, 10+i);
                Console.WriteLine(file.ReadLine());
                i++;
            }
            // Пишем информацию о возможности создания пользовательских карт. Начинаем игру.
            Console.SetCursorPosition(30, 10+i);
            Console.WriteLine("You can create your own levels!");
            Console.SetCursorPosition(30, 10 + i + 1);
            Console.WriteLine("Level must be 28x31 and there must be an empty place at");
            Console.SetCursorPosition(30, 10 + i + 2);
            Console.WriteLine("13,23  13,11  13,13  14,15  12,15 for hero and enemies.");
            Console.SetCursorPosition(30, 10 + i + 3);
            Console.WriteLine("Do not delete MainLevel file!!!");
            Console.SetCursorPosition(30, 10 + i + 4);
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
