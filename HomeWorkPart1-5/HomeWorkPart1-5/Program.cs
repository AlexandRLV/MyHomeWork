using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart1_5
{
    class Program
    {
        static void Main(string[] args)
        {
            int x1 = Convert.ToInt32(Console.ReadLine()),
            y1 = Convert.ToInt32(Console.ReadLine()),
            x2 = Convert.ToInt32(Console.ReadLine()),
            y2 = Convert.ToInt32(Console.ReadLine()),
            d11, d12, d21, d22;

            d11 = y1 - x1;
            d21 = y2 - x2;

            y1 = 9 - y1;
            y2 = 9 - y2;
            d12 = y1 - x1;
            d22 = y2 - x2;
            if ((Math.Abs(d11 - d21) == 1) || (Math.Abs(d12 - d22) == 1))
            {
                Console.WriteLine("NEIGHBOUR");
            }
            if ((d11 - d21 == 0) || (d12 - d22 == 0))
            {
                Console.WriteLine("SAME");
            }
        }
    }
}
