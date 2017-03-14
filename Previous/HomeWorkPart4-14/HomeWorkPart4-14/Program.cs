using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart4_14
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, n;
            n = Convert.ToInt32(Console.ReadLine());
            a = 1;
            for (int i=1;i<=n;i++)
            {
                a *= (i % 10);
                a %= 10;
            }
            Console.WriteLine(a);
        }
    }
}
