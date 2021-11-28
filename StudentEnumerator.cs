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
        int postition = -1;
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
                return subjects[postition];
            }
        }
        public bool MoveNext()
        {
            if (postition < subjects.Count - 1)
            {
                postition++;
                return true;
            }
            else
                return false;
        }
        public void Reset()
        {
            postition = -1;
        }
    }
}
