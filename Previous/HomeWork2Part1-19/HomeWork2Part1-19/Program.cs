using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2Part1_19
{
    class Program
    {
        static void Main(string[] args)
        {
            double pi, k, x, e, s, p;
            pi = Math.PI;
            pi *= pi;
            pi /= 18;
            e = Convert.ToDouble(Console.ReadLine());

            s = 0;
            k = 2;
            p = 1;
            x = 1;

            while (Math.Abs(s - pi) > e)
            {
                p *= (k - 1) * (k - 1);
                x *= (2 * k) * (2 * k - 1);
                s += p / x;
                k++;
            }

            Console.WriteLine("Steps: {0}", k);
        }
    }
}
