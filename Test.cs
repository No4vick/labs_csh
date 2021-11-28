using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3sh
{
    class Test
    {
        public string Subject { get; set; }
        public bool IsCompleted { get; set; }
        public Test(string newSubject, bool newIsCompleted)
        {
            Subject = newSubject;
            IsCompleted = newIsCompleted;
        }
        public Test() : this("Calculus", false) { }
        public override string ToString()
        {
            return string.Format("Subject: {0}; Completed: {1}\n", Subject, IsCompleted?"Yes":"No");
        }
        public virtual object DeepCopy()
        {
            Test test = new();
            test.Subject = Subject;
            test.IsCompleted = IsCompleted;
            return test;
        }
    }
}
