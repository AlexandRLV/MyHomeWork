using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task7
{
    class Element
    {
        public int Coef { get; set; }
        public int Deg1 { get; set; }
        public int Deg2 { get; set; }
        public int Deg3 { get; set; }

        public static bool operator > (Element e1, Element e2)
        {
            if (e1.Deg1 > e2.Deg1)
                return true;
            else if (e2.Deg1 > e1.Deg1)
                return false;
            else if (e1.Deg2 > e2.Deg2)
                return true;
            else if (e2.Deg2 > e1.Deg2)
                return false;
            else if (e1.Deg3 > e2.Deg3)
                return true;
            else if (e2.Deg3 > e1.Deg3)
                return false;
            else if (e1.Coef > e2.Coef)
                return true;
            else
                return false;
        }

        public static bool operator < (Element e1, Element e2)
        {
            if (e1.Deg1 > e2.Deg1)
                return false;
            else if (e2.Deg1 > e1.Deg1)
                return true;
            else if (e1.Deg2 > e2.Deg2)
                return false;
            else if (e2.Deg2 > e1.Deg2)
                return true;
            else if (e1.Deg3 > e2.Deg3)
                return false;
            else if (e2.Deg3 > e1.Deg3)
                return true;
            else if (e1.Coef > e2.Coef)
                return false;
            else if (e1.Coef < e2.Coef)
                return true;
            else
                return false;
        }

        public static bool operator == (Element e1, Element e2)
        {
            return (e1.Coef == e2.Coef) && (e1.Deg1 == e2.Deg1) && (e1.Deg2 == e2.Deg2) && (e1.Deg3 == e2.Deg3);
        }

        public bool CompareDegrees(Element e)
        {
            return (this.Deg1 == e.Deg1) && (this.Deg2 == e.Deg2) && (this.Deg3 == e.Deg3);
        }

        public static bool operator != (Element e1, Element e2)
        {
            return (e1.Coef != e2.Coef) || (e1.Deg1 != e2.Deg1) || (e1.Deg2 != e2.Deg2) || (e1.Deg3 != e2.Deg3);
        }

        public void Derivate(int i)
        {
            if (i == 1)
            {
                if (Deg1 != 0)
                {
                    Coef *= Deg1;
                    Deg1--;
                }
                else
                {
                    Coef = 0;
                }
            }
            else if (i == 2)
            {
                if (Deg2 != 0)
                {
                    Coef *= Deg2;
                    Deg2--;
                }
                else
                {
                    Coef = 0;
                }
            }
            else if (i == 3)
            {
                if (Deg3 != 0)
                {
                    Coef *= Deg3;
                    Deg3--;
                }
                else
                {
                    Coef = 0;
                }
            }
        }

        public double Evaluate(int x, int y, int z)
        {
            return Coef * Math.Pow(x, Deg1) * Math.Pow(y, Deg2) * Math.Pow(z,Deg3);
        }

        public override string ToString()
        {
            if (Coef != 0)
                return $"{Coef}*x^{Deg1}*y^{Deg2}*z^{Deg3}";
            else
                return "0";
        }
    }

    class Polinom3
    {
        public MyList elements;

        public Polinom3(string filename)
        {
            elements = new MyList();
            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string[] s = sr.ReadLine().Split(' ');
                    Element e = new Element();
                    e.Coef = int.Parse(s[0]);
                    e.Deg1 = int.Parse(s[1]);
                    e.Deg2 = int.Parse(s[2]);
                    e.Deg3 = int.Parse(s[3]);
                    elements.Add(e);
                }
            }
        }

        public void Remove(int d1, int d2, int d3)
        {
            Element e = new Element();
            e.Deg1 = d1;
            e.Deg2 = d2;
            e.Deg3 = d3;
            elements.Remove(e);
        }

        public void Add(Polinom3 p)
        {
            this.elements = MyList.Merge(elements, p.elements);
        }

        public void Derivate(int i)
        {
            for (int j = 0; j < elements.Length; j++)
            {
                elements[j].Derivate(i);
            }
        }

        public void Insert(int c, int d1, int d2, int d3)
        {
            Element e = new Element();
            e.Coef = c;
            e.Deg1 = d1;
            e.Deg2 = d2;
            e.Deg3 = d3;
            elements.Add(e);
        }

        public double Evaluate(int x,int y,int z)
        {
            double res = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                res += elements[i].Evaluate(x, y, z);
            }
            return res;
        }

        public int[] MinCoef()
        {
            int[] res = new int[3];
            Element e = elements[0];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] < e)
                {
                    e = elements[i];
                }
            }
            res[0] = e.Deg1;
            res[1] = e.Deg2;
            res[3] = e.Deg3;
            return res;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < elements.Length; i++)
            {
                s += " " + elements[i];
            }
            return s;
        }
    }

    class Elem
    {
        public Element Info { get; set; }
        public Elem Next { get; set; }

        public static bool operator > (Elem e1, Elem e2)
        {
            return e1.Info > e2.Info;
        }

        public static bool operator <(Elem e1, Elem e2)
        {
            return e1.Info < e2.Info;
        }

        public override string ToString()
        {
            return Info.ToString();
        }
    }

    class MyList
    {
        public Elem First { get; set; }

        public Elem Last
        {
            get
            {
                Elem f = First;
                if (f == null)
                {
                    return null;
                }
                if (f.Next == null)
                {
                    return f;
                }
                while (f.Next != null)
                    f = f.Next;
                return f;
            }
        }

        public int Length
        {
            get
            {
                int k = 0;
                Elem f = First;
                while (f != null)
                {
                    f = f.Next;
                    k++;
                }
                return k;
            }
        }

        public void Add(Element e)
        {
            if (First == null)
            {
                Elem a = new Elem();
                a.Info = e;
                First = a;
            }
            else
            {
                Elem f = First;
                Elem prev = null;
                while ((e < f.Info) && (f != Last))
                {
                    prev = f;
                    f = f.Next;
                }
                if (e < f.Info)
                {
                    Elem a = new Elem();
                    a.Info = e;
                    f.Next = a;
                }
                else
                {
                    if (e != f.Info)
                    {
                        Elem a = new Elem();
                        a.Info = e;
                        a.Next = f;
                        if (prev == null)
                        {
                            First = a;
                        }
                        else
                        {
                            prev.Next = a;
                        }
                    }
                }
            }
        }

        public Element this[int index]
        {
            get
            {
                if (index < Length)
                {
                    Elem f = First;
                    int i = 0;
                    while (i < index)
                    {
                        f = f.Next;
                        i++;
                    }
                    return f.Info;
                }
                else
                {
                    throw new System.IndexOutOfRangeException();
                }
            }
        }

        public void Remove(Element e)
        {
            Elem f = First;
            Elem prev = null;
            while (!f.Info.CompareDegrees(e))
            {
                prev = f;
                f = f.Next;
            }
            prev.Next = f.Next;
        }

        public static MyList Merge(MyList a, MyList b)
        {
            MyList res = new MyList();
            Elem i = a.First, j = b.First;
            while (i != null && j != null)
            {
                if (i.Info > j.Info)
                {
                    res.Add(i.Info);
                    i = i.Next;
                }
                else
                {
                    res.Add(j.Info);
                    j = j.Next;
                }
            }
            if (i == null)
            {
                while (j != null)
                {
                    res.Add(j.Info);
                    j = j.Next;
                }
            }
            else
            {
                while (i != null)
                {
                    res.Add(i.Info);
                    i = i.Next;
                }
            }
            return res;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Polinom3 pol = new Polinom3("input1.txt");
            Console.WriteLine(pol);

            Polinom3 pol2 = new Polinom3("input2.txt");
            Console.WriteLine(pol2);

            pol.Add(pol2);
            Console.WriteLine(pol);

            pol.Insert(1, 2, 3, 4);
            pol.Insert(1, 2, 3, 5);
            Console.WriteLine(pol);

            pol.Remove(2, 3, 4);
            Console.WriteLine(pol);

            pol.Derivate(1);
            Console.WriteLine(pol);

            Console.WriteLine($"{pol.Evaluate(1, 1, 1)}");
        }
    }
}
