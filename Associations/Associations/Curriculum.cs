using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Associations
{
    class Curriculum
    {
        private int Code;
        private DateTime CreationDate = new DateTime();
        private DateTime ConfirmationDate = new DateTime();

        public Curriculum(int n, DateTime d, DateTime m)
        {
            Code = n;
            CreationDate = d;
            ConfirmationDate = m;
        }

        public void WriteProgram()
        {
            Console.WriteLine("Code: {0}", Code);
            Console.WriteLine("Creation date: {0}", CreationDate.ToString());
            Console.WriteLine("Confirmation date: {0}", ConfirmationDate.ToString());
        }
    }
}
