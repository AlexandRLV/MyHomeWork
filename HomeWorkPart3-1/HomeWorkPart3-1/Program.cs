using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkPart3_1
{
    class Program
    {
        public static int NOD(int a,int b)
        {
            while ((a!=0)&&(b!=0))
            {
                if (a>b)
                {
                    a %= b;
                }
                else 
                {
                    b %= a;
                }
            }
            return (a + b);
        }
        
        public static int NOK(int a,int b)
        {
            return (a / NOD(a, b)) * b;
        }
        
        static void Main(string[] args)
        {
            int x, n, a, b;
            n = Convert.ToInt32(Console.ReadLine());
            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());
            
            x = NOK(a, b);
            for (int i=3;i<=n;i++)
            {
                a = Convert.ToInt32(Console.ReadLine());
                b = x;
                x = NOK(a, b);
            }
            Console.WriteLine(x);
        }
    }
}
