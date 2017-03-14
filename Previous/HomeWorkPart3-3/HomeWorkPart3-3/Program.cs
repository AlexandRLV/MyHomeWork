using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart3_3
{
    class Program
    {
        public static double Dist(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }


        static void Main(string[] args)
        {
            int n, k, j;
            double 
            x0=Convert.ToDouble(Console.ReadLine()), 
            y0 = Convert.ToDouble(Console.ReadLine()), 
            r = Convert.ToDouble(Console.ReadLine()),
            x, y, min, d;
            n = Convert.ToInt32(Console.ReadLine());
            double[] Ax = new double[1];
            double[] Ay = new double[1];


            x = Convert.ToDouble(Console.ReadLine());
            y = Convert.ToDouble(Console.ReadLine()); 
            j = 1;
            while ((Dist(x0, y0, x, y) <= r) && (j <= n)) 
            {
                j++;
                x = Convert.ToDouble(Console.ReadLine());
                y = Convert.ToDouble(Console.ReadLine());
            }
            if (j<n)
            {
                min = Dist(x0, y0, x, y);
                Ax[0] = x;
                Ay[0] = y;
                k = 1;
                for (int i=j;i<n;i++)
                {
                    x = Convert.ToDouble(Console.ReadLine());
                    y = Convert.ToDouble(Console.ReadLine());
                    d = Dist(x0, y0, x, y);
                    if ((d<min)&&(d>r))
                    {
                        min = d;
                        k = 1;
                        Array.Resize<double>(ref Ax, k);
                        Array.Resize<double>(ref Ay, k);
                        Ax[0] = x;
                        Ay[0] = y;
                    }
                    else if (d==min)
                    {
                        k++;
                        Array.Resize<double>(ref Ax, k);
                        Array.Resize<double>(ref Ay, k);
                        Ax[k-1] = x;
                        Ay[k-1] = y;
                    }
                }
            }
            else if (j==n)
            {
                Ax[0] = x;
                Ay[0] = y;
                k = 1;
            }
            else
            {
                k = 0;
            }

            if (k>0)
            {
                Console.WriteLine("Всего {0} точек.", k);
                for (int i=0;i<k;i++)
                {
                    Console.WriteLine("{0}, {1}", Ax[i], Ay[i]);
                }
            }
            else
            {
                Console.WriteLine("Нет таких точек.");
            }
        }
    }
}
