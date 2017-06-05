using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace SemestrovkaSort
{
    class Program
    {
        public static string path = "Data.txt";

        public static List<int> data = new List<int>(10000);

        public static void Shellsort<T>(IList<T> arr, ref int x) where T : IComparable
        {
            //Первоначальное значение d.
            int step = arr.Count() / 2;
            //Идём по циклу, уменьшая d.
            while (step > 0)
            {
                //Перебираем массив, начиная с номера d.
                for (int i = step; i < arr.Count; i++)
                {
                    T value = arr[i];
                    int j;
                    //Перебираем номера, отстоящие на расстоянии d от текущего, если они меньше, двигаем их.
                    for (j = i - step; (j >= 0) && (arr[j].CompareTo(value) > 0); j -= step)
                    {
                        arr[j + step] = arr[j];
                        x++;
                    }
                    //Вставляем на Новое место меньший элемент, если не двигались, ничего не изменится.
                    arr[j + step] = value;
                }
                //Уменьшаем значение d.
                step /= 2;
            }
        }

        public static void PrepareData(int x)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                Random r = new Random();
                for (int i = 0; i < 10000; i++)
                {
                    sw.Write($"{r.Next(x)} ");
                }
            }
        }

        public static void ReadData()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string[] s = sr.ReadToEnd().Split(' ');
                data = s.Take(10000).Select(a => int.Parse(a)).ToList();
            }
        }

        public static Stopwatch SortPart(int count, ref int x)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Shellsort(data.Take(count).ToList(), ref x);
            sw.Stop();
            return sw;
        }

        static void Main(string[] args)
        {
            for (int j = 10; j < 10000; j *= 10)
            {
                PrepareData(j);
                ReadData();
                using (StreamWriter s = new StreamWriter("Times"+$"{j}"+".txt"))
                {
                    for (int i = 100; i <= 10000; i += 100)
                    {
                        int x = 0;
                        Stopwatch sw = SortPart(i, ref x);
                        TimeSpan ts = sw.Elapsed;
                        string elapsedTime = String.Format($"{ts.TotalMilliseconds}");
                        s.WriteLine($"{elapsedTime} {x}");
                    }
                }
            }
        }
    }
}
