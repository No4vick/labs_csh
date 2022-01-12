using System;
using System.Collections.Generic;

namespace lab3sh
{
    public class ExamComparer : IComparer<Exam>
    {
        public int Compare(Exam x, Exam y)
        {
            if (x is null || y is null)
            {
                throw new ArgumentNullException();
            }
            
            if (x.Date > y.Date)
            {
                return 1;
            }

            if (x.Date < y.Date)
            {
                return -1;
            }

            return 0;
        }
    }
}