using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2Part1_12
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, c, e, s, k, p, t;
            x = Convert.ToDouble(Console.ReadLine());
            e = Convert.ToDouble(Console.ReadLine());

            c = Math.Cos(x);
            c *= c;
            s = 1;
            k = 1;
            p = 2;
            t = 1;


            while (Math.Abs(s-c)>e)
            {
                p *= (-1) * 4 * x * x;
                t *= (2 * k) * (2 * k - 1);
                s += p / t;
                k++;
            }

            Console.WriteLine("Steps: {0}", k);
        }
    }
}
