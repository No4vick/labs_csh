using System;

namespace lab3sh
{
    class StudentComparer : System.Collections.Generic.IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x is null || y is null)
            {
                throw new ArgumentNullException();
            }
            return x.AverageScore.CompareTo(y.AverageScore) != 0 ? x.AverageScore.CompareTo(y.AverageScore) : 0;
        }
    }
}