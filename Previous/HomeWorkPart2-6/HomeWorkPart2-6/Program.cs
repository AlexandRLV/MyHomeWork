using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart2_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int i, s = 0;
            for (i = 1; i < 1000; i++)
            {
                if ((i % 3 == 0) || (i % 5 == 0))
                {
                    s += i;
                }
            }
            Console.WriteLine(s);
        }
    }
}
