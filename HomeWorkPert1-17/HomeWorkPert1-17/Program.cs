using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPert1_17
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = Convert.ToInt32(Console.ReadLine()),
            b = Convert.ToInt32(Console.ReadLine()),
            c = Convert.ToInt32(Console.ReadLine()),
            d = Convert.ToInt32(Console.ReadLine()),
            e = Convert.ToInt32(Console.ReadLine()),
            f = Convert.ToInt32(Console.ReadLine()), x;


            x = f + e * 10 + d * 100 + c * 1000 + b * 10000 + a * 100000;

            x++;
            a = x / 100000;
            b = x / 10000 % 10;
            c = x / 1000 % 10;
            d = x / 100 % 10;
            e = x / 10 % 10;
            f = x % 10;

            if (a + c + e == b + d + f)
            {
                Console.WriteLine("Почти счастливый.");
            }

            x -= 2;

            a = x / 100000;
            b = x / 10000 % 10;
            c = x / 1000 % 10;
            d = x / 100 % 10;
            e = x / 10 % 10;
            f = x % 10;

            if (a + c + e == b + d + f)
            {
                Console.WriteLine("Почти счастливый.");
            }
        }
    }
}
