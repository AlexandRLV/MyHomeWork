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
                Console.WriteLine("You are playing in the best game!");
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
                Console.WriteLine("Press space to retry, another key to exit.");
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                key = Console.ReadKey();
                if (key.Key != ConsoleKey.Spacebar)
                {
                    break;
                }
            }
            
        }
    }
}