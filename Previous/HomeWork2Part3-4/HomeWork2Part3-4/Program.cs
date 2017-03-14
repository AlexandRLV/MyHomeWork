using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2Part3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, a, b, y, e, k,l;
            x = Convert.ToDouble(Console.ReadLine());
            e = Convert.ToDouble(Console.ReadLine());
            a = x;
            b = 1;
            y = 0;
            l = Math.Log(x, 2);

            k = 1;
            while (Math.Abs(y-l)>e)
            {
                a *= a;
                if (a>=2)
                {
                    y += b;
                }
                b /= 2;
                k++;
            }

            Console.WriteLine("{0}  {1}", k,y);
        }
    }
}
