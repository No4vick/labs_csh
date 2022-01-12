using System;
using System.Collections.Generic;

namespace lab3sh
{
    public class Exam : IComparable, IComparer<Exam>
    {
        string subject;
        int score;
        DateTime examDate;

        public Exam(string newSubject, int newScore, DateTime newDate)
        {
            subject = newSubject;
            score = newScore;
            examDate = newDate;
        }

        public Exam() : this("Calculus", 0, new DateTime(2000, 1, 1))
        {

        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public DateTime ExamDate
        {
            get { return examDate; }
            set { examDate = value; }
        }
        public override string ToString()
        {
            return $"Subject: {subject}; Score: {score}; Date: {examDate.ToShortDateString()}\n";
        }

        public int CompareTo(object obj)
        {
            Exam p = obj as Exam;
            if (p != null)
            {
                return String.Compare(this.Subject, p.Subject, StringComparison.Ordinal);
            }
            else
            {
                throw new Exception("Not comparable.");
            }
        }

        public int Compare(Exam x, Exam y)
        {
            if (x is null || y is null)
            {
                throw new ArgumentNullException();
            }
            
            if (x.Score > y.Score)
            {
                return 1;
            }

            if (x.Score < y.Score)
            {
                return -1;
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            Exam exam = obj as Exam;
            return IsEqual(exam);
        }
        public bool Equals(Exam obj)
        {
            if (obj is null)
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return IsEqual(obj);
        }
        private bool IsEqual(Exam obj)
        {
            return (subject == obj.subject) && (score == obj.score) && (examDate == obj.examDate);
        }
        public static bool operator ==(Exam lhs, Exam rhs)
        {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Exam lhs, Exam rhs)
        {
            return !lhs.Equals(rhs);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(subject, score, examDate);
        }
        public virtual object DeepCopy()
        {
            Exam exam = new();
            exam.subject = subject;
            exam.score = score;
            exam.examDate = examDate;
            return exam;
        }
        public DateTime Date
        {
            get { return examDate; }
            set { examDate = value; }
        }
    }
}
