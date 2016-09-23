using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart1_11
{
    class Program
    {
        static void Main(string[] args)
        {
            double x1 = Convert.ToDouble(Console.ReadLine()),
            y1 = Convert.ToDouble(Console.ReadLine()),
            x2 = Convert.ToDouble(Console.ReadLine()),
            y2 = Convert.ToDouble(Console.ReadLine()),
            x3 = Convert.ToDouble(Console.ReadLine()),
            y3 = Convert.ToDouble(Console.ReadLine()), s;


            s = 0.5 * Math.Abs((x1 - x3) * (y2 - y3) - (x2 - x3) * (y1 - y3));
            Console.WriteLine(s);
        }
    }
}
