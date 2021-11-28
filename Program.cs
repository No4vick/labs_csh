using System;
using System.Diagnostics; //1 var
using System.Runtime.InteropServices;

namespace lab3sh
{
    class Program
    {
        static void Mark(string text)
        {
            Console.WriteLine(">>>>>>>>>>>>>>> {0} <<<<<<<<<<<<<<<", text);
        }
        static void Main(string[] args)
        {
            Mark("1");
            Student a = new();
            Student b = (Student)a.DeepCopy();
            Console.WriteLine("{0}\n{1}", a.ToShortString(), a.ToShortString());
            Console.WriteLine("Are the objects same: {0}\nAre the adresses same: {1}\nHash codes:\na = {2}\nb = {3}\nEqual?: {4}\n",
                a == b? "Yes" : "No", (object)a == (object)b ? "Yes" : "No", a.GetHashCode(), b.GetHashCode(), a.GetHashCode() == b.GetHashCode()? "Yes" : "No");

            Mark("2");
            Student student = new();
            student.AddExams(new Exam("English", 3, new DateTime()));
            student.AddTests(new Test());
            Console.WriteLine(student.ToString());
            Console.WriteLine();
            Mark("3");
            Console.WriteLine(student.PersonData.ToString());
            Mark("4");
            Student altStudent = (Student)student.DeepCopy();
            student.EducationType = Education.Specialist;
            student.BdayYear = 2016;
            student.AddExams(new Exam("PE", 6, new DateTime()));
            Console.WriteLine(student.ToString());
            Console.WriteLine();
            Console.WriteLine(altStudent.ToString());
            Mark("5");
            try
            {
                student.GroupNumber = 600;
            }
            catch (OverflowException err)
            {
                System.Console.WriteLine(err.Message);
            }
            Mark("6");
            foreach(object obj in student.GetAllExams())
                Console.WriteLine(obj.ToString());
            Mark("7");
            foreach (Exam exam in student.GetExams(3.0))
                Console.WriteLine(exam.ToString());
            Console.WriteLine("----------------------------AE--------------------------");
            Mark("8");
            student.AddTests(new Test("English", true));
            Console.WriteLine(student.ToString());
            Console.WriteLine("-------------Same Exams-------------");
            foreach(string p in student)
            {
                Console.WriteLine(p);
            }
            Mark("9");
            foreach(var p in student.GetAllPassedExams())
            {
                Console.WriteLine(p.ToString());
            }
            Mark("10");
            foreach(Test p in student.GetTestsWithExams())
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
