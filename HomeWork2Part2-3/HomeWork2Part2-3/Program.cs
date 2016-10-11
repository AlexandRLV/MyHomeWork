using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2Part2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            double s, pi, e, a, b,k;
            pi = Math.PI;
            e = Convert.ToDouble(Console.ReadLine());

            s = 1;
            a = 1;
            b = 1;
            k = 1;

            while (Math.Abs(s - pi) > e)
            {
                a *= -1;
                b *= 3;
                s += a / (b * (2 * k + 1));
                k++;
            }

            Console.WriteLine("Steps: {0}, Result: {1}", k, s);
        }
    }
}
