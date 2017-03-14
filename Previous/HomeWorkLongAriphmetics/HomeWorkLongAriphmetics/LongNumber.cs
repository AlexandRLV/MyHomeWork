using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkLongAriphmetics
{
    // Класс длинных чисел.
    class LongNumber
    {
        // Список цифр (мантисса).
        public readonly List<int> num = new List<int>();
        // Позиция запятой(экспонента).
        private int exp;
        // Знак числа.
        private int sign;
        // Переменная для хранения точности счёта(сколько знаков считать).
        private int maxexp=10;

        public LongNumber(string s) // Конструктор, принимающий на вход строку с числом.
        {
            // Запоминаем знак.
            if (s.Contains('-'))
            {
                this.sign = -1;
                s = s.Substring(1);
            }
            else
            {
                this.sign = 1;
            }
            // Запоминаем экспоненту, если число дробное.
            if (s.Contains('.'))
            {
                this.exp = s.Length - 1 - s.IndexOf('.');
                s=s.Remove(s.IndexOf('.'), 1);
            }
            else
            {
                this.exp = 0;
            }
            // Записываем в список по одной цифре в ячейке.
            while (s.Length>0)
            {
                this.num.Add(Convert.ToInt32(s.Substring(s.Length - 1)));
                s=s.Remove(s.Length - 1);
            }
            this.RemoveZeros();
        }

        public LongNumber() // Конструктор, создающий пустое число.
        {
            // Знак - плюс, экспонента - 0, одна ячейка в списке =0.
            this.sign = 1;
            this.exp = 0;
            this.num.Add(0);
        }

        public void SetMaxExp(int k) // Метод задания точности.
        {
            this.maxexp = k;
        }

        public int GetExp() // Метод получения точности.
        {
            return this.exp;
        }

        public void WriteOut() // Метод вывода числа.
        {
            // Запоминаем длину.
            int n = this.num.Count;
            // Удаляем лишние нули.
            this.RemoveZeros();
            // Выводим минус, если отрицательное.
            if (this.sign<0)
            {
                Console.Write("-");
            }
            // Выводим цифры до запятой.
            for (int i=n-1;i>=this.exp;i--)
            {
                Console.Write("{0}", this.num[i]);
            }
            // Пишем запятую, если число дробное.
            if (this.exp > 0)
            {
                Console.Write(".");
            }
            // Выводим числа после запятой.
            for (int i = this.exp - 1; i >= 0; i--)
            {
                Console.Write("{0}", this.num[i]);
            }
            Console.WriteLine();
        }

        public void RemoveZeros() // Удаление незначащих нулей справа и слева.
        {
            // Добавление значащих нулей.
            this.AddZeros();
            // Запоминаем длину.
            int n = this.num.Count;
            // Удаляем нули слева пока не встретится цифра, отличная от нуля.
            for (int i = n - 1; i > this.exp; i--)
            {
                if (this.num[i]==0)
                {
                    this.num.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            // Переменная для количества нулей справа.
            int k = 0;
            // Считаем, сколько нулей справа можно удалить.
            for (int i=0;i<this.exp;i++)
            {
                if (this.num[i] == 0)
                {
                    k++;
                }
                if (this.num[i] != 0)
                {
                    break;
                }
            }
            // Удаляем их и меняем экспоненту.
            this.num.RemoveRange(0, k);
            this.exp-= k;
        }

        public void AddZeros() // Добавление значащий нулей, если экспонента больше длины мантиссы.
        {
            // Если экспонента больше, чем длина мантиссы, добавляем нули слева, пока не дойдём до позиции экспоненты.
            if (this.exp >= this.num.Count)
            {
                for (int i = this.num.Count; i <= this.exp; i++)
                {
                    this.num.Add(0);
                }
            }
        }

        public LongNumber Abs(LongNumber x) // Метод, возвращающий модуль числа.
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber();
            // Присваиваем, чтобы не менять исходную переменную.
            a.EqualTo(x);
            // Возвращаем модуль знака и возвращаем число.
            a.sign = Math.Abs(a.sign);
            return a;
        }

        public static LongNumber operator + (LongNumber x1, LongNumber y1) // Перегрузка оператора сложения.
        {
            // Создаём переменную для ответа.
            LongNumber a = new LongNumber();
            // Сохраняем входные переменные, т.к., возможно, будем их позже менять.
            LongNumber x = new LongNumber();
            x.EqualTo(x1);
            LongNumber y = new LongNumber();
            y.EqualTo(y1);
            // Запоминаем длины чисел.
            int xn = x.num.Count, yn = y.num.Count;
            // Проверяем равенство знаков.
            if (x.sign==y.sign)
            {
                // Переменные для текущего результата и переноса.
                int b = 0;
                int p = 0;
                // Удаляем все ячейки в переменной ответа.
                a.num.RemoveAt(0);
                // Разница в длинах дробных частей.
                int c = Math.Abs(x.exp - y.exp);
                // Сравниваем длины дробных частей.
                if (x.exp>=y.exp)
                {
                    // Если длиннее первое, переписываем более длинную часть в ответ.
                    for (int i = 0; i < c; i++)
                    {
                        a.num.Add(x.num[i]);
                    }
                    // Считаем сумму общей части, учитывая, что второе сдвинуто относительно первого
                    // для совпадения позиции запятой.
                    for (int i = c; i < Math.Min(xn, yn + c); i++)
                    {
                        b = x.num[i] + y.num[i - c] + p;
                        a.num.Add(b % 10);
                        p = b / 10;
                    }
                    // Сравниваем длины чисел.
                    if (yn + c >= xn)
                    {
                        // Если второе длиннее, переписываем его в ответ, учитывая перенос.
                        for (int i = xn; i < yn+c; i++)
                        {
                            b = y.num[i-c] + p;
                            a.num.Add(b % 10);
                            p = b / 10;
                        }
                    }
                    else
                    {
                        // Если первое длиннее, переписываем его в ответ, учитывая перенос.
                        for (int i=yn+c;i<xn;i++)
                        {
                            b = x.num[i] + p;
                            a.num.Add(b % 10);
                            p = b / 10;
                        }
                    }
                    // Записываем экспоненту в ответ.
                    a.exp = x.exp;
                }
                else
                {
                    // Если длиннее второе, переписываем более длинную часть в ответ.
                    for (int i = 0; i < c; i++)
                    {
                        a.num.Add(y.num[i]);
                    }
                    // Считаем сумму общей части.
                    for (int i = c; i < Math.Min(xn+c, yn); i++)
                    {
                        b = x.num[i-c] + y.num[i] + p;
                        a.num.Add(b % 10);
                        p = b / 10;
                    }
                    // Сравниваем длины чисел.
                    if (yn>= xn+c)
                    {
                        // Если длиннее второе , переписываем его в ответ, учитывая перенос.
                        for (int i = xn+c; i < yn; i++)
                        {
                            b = y.num[i] + p;
                            a.num.Add(b % 10);
                            p = b / 10;
                        }
                    }
                    else
                    {
                        // Если длиннее первое, переписываем его в ответ, учитывая перенос.
                        for (int i = yn; i < xn+c; i++)
                        {
                            b = x.num[i-c] + p;
                            a.num.Add(b % 10);
                            p = b / 10;
                        }
                    }
                    // Записываем экспоненту в ответ.
                    a.exp = y.exp;
                }
                // Записывем знак.
                a.sign = x.sign;
            }
            // Если знаки не равны, меняем один и считаем разность.
            else
            {
                y.sign *= -1;
                a.EqualTo(x - y);
            }
            // Удаляем лицние нули, если они есть.
            a.RemoveZeros();
            // Возвращаем ответ.
            return a;
        }

        public static LongNumber operator + (LongNumber x, int y) // Сложение длинного и обычного чисел.
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber();
            // Превращаем число в длинное.
            string s = Convert.ToString(y);
            LongNumber y1 = new LongNumber(s);
            // Считаем сумму длинных чисел.
            a.EqualTo(x + y1);
            return a;
        }

        public static LongNumber operator - (LongNumber x1, LongNumber y1) // Перегрузка оператора вычитанияы
        {
            // Создаём переменную для ответа.
            LongNumber a = new LongNumber();
            // Сохраняем входные переменные, т.к., возможно, будем их позже менять.
            LongNumber x = new LongNumber();
            x.EqualTo(x1);
            LongNumber y = new LongNumber();
            y.EqualTo(y1);
            // Запоминаем длины чисел.
            int xn = x.num.Count, yn = y.num.Count;
            // Если уменьшаемое короче, делаем так: x-y=-y+x=-(y-x).
            if (xn-x.exp<yn-y.exp)
            {
                x.sign *= -1;
                y.sign *= -1;
                a.EqualTo(y - x);
                a.RemoveZeros();
                return a;
            }
            // Проверяем равенство знаков.
            if (x.sign == y.sign)
            {
                // Переменные для текущего результата и переноса.
                int b = 0;
                int p = 0;
                // Удаляем все ячейки в переменной ответа.
                a.num.RemoveAt(0);
                // Разница в длинах дробных частей.
                int c = Math.Abs(x.exp - y.exp);
                // Сравниваем длины дробных частей.
                if (x.exp>=y.exp)
                {
                    // Если первое не короче второго (дробная часть), перепысываем более длинную часть в ответ.
                    for (int i=0;i<c;i++)
                    {
                        a.num.Add(x.num[i]);
                    }
                    // Считаем общую часть.
                    for (int i=c;i<Math.Min(xn,yn+c);i++)
                    {
                        b = x.num[i] - y.num[i-c] - p;
                        // Если текущий результат меньше нуля, добавляем к нему основание и занимаем единицу.
                        if (b < 0)
                        {
                            b += 10;
                            p = 1;
                        }
                        else
                        {
                            p = 0;
                        }
                        a.num.Add(b);
                    }
                    // Уменьшаемое не короче вычитаемого, поэтому считаем более длинную часть, если она есть.
                    for (int i=yn+c;i<xn;i++)
                    {
                        b = x.num[i] - p;
                        if (b<0)
                        {
                            b += 10;
                            p = 1;
                        }
                        else
                        {
                            p = 0;
                        }
                        a.num.Add(b);                        
                    }
                    // Записываем в ответ экспоненту.
                    a.exp = x.exp;
                }
                else
                {
                    // Если у второго длинная часть больше, вычитаем более длинную часть из нулей.
                    for (int i=0;i<c;i++)
                    {
                        b = 10 - p - y.num[i];
                        p = 1;
                        a.num.Add(b);
                    }
                    // Считаем общую часть.
                    for (int i=c;i<yn;i++)
                    {
                        b = x.num[i - c] - y.num[i] - p;
                        if (b<0)
                        {
                            p = 1;
                            b += 10;
                        }
                        else
                        {
                            p = 0;
                        }
                        a.num.Add(b);
                    }
                    // Уменьшаемое не короче вычитаемого, поэтому считаем более длинную часть, если она есть.
                    for (int i = yn - c; i < xn; i++)
                    {
                        b = x.num[i] - p;
                        if (b < 0)
                        {
                            p = 1;
                            b += 10;
                        }
                        else
                        {
                            p = 0;
                        }
                        a.num.Add(b);
                    }
                    a.exp = y.exp;
                }
                // Записываем знак.
                a.sign = y.sign;
                // Проверяем, осталось ли что-то в переносе.
                if (p>0)
                {
                    // Если осталось, вычитаем ответ из старшего разряда и меняем знак.
                    a.RemoveZeros();
                    p = 0;
                    for (int i=0;i<a.num.Count;i++)
                    {
                        b = 10 - a.num[i] - p;
                        p = 1;
                        a.num[i] = b;
                    }
                    a.sign *= -1;
                }
            }
            else
            {
                // Если знаки не равны, меняем знак и считаем сумму.
                y.sign *= -1;
                a.EqualTo(x + y);
            }
            // Удаляем лишние нули и возвращаем.
            a.RemoveZeros();
            return a;
        }

        public static LongNumber operator * (LongNumber x1, LongNumber y1) // Перегрузка умножения длинных чисел.
        {
            // Создаём переменную для ответа.
            LongNumber a = new LongNumber();
            // Сохраняем входные переменные, т.к., возможно, будем их позже менять.
            LongNumber x = new LongNumber();
            x.EqualTo(x1);
            LongNumber y = new LongNumber();
            y.EqualTo(y1);
            // Запоминаем длины чисел.
            int xn = x.num.Count, yn = y.num.Count;
            // Переменная для текущего результата.
            LongNumber n = new LongNumber();
            n.EqualTo(y * x.num[0]);
            a.EqualTo(a + n);
            // Перебираем цифры в первом числе.
            for (int i=1;i<xn;i++)
            {
                // Умножаем цифру первого числа на второе.
                n.EqualTo(y * x.num[i]);
                // Сдвигаем текущий результат на позицию цифры.
                for (int j=0;j<i;j++)
                {
                    n.num.Insert(0, 0);
                }
                // Складываем с ответом.
                a.EqualTo(a + n);
            }
            // Экпонента ответа - сумма экспонент.
            a.exp = x.exp + y.exp;
            // Удаляем лишние нули.
            a.RemoveZeros();
            // Перемножаем знаки и возвращаем.
            a.sign = x.sign * y.sign;
            return a;
        }

        public static LongNumber operator * (LongNumber x, int y) // Перегрузка умножения длинного и обычного числа.
        {
            // Запоминаем длину числа.
            int xn = x.num.Count;
            // Переменная для ответа.
            LongNumber a = new LongNumber();
            // В ответе есть ячейка с нулём, удаляем её, чтобы можно было работать только с методом Add.
            a.num.RemoveAt(0);
            // Запоминаем знак короткого числа и меняем это число, если оно меньше нуля, для удобства.
            int sign;
            if (y >= 0)
            {
                sign = 1;
            }
            else
            {
                sign = -1;
                y *= -1;
            }
            // Переменные для текущего результата и переноса.
            int b = 0;
            int p = 0;
            // Если входное число меньше 10.
            if (y/10==0)
            {
                // Умножаем его на длинное.
                for (int i=0;i<xn;i++)
                {
                    b = x.num[i] * y+p;
                    p = b / 10;
                    a.num.Add(b % 10);
                }
                // Если после умножения остался перенос, записываем его в новую ячейку.
                if (p>0)
                {
                    a.num.Add(p);
                }
                // Записываем знак.
                a.sign = x.sign * sign;
            }
            // Если входное число из нескольких цифр.
            else
            {
                // Превращаем его в длинное и считаем произведение длинных.
                string s = Convert.ToString(y);
                LongNumber y0 = new LongNumber(s);
                a.EqualTo(x * y0);
            }
            a.exp = x.exp;
            // Удаляем нули и возвращаем.
            a.RemoveZeros();
            return a;
        }

        public void PlusOne() // Метод увеличения на 1.
        {
            // Переменные для текущего результата и переноса.
            int b;
            int p = 1;
            // Запоминаем позицию экспоненты.
            int i = this.exp;
            // Пока есть что переносить и не дошли до конца идём от позиции экспоненты и прибавляем единицу.
            while ((p>0)&&(i<this.num.Count))
            {
                b = this.num[i] + p;
                p = b / 10;
                this.num[i] = b%10;
                i++;
            }
            // Если осталось, что переносить, записываем в новую ячейку.
            if (p>0)
            {
                this.num.Add(p);
            }
        }

        public static bool operator > (LongNumber x1, LongNumber y1) // Перегрузка сравнения 1.
        {
            // Сохраняем входные переменные, т.к., возможно, будем их позже менять.
            LongNumber x = new LongNumber();
            x.EqualTo(x1);
            LongNumber y = new LongNumber();
            y.EqualTo(y1);
            // Переменная для ответа.
            bool b = false;
            // Если знак первого меньше, то ложь.
            if (x.sign<y.sign)            
                return false;            
            // Если знак второго меньше - истина.
            else if (x.sign>y.sign)
                return true;
            // Знаки равны, теперь если у первого длиннее целая часть - истина.
            if (x.num.Count-x.exp>y.num.Count-y.exp)
                return true;
            // Если короче - ложь.
            else if (x.num.Count-x.exp<y.num.Count-y.exp)
                return false;
            // Целые части одинаковой длины.
            else
            {
                // Приводим оба числа к одному количеству знаков, добавляя нули в более короткое.
                int c = Math.Abs(x.exp - y.exp);
                if (x.exp>y.exp)
                {
                    for (int i=0;i<c;i++)
                    {
                        y.num.Insert(0, 0);
                    }               
                }
                else if (x.exp<y.exp)
                {
                    for (int i = 0; i < c; i++)
                    {
                        x.num.Insert(0, 0);
                    }
                }
                // Перебираем все цифры.
                for (int i = 0; i < x.num.Count; i++)
                {
                    // Если цифра первого числа больше цифры того же разряда второго числа, флаг - истина.
                    if (x.num[i]>y.num[i])
                    {
                        b = true;
                    }
                    // Если меньше - ложь. Равенство не меняет флаг.
                    if (x.num[i] < y.num[i])
                    {
                        b = false;
                    }
                }
                // Возвращаем ответ.
                return b;
            }
        }

        public static bool operator < (LongNumber x, LongNumber y) // Перегрузка сравнения 2.
        {
            // Меняем местами и считаем наоборот.
            return y > x;
        }

        public static LongNumber operator / (LongNumber x1, int y) // Перегрузка деления.
        {
            // Переменная ддля ответа.
            LongNumber a = new LongNumber();
            // Сохраняем входные переменные, т.к., возможно, будем их позже менять.
            // Интовое число не сохраняем, т.к. оно передаётся по значению и исходное меняться не будет.
            LongNumber x = new LongNumber();
            x.EqualTo(x1);
            // Запоминаем длину.
            int xn = x.num.Count;
            // Переменные для текущего результата и переноса.
            int b = 0;
            int p = 0;
            // Если короткое число меньше нуля, меняем его знак. Знак ответа противоположен знаку первого числа.
            if (y<0)
            {
                a.sign = x.sign * (-1);
                y *= -1;
            }
            // Иначе знак ответа равен знаку первого числа.
            else
            {
                a.sign = x.sign;
            }
            // Если во втором числе одна цифра.
            if (y/10==0)
            {
                // Делаем ответ полностью пустым, для удобства.
                a.num.RemoveAt(0);
                // Делим первое число на второе, результат(одна цифра) вставляем в начало ответа.
                for (int i=xn-1;i>=0;i--)
                {
                    b = p * 10 + x.num[i];
                    p = b % y;
                    b /= y;                    
                    a.num.Insert(0, b);
                }
                // Записываем эспоненту ответа.
                a.exp = x.exp;
                // Если остался перенос, считаем до 15 знака после запятой.
                if (p>0)
                {
                    while ((a.exp<x.maxexp)&&(p!=0))
                    {
                        b = p * 10;
                        p = b % y;
                        b /= y;                        
                        a.num.Insert(0, b);
                        a.exp++;
                    }
                }
            }
            // Если во втором числе больше 1 цифры, делаем его длинным и считаем частное длинных чисел.
            else
            {
                string s = Convert.ToString(y);
                x = new LongNumber(s);
                a.EqualTo(x1 / x);
            }
            // Удаляем лишние нули и возвращаем.
            a.RemoveZeros();
            return a;
        }

        public static LongNumber operator / (LongNumber x1, LongNumber y1)
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber();
            // Сохраняем входные переменные, т.к., возможно, будем их позже менять.
            LongNumber x = new LongNumber();
            x.EqualTo(x1);
            LongNumber y = new LongNumber();
            y.EqualTo(y1);
            // Запоминаем длины чисел.
            int xn = x.num.Count, yn = y.num.Count;
            // Переменная для переноса.
            int p = 0;
            // Если у второго числа есть дробная часть.
            if (y.exp > 0)
            {
                // Если у первого она не меньше, двигаем оба, пока второе не станет целым.
                if (x.exp >= y.exp)
                {
                    x.exp -= y.exp;
                    y.exp = 0;
                }
                // Если у первого она меньше, добавляем нули и двигаем оба.
                else
                {
                    for (int i = 0; i < y.exp - x.exp; i++)
                    {
                        x.num.Insert(0, 0);
                        xn++;
                    }
                    y.exp = 0;
                    x.exp = 0;
                }
            }
            // Разность длин чисел.
            int c = xn - yn;
            // Двигаем левое число до конца правого, если оно короче, с запоминанием исходной позиции.
            for (int i=0;i<c-1;i++)
            {
                y.num.Insert(0, 0);
            }
            // 
            p = x.num[xn - 1];
            // Переменная для сохранения предыдущего результата.
            LongNumber q = new LongNumber();
            // Переменная для проверки длины чисел.
            LongNumber w = new LongNumber();
            // Флаг для проверки.
            bool b = true;
            // Переменная для ранения текущей позиции запятой.
            int e = 0;
            // Переменная для сохранения точности.
            int e1 = x.maxexp;
            // Пока не дошли до удвоенной точности.
            while ((b)&&(e<=x.maxexp*2))
            {
                // Пока первое число не меньше второго, вычитаем из него второе.
                while (!(x<y))
                {
                    x.EqualTo(x - y);
                    a.PlusOne();
                }
                // Если первое число превратилось в ноль.
                if ((x.num.Count == 1) && (x.num[0] == 0))
                {
                    // Если второе число всё ещё сдвинуто, добавляем нужное число нулей и выходим из цикла.
                    if (y.num.Count>yn)
                    {
                        for (int j=0;j<yn;j++)
                        {
                            a.num.Insert(0, 0);
                        }
                    }
                    break;
                }
                // Если первое не ноль и второе сдвинуто, двигаем второе на один разряд обратно.
                if (y.num.Count>yn)
                {
                    y.num.RemoveAt(0);
                }
                // Если первое не ноль и второе не сдвинуто.
                else
                {
                    // Увеличиваем позицию запятой в ответе.
                    e++;
                    // Если не дошли до указанной точности, добавляем ноль в первое число.
                    if (e <= e1)
                    {
                        x.num.Insert(0, 0);
                    }
                    // Если дошли.
                    else
                    {
                        // Сравниваем значения текущего и предыдущего шагов.
                        w.EqualTo(a - q);
                        // Идём с позиции запятой до указанной точности.
                        int j = x.num.Count;
                        while (j >= e1)
                        {
                            // Если есть не ноль в разности, выходим из цикла.
                            if (w.num[j-1] != 0)
                            {
                                break;
                            }
                            j--;
                        }
                        // Если дошли до указанной точности,
                        // то бишь, в разности текущего и предыдущего все нули,
                        // то бишь, разряды текущего шага до указанной точности не изменились,
                        // выходим из цикла.
                        if (j <= e1)
                        {
                            break;
                        }
                    }
                }
                // Сохраняем значение текущего шага.
                q.EqualTo(a);
                // Добавляем разряд в ответ.
                a.num.Insert(0, 0);
            }
            // Записываем экспоненту в ответ.
            a.exp = e+x.exp;
            // Записываем знак в ответ и возвращаем.
            a.sign = x.sign * y.sign;
            return a;
        }

        public static LongNumber UpToPow(LongNumber x1,LongNumber y1) // Бинарное возведение в степень.
        {
            // Переменная для ответа.
            LongNumber a = new LongNumber("1");
            // "Длинная" единица, для удобства.
            LongNumber b = new LongNumber("1");
            // Сохраняем входные данные, т.к., возможно, будем их менять.
            LongNumber x = new LongNumber();
            x.EqualTo(x1);
            LongNumber y = new LongNumber();
            y.EqualTo(y1);
            a.EqualTo(x);
            // Если степень целая, считаем.
            if (y.exp==0)
            {
                if ((y.num.Count == 1) && (y.num[0] == 0))
                {
                    return b;
                }
                if (y.num[0] % 2 == 1)
                {
                    y.EqualTo(y - b);
                    y.EqualTo(UpToPow(x, y));
                    y.EqualTo(y * x);
                    return y;
                }
                else
                {
                    y.EqualTo(y / 2);
                    a.EqualTo(UpToPow(x,y));
                    a.EqualTo(a * a);
                    return a;
                }
            }
            // Удаляем лишние нули и возвращаем.
            a.RemoveZeros();
            return a;
        }

        public static LongNumber UpToPow(LongNumber x, int y)
        {
            LongNumber a = new LongNumber(Convert.ToString(y));
            return UpToPow(x, a);
        }

        public static LongNumber Min(LongNumber x, LongNumber y) // Метод нахождения минимального.
        {
            // Если первое больше, возвращаем второе, инача - первое.
            if (x>y)
                return y;
            else
                return x;
        }

        public void EqualTo(LongNumber x) // Метод присваивания.
        {
            // Присваиваем экспоненту и знак.
            this.exp = x.exp;
            this.sign = x.sign;
            this.maxexp = x.maxexp;
            // Удаляем всё в исходном числе.
            this.num.RemoveRange(0, this.num.Count);
            // Записываем мантиссу входного числа в исходное.
            for (int i=0;i<x.num.Count;i++)
            {
                this.num.Add(x.num[i]);
            }
            // Удаляем лишние нули.
            this.RemoveZeros();
        }
    }
}