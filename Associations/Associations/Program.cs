using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    class Program
    {
        static void Main(string[] args)
        {
            Statements s = new Statements();
            while (true)
            {
                Console.WriteLine("This is our study model. Press escape to see our degrees, space to see courses, enter to add new application.");
                ConsoleKeyInfo key = Console.ReadKey(true);
                while ((key.Key != ConsoleKey.Escape) && (key.Key != ConsoleKey.Spacebar) && (key.Key != ConsoleKey.Enter))
                {
                    key = Console.ReadKey(true);
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    s.WriteDegrees();
                    Console.WriteLine("Press escape to return in menu, enter to add new degree.");
                    key = Console.ReadKey(true);
                    while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter)
                    {
                        key = Console.ReadKey(true);
                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        s.AddNewDegree();
                    }
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    s.WriteCources();
                    Console.WriteLine("Press escape to return in menu, enter to add new course.");
                    key = Console.ReadKey(true);
                    while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter)
                    {
                        key = Console.ReadKey(true);
                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        s.AddNewCourse();
                    }
                }
                else
                {
                    break;
                }
                Console.Clear();
            }
            Application app = new Application();
            app.CreateNewStatement();
            app.WriteApplication();
            Console.ReadKey();
        }
    }
}
