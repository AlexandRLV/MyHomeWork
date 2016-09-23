using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart4_16
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, k, s, a, b;
            n = Convert.ToInt32(Console.ReadLine());
            a = 1;
            b = 1;
            k = 1;
            s = 1;
            while(s!=n)
            {
                if (s<n)
                {
                    b++;
                    s += b;
                    k++;
                }
                else
                {
                    s -= a;
                    a++;
                    k--;
                }
            }
            Console.WriteLine(k);
        }
    }
}
