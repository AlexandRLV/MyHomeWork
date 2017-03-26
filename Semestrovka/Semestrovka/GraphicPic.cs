using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Semestrovka
{
    class Segment
    {
        public double x1, y1, x2, y2;

        public double Length
        {
            get
            {
                return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            }
        }

        public Segment(int x1,int y1,int x2,int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }

        public override string ToString()
        {
            return $"({x1};{y1}):({x2};{y2}) - {Length}";
        }

        public bool Compare(Segment s)
        {
            return (this.x1 == s.x1) && (this.y1 == s.y1) && (this.x2 == s.x2) && (this.y2 == s.y2);
        }
    }

    class GraphicPic
    {
        public MyList segments;

        public GraphicPic()
        {
            segments = new MyList();
        }

        public GraphicPic(string path)
        {
            segments = new MyList();
            using (StreamReader file = new StreamReader(path))
            {
                string[] s;
                while (!file.EndOfStream)
                {
                    s = file.ReadLine().Split(' ');
                    segments.AddLast(new Segment(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));
                }
            }
        }

        public void ReadFromFile(string path)
        {
            using (StreamReader file = new StreamReader(path))
            {
                string[] s;
                while (!file.EndOfStream)
                {
                    s = file.ReadLine().Split(' ');
                    segments.AddLast(new Segment(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3])));
                }
            }
        }

        public void Show()
        {
            for (int i = 0; i < segments.Length; i++)
            {
                Console.WriteLine(segments[i]);
            }
            Console.WriteLine();
        }

        public void Insert(Segment s)
        {
            bool b = true;
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i].Info.Compare(s))
                {
                    b = false;
                }
            }
            if (b)
            {
                segments.AddLast(s);
            }
        }

        private bool OnAngles(Segment s)
        {
            return ((Math.Abs(s.x2 - s.x1) == Math.Abs(s.y2 - s.y1)) || (s.Length / Math.Abs(s.y2 - s.y1) == 2));
        }

        public GraphicPic AngleList()
        {
            GraphicPic s = new GraphicPic();
            for (int i = 0; i < segments.Length; i++)
            {
                if (OnAngles(segments[i].Info))
                {
                    s.segments.AddLast(segments[i].Info);
                }
            }
            return s;
        }

        public GraphicPic LengthList(int a, int b)
        {
            GraphicPic s = new GraphicPic();
            for (int i = 0; i < segments.Length; i++)
            {
                if ((segments[i].Info.Length >= a) && (segments[i].Info.Length <= b))
                {
                    s.segments.AddLast(segments[i].Info);
                }
            }
            return s;
        }

        public void Sort()
        {
            for (int i = 0; i < segments.Length; i++)
            {
                for (int j = 1; j < segments.Length - i - 1; j++)
                {
                    if (segments[j].Info.Length > segments[j + 1].Info.Length)
                    {
                        Elem s = segments[j];
                        segments[j] = segments[j + 1];
                        segments[j + 1] = s;
                    }
                }
            }
        }
    }
}
