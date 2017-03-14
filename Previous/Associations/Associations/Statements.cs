using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    class Statements
    {
        public readonly List<Degree> degrees = new List<Degree>();
        public readonly List<Course> cources = new List<Course>();

        public void ReadCources()
        {
            string[] files = Directory.GetFiles("Courses/");
            cources.RemoveRange(0, cources.Count);
            string s;
            StreamReader file;
            for (int i = 0; i < files.Length; i++)
            {
                file = new StreamReader(files[i]);
                s = "";
                while (!file.EndOfStream)
                {
                    s += " " + file.ReadLine();
                }
                cources.Add(new Course(s));
                file.Close();
            }
        }

        public void AddNewCourse()
        {
            string s, f;
            Console.WriteLine("Enter code:");
            f = Console.ReadLine();
            string path = "Courses/" + f + ".txt";
            StreamWriter file = new StreamWriter(path);
            file.WriteLine(f);
            s = " " + f;
            Console.WriteLine("Enter title:");
            f = Console.ReadLine();
            s += " " + f;
            file.WriteLine(f);
            Console.WriteLine("Enter 1 if it's special course, if not, enter 0:");
            string s1 = Console.ReadLine();
            if (s1 == "1")
            {
                f = "true";
                file.WriteLine(f);
                s += " true";
            }
            else
            {
                f = "false";
                file.WriteLine(f);
                s += " false";
            }
            Console.WriteLine("Enter lecture hours:");
            f = Console.ReadLine();
            s += " " + f;
            file.WriteLine(f);
            Console.WriteLine("Enter practure hours:");
            f = Console.ReadLine();
            s += " " + f;
            file.WriteLine(f);
            Console.WriteLine("Enter 1 if it has exam, if not, enter 0:");
            s1 = Console.ReadLine();
            if (s1 == "1")
            {
                f = "true";
                file.WriteLine(f);
                s += " true";
            }
            else
            {
                f = "false";
                file.WriteLine(f);
                s += " false";
            }
            Console.WriteLine("Enter 1 if it has course paper, if not, enter 0:");
            s1 = Console.ReadLine();
            if (s1 == "1")
            {
                f = "true";
                file.WriteLine(f);
                s += " true";
            }
            else
            {
                f = "false";
                file.WriteLine(f);
                s += " false";
            }
            Console.WriteLine("Enter 1 if it has prerequisities, if not, enter 0:");
            s1 = Console.ReadLine();
            if (s1 == "1")
            {
                Console.WriteLine("Enter number of prerequisities:");
                int s2 = Convert.ToInt32(Console.ReadLine());
                for (int i=0;i<s2;i++)
                {
                    Console.WriteLine("Enter code of course:");
                    f = Console.ReadLine();
                    s += " " + f;
                    file.WriteLine(f);
                }
            }
            cources.Add(new Course(s));
            file.Close();
        }

        public void WriteCources()
        {
            ReadCources();
            Console.WriteLine("Code:  Title:  Is special:  Lecture hours:  Practise hours:  \r\nHas exam:  Has course paper:  Credits:  \r\nPrerequisities:  ");
            for (int i=0;i<cources.Count;i++)
            {
                Console.WriteLine("{0}: " + cources[i].GetCourse(),i+1);
            }
        }

        public void DeleteCource(int code)
        {
            string path = "Cources/" + code.ToString() + ".txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            int i = 0;
            while (i < this.cources.Count && this.cources[i].Code != code)
            {
                i++;
            }
            this.cources.RemoveAt(i);
        }

        public Course GetCourse(int n)
        {
            return this.cources[n - 1];
        }

        public void ReadDegrees()
        {
            string[] files = Directory.GetFiles("Degrees/");
            string s;
            degrees.RemoveRange(0, degrees.Count);
            StreamReader file;
            for (int i = 0; i < files.Length; i++)
            {
                file = new StreamReader(files[i]);
                s = "";
                while (!file.EndOfStream)
                {
                    s += " " + file.ReadLine();
                }
                degrees.Add(new Degree(s));
                file.Close();
            }
        }

        public void AddNewDegree()
        {
            string s, f;
            Console.WriteLine("Enter code:");
            f = Console.ReadLine();
            string path = "Degrees/" + f + ".txt";
            StreamWriter file = new StreamWriter(path);
            file.WriteLine(f);
            s = " " + f;
            Console.WriteLine("Enter title:");
            f = Console.ReadLine();
            s += " " + f;
            file.WriteLine(f);
            Console.WriteLine("Enter how many credits required:");
            f = Console.ReadLine();
            s += " " + f;
            file.WriteLine(f);
            Console.WriteLine("Enter how many special cources required:");
            f = Console.ReadLine();
            s += " " + f;
            file.WriteLine(f);
            degrees.Add(new Degree(s));
            file.Close();
        }

        public void WriteDegrees()
        {
            ReadDegrees();
            Console.WriteLine("Code:  Title:  Credits required:  Special courses required:");
            for (int i = 0; i < degrees.Count; i++)
            {
                Console.WriteLine("{0}: " + degrees[i].GetDegree(), i+1);
            }
        }

        public void DeleteDegree(int code)
        {
            string path = "Degrees/" + code.ToString() + ".txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            int i = 0;
            while (i < this.degrees.Count && this.degrees[i].Code != code)
            {
                i++;
            }
            this.degrees.RemoveAt(i);
        }

        public Degree GetDegree(int n)
        {
            return this.degrees[n - 1];
        }
    }
}
