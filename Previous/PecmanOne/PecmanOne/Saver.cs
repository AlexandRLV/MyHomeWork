using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PecmanOne
{
    class Saver
    {
        private List<string> names = new List<string>();
        private List<int> records = new List<int>();

        public void ReadRecords()
        {
            StreamReader file = new StreamReader(@"Records.txt");
            string s;
            names.RemoveRange(0,names.Count);
            records.RemoveRange(0, records.Count);
            while (!file.EndOfStream)
            {
                s = file.ReadLine();
                names.Add(s.Split(' ')[0]);
                records.Add(Convert.ToInt32(s.Split(' ')[1]));
            }
            file.Close();
        }

        public void AddNewRecord(string name, int record)
        {
            ReadRecords();
            int i = 0;
            while (this.records[i]>record)
            {
                i++;
            }
            if (i<this.records.Count)
            {
                if (this.records.Count < 10)
                {
                    this.records.Add(this.records[this.records.Count - 1]);
                    this.names.Add(this.names[this.names.Count - 1]);
                    for (int j = this.records.Count - 2; j > i; j--)
                    {
                        this.records[j] = this.records[j - 1];
                        this.names[j] = this.names[j - 1];
                    }
                }
                else
                {
                    for (int j = this.records.Count - 1; j > i; j--)
                    {
                        this.records[j] = this.records[j - 1];
                        this.names[j] = this.names[j - 1];
                    }
                }
                this.records[i] = record;
                this.names[i] = name;
            }
            else
            {
                if (this.records.Count<10)
                {
                    this.records.Add(record);
                    this.names.Add(name);
                }
            }
            StreamWriter file = new StreamWriter(@"Records.txt");
            for (i=0;i<this.records.Count;i++)
            {
                file.WriteLine(this.names[i] + ' ' + this.records[i]);
            }
            file.Close();
        }

        public void WriteRecords()
        {
            ReadRecords();
            for (int i = 0; i < names.Count; i++)
            {
                if (this.records[i]>0)
                {
                    Console.WriteLine("{0}: " + names[i] + "  {1}", i+1, records[i]);
                }                
            }
        }
    }
}
