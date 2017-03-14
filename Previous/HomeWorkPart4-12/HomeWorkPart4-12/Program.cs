using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart4_12
{
    class Program
    {
        public static double BinExp1(double a, int n)
        {
            if (n == 0)
            {
                return 1;
            }
            if (n % 2 == 1)
            {
                return BinExp1(a, n - 1) * a;
            }
            else
            {
                double b = BinExp1(a, n / 2);
                return b * b;
            }
        }


        static void Main(string[] args)
        {
            int x, y, z, k, m, n=0;
            k = Convert.ToInt32(Console.ReadLine());
            m = Convert.ToInt32(Console.ReadLine());
            for (x=0;x<m;x++)
            {
                for (y=0;y<m;y++)
                {
                    for (z=0;z<m;z++)
                    {
                        if (BinExp1(x,k)==BinExp1(y,k)+BinExp1(z,k)%m)
                        {
                            n++;
                            Console.WriteLine("{0}, {1}, {2}", x, y, z);
                        }
                    }
                }
            }
            Console.WriteLine("{0}", n);
        }
    }
}
