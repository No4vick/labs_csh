using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace lab3sh
{
    class StudentEnumerator: System.Collections.IEnumerator
    {
        int position = -1;
        System.Collections.Generic.List<string> subjects;

        public StudentEnumerator(Student st)
        {
            subjects = new List<string>();
            foreach (Exam exam in st.PassedExams)
            {
                foreach (Test test in st.Tests)
                {
                    if (exam.Subject.Equals(test.Subject))
                    {
                        subjects.Add(exam.Subject);
                    }
                }
            }    
        }
        public object Current
        {
            get
            {
                return subjects[position];
            }
        }
        public bool MoveNext()
        {
            if (position < subjects.Count - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }
        public void Reset()
        {
            position = -1;
        }
    }
}
