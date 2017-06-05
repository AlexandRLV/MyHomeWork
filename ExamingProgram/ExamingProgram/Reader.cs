using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExamingProgram
{
    interface IReader
    {
        List<string> ReadShops { get; }
        List<string> ReadPrices { get; }
        List<string> ReadGoods { get; }
        string PathShops { get; set; }
        string PathPrices { get; set; }
        string PathGoods { get; set; }
    }

    class Reader:IReader
    {
        public string PathShops { get; set; }
        public string PathPrices { get; set; }
        public string PathGoods { get; set; }

        public List<string> ReadShops
        {
            get
            {
                List<string> res = new List<string>();
                using (StreamReader sr = new StreamReader(PathShops))
                {
                    while (!sr.EndOfStream)
                    {
                        res.Add(sr.ReadLine());
                    }
                }
                return res;
            }
        }

        public List<string> ReadPrices
        {
            get
            {
                List<string> res = new List<string>();
                using (StreamReader sr = new StreamReader(PathPrices))
                {
                    while (!sr.EndOfStream)
                    {
                        res.Add(sr.ReadLine());
                    }
                }
                return res;
            }
        }

        public List<string> ReadGoods
        {
            get
            {
                List<string> res = new List<string>();
                using (StreamReader sr = new StreamReader(PathGoods))
                {
                    while (!sr.EndOfStream)
                    {
                        res.Add(sr.ReadLine());
                    }
                }
                return res;
            }
        }
    }
}
