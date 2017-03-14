using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    class Application
    {
        Student student;
        Curriculum program;
        public readonly List<Course> chosencourses = new List<Course>();
        private Degree chosendegree;
        
        public void CreateNewStatement()
        {
            Statements s1 = new Statements();
            int n;
            string s;
            DateTime d;
            Console.WriteLine("Enter number of application:");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter date of student's birthday:");
            d = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter student's name:");
            s = Console.ReadLine();
            this.student = new Student(n, d, s);
            Console.WriteLine("Enter registration code:");
            int c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter creation date:");
            d = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("This is our degrees. You can choose one.");
            Console.WriteLine("Code:  Title:  Credits required:  Special courses required:");
            s1.WriteDegrees();
            Console.WriteLine("Enter number of degree:");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("You chosed {0} degree.");
            this.chosendegree = s1.GetDegree(n);
            ChooseCourses(s1);
            program = new Curriculum(c, d, DateTime.Now);
        }

        public void WriteCources()
        {
            for (int i = 0; i < chosencourses.Count; i++)
            {
                Console.WriteLine("{0}: " + chosencourses[i].GetCourse(), i+1);
            }
        }

        public void ChooseCourses(Statements s1)
        {
            double sum;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("This is courses you chosed:");
                WriteCources();
                int k;
                sum = 0;
                for (int i = 0; i < chosencourses.Count; i++)
                {
                    sum += chosencourses[i].Credits;
                }
                if (chosendegree.CreditsRequired <= sum)
                {
                    sum = 0;
                    for (int i = 0; i < chosencourses.Count; i++)
                    {
                        if (chosencourses[i].IsSpecial)
                        {
                            sum++;
                        }
                    }
                    if (chosendegree.SpecialCoursesRequired <= sum)
                    {
                        Console.WriteLine("Your program is suitable.");
                        Console.WriteLine("Press escape to delete course, space to add course, enter to continue.");
                        ConsoleKeyInfo key = Console.ReadKey();
                        while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Spacebar && key.Key != ConsoleKey.Enter)
                        {
                            key = Console.ReadKey();
                        }
                        if (key.Key == ConsoleKey.Escape)
                        {
                            WriteCources();
                            Console.WriteLine("Choose the one:");
                            k = Convert.ToInt32(Console.ReadLine());
                            chosencourses.RemoveAt(k - 1);
                        }
                        else if (key.Key == ConsoleKey.Spacebar)
                        {
                            Console.WriteLine("This is our courses. You can choose some of them.");
                            Console.WriteLine("Code:  Title:  Is special:  Lecture hours:  Practise hours:  \r\nHas exam:  Has course paper:  Credits:  \r\nPrerequisities:  ");
                            s1.WriteCources();
                            k = Convert.ToInt32(Console.ReadLine());
                            chosencourses.Add(s1.GetCourse(k));
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is not enough special courses in your program.");
                        Console.WriteLine("Press escape to delete course, space to add course.");
                        ConsoleKeyInfo key = Console.ReadKey();
                        while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Spacebar)
                        {
                            key = Console.ReadKey();
                        }
                        if (key.Key == ConsoleKey.Escape)
                        {
                            WriteCources();
                            Console.WriteLine("Choose the one:");
                            k = Convert.ToInt32(Console.ReadLine());
                            chosencourses.RemoveAt(k - 1);
                        }
                        else
                        {
                            Console.WriteLine("This is our courses. You can choose some of them.");
                            Console.WriteLine("Code:  Title:  Is special:  Lecture hours:  Practise hours:  \r\nHas exam:  Has course paper:  Credits:  \r\nPrerequisities:  ");
                            s1.WriteCources();
                            k = Convert.ToInt32(Console.ReadLine());
                            chosencourses.Add(s1.GetCourse(k));
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There is not enougn course credits in your program.");
                    Console.WriteLine("Press escape to delete course, space to add course.");
                    ConsoleKeyInfo key = Console.ReadKey();
                    while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Spacebar)
                    {
                        key = Console.ReadKey();
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        WriteCources();
                        Console.WriteLine("Choose the one:");
                        k = Convert.ToInt32(Console.ReadLine());
                        chosencourses.RemoveAt(k - 1);
                    }
                    else
                    {
                        Console.WriteLine("This is our courses. You can choose some of them.");
                        Console.WriteLine("Code:  Title:  Is special:  Lecture hours:  Practise hours:  \r\nHas exam:  Has course paper:  Credits:  \r\nPrerequisities:  ");
                        s1.WriteCources();
                        k = Convert.ToInt32(Console.ReadLine());
                        chosencourses.Add(s1.GetCourse(k));
                    }
                }
            }
        }

        public void WriteApplication()
        {
            Console.WriteLine("Student:");
            student.WriteStudent();
            Console.WriteLine("Curriculum:");
            program.WriteProgram();
            Console.WriteLine("Degree:");
            chosendegree.WriteDegree();
            Console.WriteLine("Courses:");
            for (int i=0;i<chosencourses.Count;i++)
            {
                Console.WriteLine(chosencourses[i].GetCourse());
            }
        }
    }
}
