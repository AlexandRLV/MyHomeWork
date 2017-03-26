using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semestrovka
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphicPic g = new GraphicPic("input.txt");
            g.Show();
            GraphicPic a = g.AngleList();
            GraphicPic b = g.LengthList(2, 20);
            Console.WriteLine("AngleList:");
            a.Show();
            Console.WriteLine("LengthList:");
            b.Show();
            Console.WriteLine("Enter your segment: (x1, y1) (x2, y2).");
            string[] s = Console.ReadLine().Split(' ');
            g.Insert(new Segment(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));
            g.Show();
        }
    }
}
