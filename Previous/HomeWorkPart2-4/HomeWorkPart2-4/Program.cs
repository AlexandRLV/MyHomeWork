using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart2_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int f = Convert.ToInt32(Console.ReadLine()),
            s = Convert.ToInt32(Console.ReadLine()),
            n = Convert.ToInt32(Console.ReadLine()), m;
            if (n >= f)
            {
                m = n - f;
                if (m % s == 0)
                {
                    m = m / s + 1;
                    Console.WriteLine(m);
                }
                else
                {
                    Console.WriteLine("Не входит.");
                }
            }
            else
            {
                Console.WriteLine("Не входит.");
            }
        }
    }
}
