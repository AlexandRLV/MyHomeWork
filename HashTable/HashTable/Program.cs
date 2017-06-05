using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace HashTable
{
    

    public class HashTree
    {
        private string Hash(string str)// Хеш-функция ly
        {
            uint hash = 0;

            for (int i = 0; i < str.Length; i++)
            {
                hash = (hash + 1664525) + (byte)(str[i]) + 1013904223;
            }

            return hash.ToString();
        }            

        private string LeafHash(string str)  // Нахождение хеша для листьев
        {
            return "00h"+Hash(str);
        }

        private string InternalHash(string str1, string str2) // Нахождение хеша для узлов
        {
            return "01h"+Hash(str1)+Hash(str2);
        }

        public string GetHash(string data)// Разбиение на блоки, составление дерева и нахождение общего хеша
        {
            List<string> MemoryBlocks = new List<string>();

            int counter = 0;
            while (data.Length - counter > 512)
            {
                MemoryBlocks.Add(LeafHash(data.Substring(counter, 512)));
                counter += 512;
            }

            if (counter != data.Length)
                MemoryBlocks.Add(LeafHash(data.Substring(counter, data.Length - counter)));

            while (MemoryBlocks.Count > 1)
            {
                LevelUp(MemoryBlocks);
            }

            return MemoryBlocks[0];
        }

        private void LevelUp(List<string> MemoryBlocks)// Построение уровня в дереве
        {
            if (MemoryBlocks.Count == 1)
                return;

            int counter = 0;
            string x;
            string y;

            while(MemoryBlocks.Count-counter>1)
            {
                x = MemoryBlocks[counter];
                y = MemoryBlocks[counter + 1];

                MemoryBlocks.RemoveAt(0);
                MemoryBlocks.RemoveAt(0);

                MemoryBlocks.Add(InternalHash(x.ToString(), y.ToString()));
                counter++;
            }
        }
    }

    public class HashTable<T> 
    {
        public class Cell<T>
        {
            public List<T> Values;
            public int index;

            public override string ToString()
            {
                StringBuilder s = new StringBuilder();
                foreach(T t in Values)
                {
                    s.Append($"{t.ToString()}; ");
                }
                return s.ToString();
            }

            public Cell(int index, T value)
            {
                this.index = index;
                Values = new List<T>();
                Values.Insert(0, value);
            }
        }

        public int Hash(T value) 
        {
            return Convert.ToInt32(value) % MaxLength;
        }

        public void Add(T value)
        {
            if (Contains(value)) return;
            int index = Hash(value);
            if(table.ContainsKey(index))
            {
                table[index].Values.Add(value);
            }
            else
            {
                table.Add(index, new Cell<T>(index, value));
            }
            Count++;
        }

        public bool Contains(T value) 
        {
            if (table.ContainsKey(Hash(value)))
            {
                Cell<T> cell = table[Hash(value)];
                foreach (T val in cell.Values)
                {
                    if (value.Equals(val)) return true;
                }
            }
            return false;
        }

        public void Delete(T value)
        {
            if(Contains(value))
            {
                Cell<T> cell = table[Hash(value)];
                foreach(T val in cell.Values)
                {
                    if (value.Equals(val))
                    {
                        cell.Values.Remove(val);
                        Count--;
                        break;
                    }
                }
            }
        }

        public void Delete(int index)
        {
            if (table.ContainsKey(index)) table.Remove(index);
            Count--;
        }


        public Dictionary<int, Cell<T>> table { get; private set; }



        public int Count { get; private set; }

        public int MaxLength { get; private set; }


        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            foreach(var cell in table)
            {
                s.Append($"{cell.Key}) - {cell.Value.ToString()}\n");
            }
            return s.ToString();
        }

        public HashTable()
        {
            table = new Dictionary<int, Cell<T>>();
            Count = 0;
            MaxLength = 127;
        }
    }
	// Хеш-таблица, ее я уже скидывал

    class Program
    {
        static void Hashing(Object state)
        {
            HashTree ht = new HashTree();
            Thread.Sleep(0);
            byte[] bytes = File.ReadAllBytes(state.ToString());
            StringBuilder s = new StringBuilder();
            foreach (byte b in bytes) s.Append(b.ToString());
            Console.WriteLine($"{state} - {ht.GetHash(s.ToString())}");
        }
        
        static Queue<Thread> threads;

        static void Main(string[] args)
        {
            //string s;
            //HashTable<int> h = new HashTable<int>();
            //while (true)
            //{
            //    s = Console.ReadLine();
            //    if (s == "") break;
            //    else h.Add(Convert.ToInt32(s));
            //}

            //while (true)
            //{
            //    s = Console.ReadLine();
            //    if (s == "") break;
            //    else h.Delete(Convert.ToInt32(s));
            //}
            //Console.WriteLine(h);



            string filename;
            while(true)
            {
                filename = Console.ReadLine();//Если файл с введенным именем существует, вычисляется хеш
                if (File.Exists(filename))
                {
                    ThreadPool.QueueUserWorkItem(Hashing, filename);
                }
                else Console.WriteLine("File doesn't exist");
            }
        }
    }
}
