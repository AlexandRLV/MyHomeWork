using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    class Course
    {
        public readonly int Code;
        public readonly string Title;
        public readonly bool IsSpecial;
        public readonly int LectureHours;
        public readonly int PractureHours;
        public readonly bool HasExam;
        public readonly bool HasCoursePaper;
        public readonly double Credits;
        public readonly List<int> Prerequisities = new List<int>();

        public Course(string s)
        {
            string[] s1 = s.Split(' ');
            this.Code = Convert.ToInt32(s1[1]);
            this.Title = s1[2];
            this.IsSpecial = Convert.ToBoolean(s1[3]);
            this.LectureHours = Convert.ToInt32(s1[4]);
            this.PractureHours = Convert.ToInt32(s1[5]);
            this.HasExam = Convert.ToBoolean(s1[6]);
            this.HasCoursePaper = Convert.ToBoolean(s1[7]);
            this.Credits = (LectureHours + 1.25 * PractureHours) / 18.0;
            this.Credits = Math.Round(Credits, 2);
            if (s1.Length>8)
            {
                for (int i=8;i<s1.Length;i++)
                {
                    this.Prerequisities.Add(Convert.ToInt32(s1[i]));
                }
            }
        }

        public bool HasPrerequisities
        {
            get
            {
                return this.Prerequisities.Count > 0;
            }
        }

        public int GetCode
        {
            get { return this.Code; }
        }

        public string GetCourse()
        {
            string s;
            s = this.Code.ToString();
            s += " " + this.Title;
            s += " " + this.IsSpecial.ToString();
            s += " " + this.LectureHours.ToString();
            s += " " + this.PractureHours.ToString();
            s += " " + this.HasExam.ToString();
            s += " " + this.HasCoursePaper.ToString();
            s += " " + this.Credits.ToString();
            for (int i=0;i<this.Prerequisities.Count;i++)
            {
                s += " " + this.Prerequisities[i].ToString();
            }
            return s;
        }
    }
}
