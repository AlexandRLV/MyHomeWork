using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHash
{
    class HashTable
    {
        public class Elem
        {
            public List<int> Values;

            public Elem(int value)
            {
                this.Values = new List<int>();
                this.Values.Add(value);
            }

            public override string ToString()
            {
                string s = "";
                foreach(int a in Values)
                {
                    s += a.ToString()+"; ";
                }
                return s;
            }
        }

        public Dictionary<uint, Elem> Table;
        public int Count;
        public int Maxlength;

        public uint Hash(int input)// Хеш-функция RS
        {
            uint hash = 0;
            uint a = 63689;
            uint b = 378551;
            byte[] data = BitConverter.GetBytes(input);
            for (int i = 0; i < data.Length; i++)
            {
                hash = hash * a + data[i];
                a *= b;
            }

            return hash;
        }

        public bool Contains(int value)
        {
            if (Table.ContainsKey(Hash(value)))
            {
                Elem elem = Table[Hash(value)];
                foreach (int x in elem.Values)
                {
                    if (x == value)
                        return true;
                }
            }
            return false;
        }

        public void Add(int value)
        {
            if (!Contains(value))
            {
                uint hashed = Hash(value);
                if (Table.ContainsKey(hashed))
                {
                    Table[hashed].Values.Add(value);
                }
                else
                {
                    Table.Add(hashed, new Elem(value));
                }
                Count++;
            }
        }

        public void Delete(int value)
        {
            if (Contains(value))
            {
                uint hashed = Hash(value);
                Elem elem = Table[hashed];
                foreach(int x in elem.Values)
                {
                    if (x == value)
                    {
                        elem.Values.Remove(x);
                        if (elem.Values.Count == 0)
                        {
                            Table.Remove(hashed);
                        }
                        Count--;
                        return;
                    }
                }
            }
        }

        public HashTable(int MaxCount)
        {
            this.Count = 0;
            this.Maxlength = MaxCount;
            this.Table = new Dictionary<uint, Elem>();
        }

        public override string ToString()
        {
            string s = "";
            foreach (var e in Table)
            {
                s += e.ToString();
            }
            return s;
        }
    }
}
