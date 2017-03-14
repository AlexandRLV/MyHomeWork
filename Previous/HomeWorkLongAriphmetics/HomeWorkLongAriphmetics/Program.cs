using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkLongAriphmetics
{
    class Program
    {
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

        public static LongNumber Task6(int k) // Задача номер СЕМЬ!
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber("2");
            // Переменная для текущего результата.
            LongNumber q = new LongNumber("1");
            // Переменная для проверки точности.
            LongNumber w = new LongNumber("1");
            // "Длинная" единица для удобства.
            LongNumber x = new LongNumber("1");
            // Итерационная переменная.
            int i = 2;
            // Пременная для знака.
            int p = -1;
            // Пока не дошли до указанной точности и не вышли за границы int32.
            while ((a.GetExp() <= k * 2) && (i < int.MaxValue))
            {
                // Считаем.
                q.SetMaxExp(k);
                q.EqualTo(x / i);
                q.EqualTo(q * p);
                a.EqualTo(a + q);
                // Проверяем точность.
                w.EqualTo(a - w);
                int j = a.num.Count;
                if (a.GetExp() > k)
                {
                    while (j >= k)
                    {
                        if (w.num[j - 1] != 0)
                        {
                            break;
                        }
                        j--;
                    }
                    if (j <= k)
                    {
                        break;
                    }
                }
                // Сохраняем текущий результат.
                w.EqualTo(a);
                // Двигаем итерацию.
                i+=2;
                // Меняем знак.
                p *= -1;
            }
            a.EqualTo(a * 2);
            return a;
        }

        public static LongNumber Task7(int k) // Задача номер СЕМЬ!
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber("2");
            // Переменная для текущего результата.
            LongNumber q = new LongNumber("1");
            // Переменная для проверки точности.
            LongNumber w = new LongNumber("1");
            // Итерационная переменная.
            int i = 2;
            // Пока не дошли до указанной точности и не вышли за границы int32.
            while ((a.GetExp()<=k*2)&&(i<int.MaxValue))
            {
                // Считаем.
                q.SetMaxExp(k);
                q.EqualTo(q / i);
                a.EqualTo(a + q);
                // Проверяем точность.
                w.EqualTo(a - w);
                int j = a.num.Count;
                if (a.GetExp()>k)
                {
                    while (j >= k)
                    {
                        if (w.num[j-1] != 0)
                        {
                            break;
                        }
                        j--;
                    }
                    if (j <= k)
                    {
                        break;
                    }
                }
                // Сохраняем текущий результат.
                w.EqualTo(a);
                i++;
            }
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
            LongNumber.UpToPow(a, n);
            a.EqualTo(b-a);
            b.EqualTo(b - k);
            a.EqualTo(a / b);
            a.RemoveZeros();
            return a;
        }

        public static LongNumber Task13 (LongNumber x) // Задача номер ТРИНАДЦАТЬ!
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber();
            // Длинная единица, для удобства.
            LongNumber b = new LongNumber("1");
            // Если модуль икса больше единицы.
            if (x.Abs(x)>b)
            {
                // Возвращаем ноль.
                return a;
            }
            // Если всё нормально.
            else
            {
                // Считаем 100 шагов.
                int n = 100;
                // Переменная для текущего шага.
                // Считаем со 2 шага, поэтому изначально она равна первому шагу, то бишь иксу.
                LongNumber k = new LongNumber("1");
                k.EqualTo(x);
                // Переменная для знака.
                int p = 1;
                LongNumber y = new LongNumber("1");
                // Считаем ряд со второго шага в цикле.
                for (int i=2;i<=n;i+=2)
                {
                    k.EqualTo(k * x);
                    k.EqualTo(k * x);
                    k.EqualTo(k * p);
                    y.EqualTo(y*(i*(i-1)));
                    k.EqualTo(k / y);
                    p *= -1;
                    a.EqualTo(a + k);
                }
                // Удаляем лишние нули и возвращаем.
                a.RemoveZeros();
                return a;
            }
        }

        public static void Task16() // Задача номер ШЕСТНАДЦАТЬ!
        {
            // Массив со степенями.
            int[] p = new int[] { 2, 3, 5, 7, 13, 17, 19, 31, 61, 89 };
            LongNumber q = new LongNumber("2");
            LongNumber b = new LongNumber("1");
            // Переменная для ответа.
            LongNumber a = new LongNumber();
            // Идём по степеням в массиве и считаем числа Мерсенна.
            for (int i=0;i<10;i++)
            {
                a.EqualTo(q);
                a.EqualTo(LongNumber.UpToPow(a,p[i]));
                a.EqualTo(a - b);
                a.WriteOut();
            }
        }

        static void Main(string[] args)
        {
            LongNumber a = new LongNumber();
            Console.WriteLine("В этой программке работают 1, 4, 6, 7, 10, 13 и 16 задачи.");
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
            Console.WriteLine("Введите число знаков для шестой задачи.");
            k = Convert.ToInt32(Console.ReadLine());
            a.EqualTo(Task6(k));
            a.WriteOut();
            Console.WriteLine("Введите число знаков для седьмой задачи.");
            k = Convert.ToInt32(Console.ReadLine());
            a.EqualTo(Task7(k));
            a.WriteOut();
            Console.WriteLine("Введите основание и степень для 10 задачи.");
            s = Console.ReadLine();
            s1 = Console.ReadLine();
            x1 = new LongNumber(s);
            y1 = new LongNumber(s1);
            a.EqualTo(Task10(x1, y1));
            Console.WriteLine("Сиё есть 10 задача.");
            a.WriteOut();
            Console.WriteLine("Введите число Х для тринадцатой задачи.");
            s = Console.ReadLine();
            x1 = new LongNumber(s);
            a.EqualTo(Task13(x1));
            Console.WriteLine("Сиё есть 13 задача.");
            a.WriteOut();
            Console.WriteLine("Сиё есть 16 задача.");
            Task16();
        }
    }
}
