using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2Part4_4_4
{
    class Program
    {
        public static double F(double x)
        {
            return -Math.Sin(Math.Tan(x));
        }

        static void Main(string[] args)
        {
            double i, h, n, a, b, s;
            n = Convert.ToDouble(Console.ReadLine());
            a = 2;
            b = 3;
            h = (b - a) / n;

            s = 0;
            i = a;
            while (i<b-3*h)
            {
                s += (F(i) + F(i + 3 * h)) / 2 + F(i + h) + F(i + 2 * h);
                i += h;
            }

            s *= h;

            Console.WriteLine("{0} {1}", n, s);
        }
    }
}
