using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2Part4_1_4
{
    class Program
    {
        public static double F(double x)
        {
            return -Math.Sin(Math.Tan(x));
        }
        static void Main(string[] args)
        {
            double s, a, b, h, n,i;
            n = Convert.ToDouble(Console.ReadLine());
            a = 2;
            b = 3;
            h = (b - a) / n;

            s = 0;
            i = a;
            while (i<b)
            {
                s += F(i);
                i += n;
            }

            s *= h;
            Console.WriteLine("{0}  {1}", n, s);
        }
    }
}
