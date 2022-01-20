using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

namespace lab3sh
{
    [Serializable]
    internal class Student : Person, IEnumerable, INotifyPropertyChanged
    {
        Education educationType;
        int groupNumber;
        List<Exam> passedExams;
        List<Test> tests;
        public event PropertyChangedEventHandler PropertyChanged;

        public Student(Person newPersonData, Education newEducation, int newGroupNumber)
        {
            PersonData = newPersonData;
            educationType = newEducation;
            groupNumber = newGroupNumber;
            passedExams = new();
            tests = new();
        }
        public Student() : this(new Person(), Education.Bachelor, 11) { }

        public override string Name
        {
            get { return name; }
            set
            {
                name = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Person data"));
            }
        }
        public Education EducationType
        {
            get { return educationType; }
            set
            {
                educationType = value; 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Education type"));
            }
        }
        public List<Exam> PassedExams
        {
            get { return passedExams; }
            set
            {
                passedExams = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Passed exams"));
            }
        }
        public List<Test> Tests
        {
            get { return tests; }
            set
            {
                tests = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tests"));
            }
        }
        public int GroupNumber
        {
            get { return groupNumber; }
            set
            {
                if (value <= 100 || value >= 599)
                    throw new System.OverflowException("Invalid group number. The group number cannot be higher" + 
                                                       " than 599 or lower than 100. (100 <= x <= 599)");
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Group number"));
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
            {
                passedExams.Add(exam);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Exam"));
            }
        }
        public void AddTests(params Test[] newTests)
        {
            foreach (Test test in newTests)
            {
                tests.Add(test);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Test"));
            }
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
            return String.Format("> Student:\n{0}\n> Education type:\n{1}\n> Group number: {2}\n> Passed exams:\n{3}\n> Tests:\n{4}",
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
        // public override object DeepCopy()
        // {
        //     Student student = new((Person)PersonData.DeepCopy(), educationType, groupNumber);
        //     foreach (Exam exam in passedExams)
        //         student.AddExams((Exam)exam.DeepCopy());
        //     foreach (Test test in tests)
        //         student.AddTests((Test)test.DeepCopy());
        //     return student;
        // }
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
        
        public new Student DeepCopy()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (Student) formatter.Deserialize(stream);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh no. Error: " + e.Message);
                Console.ResetColor();
                return new Student();
            }
            
        }

        public bool Save(string filename)
        {
            try
            {
                // @"E:\vs proj's\rider proj's\labs_csh\test";
                using var fs = File.OpenWrite(filename);
                DataContractSerializer serializer = new(typeof(Student));
                serializer.WriteObject(fs, this);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Load(string filename)
        {
            try
            {
                // @"E:\vs proj's\rider proj's\labs_csh\test";
                using var fs = File.OpenRead(filename);
                DataContractSerializer serializer = new(typeof(Student));
                var readStudent = (Student) serializer.ReadObject(fs);
                
                name = readStudent.name;
                surname = readStudent.surname;
                date = readStudent.date;
                educationType = readStudent.educationType;
                groupNumber = readStudent.groupNumber;
                passedExams = readStudent.passedExams;
                tests = readStudent.tests;
                
                return true;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh no. Error: " + e.Message);
                Console.ResetColor();
                return false;
            }
        }

        public bool AddFromConsole()
        {
            try
            {
                Console.WriteLine("Enter new exam to add in this format:\nSubject Name, Score (0-5), ExamDD.ExamMM.ExamYYYY");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Write \"skip\" to skip.");
                Console.ResetColor();
                
                string[] splitWords = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                if (splitWords.Contains("skip"))
                    return true;
                string[] dateStrings = splitWords[2].Split(".");
                passedExams.Add(
                    new Exam(splitWords[0], Convert.ToInt16(splitWords[1]),
                    new DateTime(
                        Convert.ToInt32(dateStrings[2]),
                        Convert.ToInt16(dateStrings[1]),
                        Convert.ToInt16(dateStrings[0])))
                    );
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input");
                Console.ResetColor();
                return false;
            }
        }

        public static bool Save(string filename, Student student)
        {
            try
            {
                // @"E:\vs proj's\rider proj's\labs_csh\test";
                using var fs = File.OpenWrite(filename);
                DataContractSerializer serializer = new(typeof(Student));
                serializer.WriteObject(fs, student);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool Load(string filename, Student student)
        {
            try
            {
                // @"E:\vs proj's\rider proj's\labs_csh\test";
                using var fs = File.OpenRead(filename);
                DataContractSerializer serializer = new(typeof(Student));
                var readStudent = (Student) serializer.ReadObject(fs);
                
                student.Name = readStudent.name;
                student.Surname = readStudent.surname;
                student.Date = readStudent.date;
                student.EducationType = readStudent.educationType;
                student.GroupNumber = readStudent.groupNumber;
                student.PassedExams = readStudent.passedExams;
                student.Tests = readStudent.tests;
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
