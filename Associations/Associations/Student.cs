using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    class Student
    {
        private int ApplicationNumber;
        private DateTime BirthDate = new DateTime();
        private string FullName;

        public Student(int n, DateTime d, string s)
        {
            ApplicationNumber = n;
            BirthDate = d;
            FullName = s;
        }

        public void WriteStudent()
        {
            Console.WriteLine("ApplicationNumber: {0}", ApplicationNumber);
            Console.WriteLine("Student's birthdate: {0}", BirthDate.ToString());
            Console.WriteLine("Student's name: " + FullName);
        }
    }
}
