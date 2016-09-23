using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart2_8
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
            double n = Convert.ToDouble(Console.ReadLine());
            int k = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(BinExp1(n, k));
        }
    }
}
