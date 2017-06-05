using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ExamingProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            IReader reader = new Reader();
            ILogics logics = new Logics();
            IController controller = new Controller();
            reader.PathGoods = "goods.txt";
            reader.PathPrices = "prices.txt";
            reader.PathShops = "shops.txt";
            controller.reader = reader;
            controller.logics = logics;
            controller.WriteData("cheap.txt","medium.txt","expensive.txt");

        }
    }
}
