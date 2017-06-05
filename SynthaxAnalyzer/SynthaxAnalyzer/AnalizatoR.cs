using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynthaxAnalyzer
{
    class BaseString
    {
        public string s;
        public int Counter { get; private set; }

        public char GetCurrent()
        {
            if (Counter == -1) return '|';
            return s[Counter];
        }

        public void Next()
        {
            if (Counter < s.Length - 1)
            {
                Counter++;
                if (s[Counter] == ' ') Next();
            }
            else
            {
                Counter = -1;
            }
        }

        public void Prev()
        {
            if (Counter == -1) Counter = s.Length - 1;
            else
                Counter--;
        }

        public BaseString(string s)
        {
            this.s = s;
            Counter = 0;
        }
    }

    class AnalizatoR
    {
        BaseString s;

        public double Number()
        {
            string number = "";
            while (char.IsDigit(s.GetCurrent())||(s.GetCurrent()==','))
            {
                number += s.GetCurrent().ToString();
                s.Next();
            }
            return double.Parse(number);
        }

        public double Brackets()
        {
            if (char.IsDigit(s.GetCurrent()))
                return Number();
            else
            {
                s.Next();
                double result = Add();
                s.Next();
                return result;
            }
        }

        public double Mult()
        {
            double first = Brackets();
            if (s.GetCurrent() == '*')
            {
                s.Next();
                double second = Mult();
                return first * second;
            }
            else if (s.GetCurrent() == '/')
            {
                s.Next();
                double second = Mult();
                return first / second;
            }
            else return first;
        }

        public double Add()
        {
            double first = Mult();
            if (s.GetCurrent() == '+')
            {
                s.Next();
                double second = Add();
                return first + second;
            }
            else if (s.GetCurrent() == '-')
            {
                s.Next();
                double second = Add();
                return first - second;
            }
            else return first;
        }

        public double Result()
        {
            return Add();
        }

        public AnalizatoR(string s)
        {
            this.s = new BaseString(s);
        }
    }
}
