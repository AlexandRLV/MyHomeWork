using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestrovka
{
    class Elem
    {
        public Segment Info { get; set; }
        public Elem Next { get; set; }
        public Elem Prev { get; set; }

        public void Show()
        {
            Console.WriteLine(Info);
        }

        public override string ToString()
        {
            return Info.ToString();
        }
    }

    class MyList
    {
        public Elem first;
        public Elem First { get { return first; } }

        public Elem Last
        {
            get
            {
                Elem f = first;
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
                Elem f = first;
                while (f != null)
                {
                    f = f.Next;
                    k++;
                }
                return k;
            }
        }

        public void AddFirst(Segment s)
        {
            Elem newElem = new Elem();
            newElem.Info = s;
            newElem.Next = first;
            first = newElem;
        }

        public void AddLast(Segment s)
        {
            Elem l = Last;
            if (l == null)
            {
                AddFirst(s);
                return;
            }
            Elem newElem = new Elem();
            newElem.Info = s;
            newElem.Next = null;
            newElem.Prev = l;
            l.Next = newElem;
        }

        public void Show()
        {
            Console.WriteLine("Our students:");
            if (first == null)
            {
                Console.WriteLine("These is no students unfortunately.");
                Console.WriteLine();
                return;
            }
            Elem f = first;
            while (f != null)
            {
                Console.WriteLine(f.Info);
                f = f.Next;
            }
            Console.WriteLine();
        }

        public void InsertAfter(int k, Segment s)
        {
            if (k <= Length)
            {
                int i = 0;
                Elem f = first;
                while (i < k)
                {
                    f = f.Next;
                    i++;
                }
                Elem newElem = new Elem();
                newElem.Info = s;
                newElem.Next = f.Next;
                newElem.Prev = f;
                f.Next = newElem;
            }
            else
            {
                AddLast(s);
            }
        }

        public void InsertBefore(int k, Segment s)
        {
            if (k <= Length)
            {
                if (k == 0)
                {
                    AddFirst(s);
                    return;
                }
                int i = 0;
                Elem f = first;
                while (i < k - 1)
                {
                    f = f.Next;
                    i++;
                }
                Elem newElem = new Elem();
                newElem.Info = s;
                newElem.Next = f.Next;
                newElem.Prev = f;
                f.Next = newElem;
            }
            else
            {
                AddLast(s);
            }
        }

        public void Reverse()
        {
            Elem l = first;
            while (l.Next != null)
            {
                Elem a = l.Next;
                l.Next = l.Prev;
                l.Prev = a;
                l = l.Prev;
            }
            Elem b = l.Next;
            l.Next = l.Prev;
            l.Prev = b;
            first = l;
        }

        public void Delete(int k)
        {
            if (k <= Length)
            {
                if (k == 0)
                {
                    if (Length > 0)
                    {
                        first.Next.Prev = null;
                        first = first.Next;
                    }
                }
                else
                {
                    Elem f = first.Next;
                    Elem f1 = first;
                    int i = 0;
                    while (i < k - 1)
                    {
                        f1 = f;
                        f = f.Next;
                        i++;
                    }
                    f.Next.Prev = f1;
                    f1.Next = f.Next;
                }
            }
        }

        public void DeleteAll()
        {
            first = null;
        }

        private Elem Get(int k)
        {
            if (k < Length)
            {
                Elem f = first;
                int i = 0;
                while (i < k)
                {
                    f = f.Next;
                    i++;
                }
                return f;
            }
            else
            {
                throw new System.IndexOutOfRangeException();
            }
        }

        public Elem this[int index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                Elem k = Get(index);
                k.Next.Prev = value;
                value.Next = k.Next;
                value.Prev = k.Prev;
                k.Prev.Next = value;
            }
        }
    }
}
