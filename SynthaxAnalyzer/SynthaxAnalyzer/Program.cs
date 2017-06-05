using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SynthaxAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Analyzer r = new Analyzer();
            while (true)
            {
                //AnalizatoR a = new AnalizatoR(Console.ReadLine());
                //Console.WriteLine($"{a.Result()}");
                r.Calculate(Console.ReadLine());
                r.WriteVariables();
                Console.WriteLine();
            }
        }
    }
}
