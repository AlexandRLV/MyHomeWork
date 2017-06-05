using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExamingProgram
{
    interface IController
    {
        IReader reader { get; set; }
        ILogics logics { get; set; }
        List<String> ProcessedData { get; }
        void WriteData(string pathCheap, string pathMed, string pathExp);
    }

    interface ILogics
    {
        List<string> WorkWithData(IReader r);
    }

    class Controller:IController
    {
        public IReader reader { get; set; }
        public ILogics logics { get; set; }
        public List<string> ProcessedData
        {
            get
            {
                return logics.WorkWithData(reader);
            }
        }

        public void WriteData(string pathCheap, string pathMed, string pathExp)
        {
            var result = ProcessedData.Select(a => new { Info = a, Price = int.Parse(a.Split(';')[1]) }).ToList();
            int i = 0;
            using (StreamWriter sw = new StreamWriter(pathCheap))
            {
                while ((i < result.Count)&&(result[i].Price < 1000))
                {
                    sw.WriteLine(result[i].Info);
                    i++;
                }
            }
            using (StreamWriter sw = new StreamWriter(pathMed))
            {
                while ((i < result.Count) && (result[i].Price < 10000))
                {
                    sw.WriteLine(result[i].Info);
                    i++;
                }
            }
            using (StreamWriter sw = new StreamWriter(pathExp))
            {
                while (i<result.Count)
                {
                    sw.WriteLine(result[i].Info);
                    i++;
                }
            }
        }
    }

    class Logics : ILogics
    {
        public List<string> WorkWithData(IReader r)
        {
            List<string> result = new List<string>();
            List<string> shops = r.ReadShops;
            List<string> prices = r.ReadPrices;
            List<string> goods = r.ReadGoods;
            var shop = shops.Select(a => new { Code = int.Parse(a.Split(';')[0]), Name = a.Split(';')[1], Adress = a.Split(';')[2] });
            var good = goods.Select(a => new { Code = int.Parse(a.Split(';')[0]), Name = a.Split(';')[1], Category = a.Split(';')[2] });
            var res = prices.Select(a => new
            {   Shop = int.Parse(a.Split(';')[0]),
                Product = int.Parse(a.Split(';')[1]),
                Price = int.Parse(a.Split(';')[2]),
                Discount = int.Parse(a.Split(';')[3])
            }).Select(a => new
            {   Shop = a.Shop,
                Product = a.Product,
                Price = a.Price * (a.Discount) / 100
            }).GroupBy(a => a.Product).Select(a => a.Where(b => b.Price == a.Min(n => n.Price)).ElementAt(0)).Select(a => new
            {   ProductName = good.Where(b => b.Code == a.Product).ElementAt(0).Name,
                Price = a.Price,
                ShopName = shop.Where(b => b.Code == a.Shop).ElementAt(0).Name,
                ShopAdress = shop.Where(b => b.Code == a.Shop).ElementAt(0).Adress
            }).OrderBy(a=>a.Price).Select(a => $"{a.ProductName};{a.Price};{a.ShopAdress};{a.ShopName}");
            result = res.ToList();
            return result;
        }
    }
}
