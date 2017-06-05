using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tasks
{
    class Program
    {
        // Метод вычисления произведения.
        public static int[] Calculate(int[,] matrix, int[] vector)
        {
            // Создаём массив для ответа.
            int[] result = new int[matrix.GetLength(0)];
            // Создаём массив Task, каждый элемент - ячейка массива ответа.
            Task<int>[] task = new Task<int>[matrix.GetLength(0)];
            // Проходим по строкам матрицы, умножая каждую на вектор.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                task[i] = TaskCalc(i, matrix, vector);
            }
            return task.Select(a => a.Result).ToArray();
        }

        // Метод умножения строки на вектор.
        public static Task<int> TaskCalc(object i, int[,] matrix, int[] vector)
        {
            int n = (int)i;
            return Task.Run(() =>
            {
                int k = 0;
                for (int j = 0; j < vector.Length; j++)
                {
                    k += matrix[n, j] * vector[j];
                }
                return k;
            });
            
        }

        // Метод умножения матрицы на вектор стандартным способом, ничего не возвращаем, т.к. нужно лишь замерить время.
        public static void Calculate1(int[,] matrix, int[] vector)
        {
            int[] result = new int[vector.Length];
            for (int i = 0; i< matrix.GetLength(0); i++)
            {
                for (int j = 0; j < vector.Length; j++)
                {
                    result[i] += matrix[i, j] * vector[i];
                }
            }
        }

        // Метод замера времени для Task.
        public static Stopwatch Check(int[,] matrix, int[] vector)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int[] result = Calculate(matrix, vector);
            sw.Stop();
            return sw;
        }

        // Метод замера времени для стандартного способа.
        public static Stopwatch Check2(int[,] matrix, int[] vector)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Calculate1(matrix, vector);
            sw.Stop();
            return sw;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Размер массива: Время подсчёта с помощью Task - Время подсчёта стандартным методом. (в мс)");
            // Генерируем случайные массивы разной длины, замеряем для них оба времени, выводим результат.
            for (int k = 10; k <= 1000; k += 10)
            {
                int[,] matrix = new int[k, k];
                int[] vector = new int[k];
                Random r = new Random();
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        matrix[i, j] = r.Next(10);
                    }
                    vector[i] = r.Next(10);
                }
                Stopwatch sw = Check(matrix, vector);
                TimeSpan ts = sw.Elapsed;
                string elapsedTime = String.Format($"{ts.TotalMilliseconds}");
                sw = Check2(matrix, vector);
                ts = sw.Elapsed;
                string elapsedTime1 = String.Format($"{ts.TotalMilliseconds}");
                Console.WriteLine($"{k}: {elapsedTime} - {elapsedTime1}");
            }
        }
    }
}
