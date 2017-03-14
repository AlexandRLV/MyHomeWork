using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart3_5
{
    class Program
    {
        public static double Area(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            return 0.5 * Math.Abs((x1 - x3) * (y2 - y3) + (x2 - x3) * (y3 - y1));
        }
        

        public static bool IsIn(double x1, double y1, double x2, double y2, double x3, double y3,double x,double y)
        {
            double A1, A2, A3, A;
            A = Area(x1, x2, x3, y1, y2, y3);
            A1 = Area(x, x2, x3, y, y2, y3);
            A2 = Area(x1, x, x3, y1, y, y3);
            A3 = Area(x1, x2, x, y1, y2, y);
            if (A1+A2+A3==A)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            double x1, x2, x3, y1, y2, y3, x, y;
            int n, k;
            x1 = Convert.ToDouble(Console.ReadLine());
            y1 = Convert.ToDouble(Console.ReadLine());
            x2 = Convert.ToDouble(Console.ReadLine());
            y2 = Convert.ToDouble(Console.ReadLine());
            x3 = Convert.ToDouble(Console.ReadLine());
            y3 = Convert.ToDouble(Console.ReadLine());
            n = Convert.ToInt32(Console.ReadLine());
            k = Convert.ToInt32(Console.ReadLine());

            k = 0;
            for (int i=1;i<=n;i++)
            {
                if (IsIn(x1,y1,x2,y2,x3,y3,x,y))
                {
                    k++;
                }
            }
            Console.WriteLine(k);
        }
    }
}
