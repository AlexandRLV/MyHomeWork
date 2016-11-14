using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkLongAriphmetics
{
    class Program
    {
        public static int Fact(int x) // Факториал числа.
        {
            int k = 1;
            for (int i=1;i<=x;i++)
            {
                k *= i;
            }
            return k;
        }

        public static LongNumber Task1 (int k) // Задача номер ОДИН!
        {
            // Переменные для ответа.
            LongNumber a = new LongNumber("1");
            LongNumber x = new LongNumber("1");
            // Дополнительная переменная.
            int p;
            // Собственно, сам счёт.
            for (int i = 2; i <= k; i++)
            {
                p = i * i;
                x.EqualTo(a * p);
                a.EqualTo(a + x);
            }
            // Возвращаем.
            a.RemoveZeros();
            return a;
        }

        public static LongNumber Task10 (LongNumber k, LongNumber n) // Задача номер ДЕСЯТЬ!
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber();
            // Длинная единица, для удобства.
            LongNumber b = new LongNumber("1");
            // Считаем сумму геометрической прогрессии.
            a.EqualTo(k);
            a.UpToPow(n);
            a.EqualTo(b-a);
            b.EqualTo(b - k);
            a.EqualTo(a / b);
            a.RemoveZeros();
            return a;
        }

        static void Main(string[] args)
        {
            LongNumber a = new LongNumber("1");
            Console.WriteLine("В этой программе работают 1, 4 и 10 задачи.");
            Console.WriteLine("Введите число К для первой задачи.");
            int k = Convert.ToInt32(Console.ReadLine());
            a.EqualTo(Task1(k));
            Console.WriteLine("Сиё есть 1 задача.");
            a.WriteOut();
            Console.WriteLine("Введите два длинных числа для 4 задачи.");
            string s = Console.ReadLine();
            string s1 = Console.ReadLine();
            LongNumber x1 = new LongNumber(s);
            LongNumber y1 = new LongNumber(s1);
            a.EqualTo(x1 * y1);
            Console.WriteLine("Сиё есть умножение.");
            a.WriteOut();
            a.EqualTo(x1 / y1);
            Console.WriteLine("Сиё есть деление.");
            a.WriteOut();
            Console.WriteLine("Введите основание и степень для 10 задачи.");
            s = Console.ReadLine();
            s1 = Console.ReadLine();
            x1 = new LongNumber(s);
            y1 = new LongNumber(s1);
            a.EqualTo(Task10(x1,y1));
            Console.WriteLine("Сиё есть 10 задача.");
            a.WriteOut();
        }
    }
}
