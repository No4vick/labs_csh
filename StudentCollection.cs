using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3sh
{
    class StudentCollection
    {
        private System.Collections.Generic.List<Student> students;

        public List<Student> ListStudents
        {
            get { return students; }
            set { students = value; }
        }
        
        public double MaxAverage
        {
            get
            {
                return students.Max(s => s.AverageScore);
            }
        }

        public IEnumerable<Student> Specialists
        {
            get
            {
                return students.Where(student => student.EducationType == Education.Specialist);
            }
        }

        public void AddDefaults()
        {
            int number = 5;
            students = new List<Student>();
            for (int i = 0; i < number; i++)
            {
                students.Add(new Student());
            }
        }

        public void AddStudents(params Student[] stds)
        {
            foreach (Student student in stds)
            {
                students.Add(student);
            }
        }

        public override string ToString()
        {
            string res = new string("");
            foreach (Student student in students)
            {
                res += $"{student.ToString()}\n";
            }
            return res;
        }

        public string ToShortString()
        {
            string res = new string("");
            foreach (Student student in students)
            {
                res += $"{student.ToShortString()}\n";
            }
            return res;
        }

        public void sortBySurname()
        {
            students.Sort((student, student1) => String.Compare(student.Surname, student1.Surname));
        }

        public void sortByBday()
        {
            students.Sort(new Person().Compare);
        }

        public void sortByScoreAverage()
        {
            students.Sort(new StudentComparer());
        }
        
        public List<List<Student>> AverageMarkGroup(double value)
        {
            return students
                .GroupBy(s => s.AverageScore.Equals(value))
                .Select(grp => grp.ToList())
                .ToList();
        }

    }
}