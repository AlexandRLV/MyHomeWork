using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    class Degree
    {
        public readonly int Code;
        public readonly string Title;
        public readonly int CreditsRequired;
        public readonly int SpecialCoursesRequired;

        public Degree(string s)
        {
            string[] s1 = s.Split(' ');
            this.Code = Convert.ToInt32(s1[1]);
            this.Title = s1[2];
            this.CreditsRequired = Convert.ToInt32(s1[3]);
            this.SpecialCoursesRequired = Convert.ToInt32(s1[4]);
        }
        
        public string GetDegree()
        {
            string s;
            s = this.Code.ToString();
            s += " " + this.Title;
            s += " " + this.CreditsRequired.ToString();
            s += " " + this.SpecialCoursesRequired.ToString();
            return s;
        }

        public void WriteDegree()
        {
            Console.WriteLine("Code: {0}", Code);
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Credits required: {0}", CreditsRequired.ToString());
            Console.WriteLine("Special courses required: {0}", SpecialCoursesRequired.ToString());
        }
    }
}
