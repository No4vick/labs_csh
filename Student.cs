using System;
using System.Collections;
using System.Collections.Generic;

namespace lab3sh
{
    internal class Student : Person, IEnumerable
    {
        Education educationType;
        int groupNumber;
        List<Exam> passedExams;
        List<Test> tests;

        public Student(Person newPersonData, Education newEducation, int newGroupNumber)
        {
            PersonData = newPersonData;
            educationType = newEducation;
            groupNumber = newGroupNumber;
            passedExams = new();
            tests = new();
        }
        public Student() : this(new Person(), Education.Bachelor, 11) { }

        public Student(int n)
        {
            Random rnd = new Random();
            educationType = (Education) (n % 3);
            groupNumber = rnd.Next(101, 598);
            passedExams = new() { new Exam() };
            tests = new();
            Tests.Add(new Test());
        }
        
        public Person PersonData
        {
            get { return new Person(name, surname, date); }
            set
            {
                name = value.Name;
                surname = value.Surname;
                date = value.Date;
            }
        }
        public Education EducationType
        {
            get { return educationType; }
            set { educationType = value; }
        }
        public List<Exam> PassedExams
        {
            get { return passedExams; }
            set => passedExams = value;
        }
        public List<Test> Tests
        {
            get { return tests; }
            set { tests = value; }
        }
        public int GroupNumber
        {
            get { return groupNumber; }
            set
            {
                if (value <= 100 || value >= 599)
                    throw new System.OverflowException("Invalid group number. The group number cannot be higher" + 
                                                       " than 599 or lower than 100. (100 <= x <= 599)");
            }
        }
        public double AverageScore
        {
            get
            {
                double avg = 0;
                if (passedExams.Count == 0)
                {
                    return avg;
                }
                foreach (Exam exam in passedExams)
                {
                    avg += exam.Score;
                }
                return avg / passedExams.Count;
            }
        }

        public bool this[Education e] // Индексатор
        {
            get
            {
                return educationType == e;
            }
        }
        public void AddExams(params Exam[] newExams)
        {
            foreach (Exam exam in newExams)
                passedExams.Add(exam);
        }
        public void AddTests(params Test[] newTests)
        {
            foreach (Test test in newTests)
                tests.Add(test);
        }
        string EducationString()
        {
            string educationString = "None";
            switch (educationType)
            {
                case Education.Specialist:
                    educationString = "Specialist";
                    break;
                case Education.Bachelor:
                    educationString = "Bachelor";
                    break;
                case Education.SecondEducation:
                    educationString = "Second Education";
                    break;
            }
            return educationString;
        }
        public override string ToString()
        {
            //string educationStr = ;
            string examsString = "";
            if (passedExams.Count > 0)
            {
                foreach (Exam exam in passedExams)
                {
                    examsString += exam.ToString();
                }
            }
            else
                examsString = "None";
            string testsString = "";
            if (tests.Count > 0)
            {
                foreach (Test test in tests)
                {
                    testsString += test.ToString();
                }
            }
            else
                testsString = "None";
            return String.Format("> Student:\n{0}\n> Education type:\n{1}\n> Group number:{2}\n> Passed exams:\n{3}\n> Tests:\n{4}",
                PersonData.ToString(), EducationString(), groupNumber.ToString(), examsString, testsString);
        }
        public override string ToShortString()
        {
            return String.Format("/ Student:\n{0}\n/ Education type:\n{1}\n/ Group number:\n{2}\n/ Passed exams AVG:\n{3}",
                PersonData.ToShortString(), EducationString(), groupNumber.ToString(), AverageScore);
        }
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            Student student = obj as Student;
            return IsEqual(student);
        }
        public bool Equals(Student obj)
        {
            if (obj is null)
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return IsEqual(obj);
        }
        private bool IsEqual(Student obj)
        {
            bool equalsExam = passedExams.Count == 0 && obj.PassedExams.Count == 0;
            foreach (Exam exam in passedExams) equalsExam = obj.PassedExams.Contains(exam);
            return (PersonData.Equals(obj.PersonData)) && (educationType.Equals(obj.educationType)) && 
                   (groupNumber.Equals(obj.groupNumber)) && equalsExam;
        }
        public static bool operator ==(Student lhs, Student rhs)
        {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Student lhs, Student rhs)
        {
            return !lhs.Equals(rhs);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(PersonData, educationType, groupNumber, passedExams, tests);
        }
        public override object DeepCopy()
        {
            Student student = new((Person)PersonData.DeepCopy(), educationType, groupNumber);
            // Exam[] newPassedExams = new Exam[passedExams.Length];
            // for (int i = 0; i < passedExams.Length; i++)
            //     newPassedExams[i] = (Exam)passedExams[i].DeepCopy();
            foreach (Exam exam in passedExams)
                student.AddExams((Exam)exam.DeepCopy());
            foreach (Test test in tests)
                student.AddTests((Test)test.DeepCopy());
            return student;
        }
        public IEnumerable<object> GetAllExams()
        {
            foreach (Exam exam in passedExams)
                yield return exam;
            foreach (Test test in tests)
                yield return test;
        }
        public IEnumerable<object> GetAllPassedExams()
        {
            int grade = 2;
            foreach (Exam exam in passedExams)
                if (exam.Score > grade) 
                    yield return exam;
            foreach (Test test in tests)
                if (test.IsCompleted)
                    yield return test;
        }
        public IEnumerable<Exam> GetExams(double grade)
        {
            foreach (Exam exam in passedExams)
                if (exam.Score > grade)
                    yield return exam;
        }
        public IEnumerable<Test> GetTestsWithExams()
        {
            int grade = 2;
            foreach (Test test in tests)
                foreach (Exam exam in passedExams)
                    if (test.Subject == exam.Subject && exam.Score > grade && test.IsCompleted)
                        yield return test;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StudentEnumerator(this);
        }

        public void SortExamsSubject()
        {
            passedExams.Sort((x, y) => x.CompareTo(y));
        }

        public void SortExamsScore()
        {
            passedExams.Sort(new Exam().Compare);
        }

        public void SortExamDate()
        {
            passedExams.Sort(new ExamComparer().Compare);
        }
        
        public string GetExamsString()
        {
            string str = "Exam list:\n";
            foreach(Exam i in passedExams)
            {
                str = str + i + "\n";
            }
            return str;
        }
    }
}
