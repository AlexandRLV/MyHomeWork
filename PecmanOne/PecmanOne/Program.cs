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
            while (true)
            {
                MainMenu menu = new MainMenu();
                int q = menu.Start();
                if (q==0)
                {
                    return;
                }
                Console.Clear();
                Console.WriteLine("This is a Pac-Man. You are playing as a O, your task is to collect all points °.\r\nAfraid the ghosts R!\r\nCollect lives @ and energizers $!");
                Console.WriteLine("Enter your name.");
                string name = Console.ReadLine();                
                Console.WriteLine("Enter your level.");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                Controller c = new Controller(x);
                c.Start();
                int r = c.GetRecord();
                Console.Clear();
                Console.WriteLine("Thank you for playing!");
                Saver s = new Saver();
                Console.WriteLine("Your name: {0}", name);
                Console.WriteLine("Your record: {0}", r);                    
                s.AddNewRecord(name, r);
                Console.WriteLine("Records:");
                s.WriteRecords();
                Console.WriteLine("Press space to retry, Escape to exit.");
                ConsoleKeyInfo key = new ConsoleKeyInfo();                
                bool ready = false;
                bool retry = false;
                while (!ready)
                {
                    key = Console.ReadKey();
                    if (key.Key==ConsoleKey.Spacebar)
                    {
                        ready = true;
                        retry = true;
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        ready = true;
                        retry = false;
                    }
                }
                if (retry)
                {
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }
            
        }
    }
}