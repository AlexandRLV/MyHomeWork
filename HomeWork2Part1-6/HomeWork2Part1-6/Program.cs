using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork2Part1_6
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, p, k, e, s,t,a;
            x = Convert.ToDouble(Console.ReadLine());
            e = Convert.ToDouble(Console.ReadLine());

            k = 1;
            s = x;
            p = x;
            a = 1;
            t = Math.Tan(x);

            while (Math.Abs(s-t)>e)
            {
                p *= (-1)*x * x;
                a += 2;
                s += p / a;
                k++;
            }

            Console.WriteLine("Steps: {0}", k);
        }
    }
}
