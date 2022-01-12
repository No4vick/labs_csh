using System;
using System.Collections.Generic;

//1 var

namespace lab3sh
{
    delegate TKey KeySelector<TKey>(Student st);
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    class Program
    {
        static void Main(string[] args)
        {

            GenerateElement<Person, Student> generateElement = n => new KeyValuePair<Person, Student>(new Person(n), new Student(n));

            
            KeySelector<String> delegateKeySelector = st =>  st.GetHashCode().ToString(); 
            var collection = new StudentCollection<String>(delegateKeySelector);
            
            Exam exam1 = new Exam("Exam 1", 4, new DateTime(1, 1, 1));
            Exam exam2 = new Exam("Exam 2",3 , new DateTime(1, 1, 2));
            Exam exam3 = new Exam("Exam 3", 5, new DateTime(1, 1, 1));
            Console.WriteLine(exam1.CompareTo(exam2));
            Console.WriteLine(exam2.CompareTo(exam1));
            Console.WriteLine(exam3.CompareTo(exam1));
            
            Student defaultStudent = new Student();

            defaultStudent.AddExams(exam1, exam2, exam3);
            Console.WriteLine(defaultStudent.GetExamsString());

            defaultStudent.SortExamDate();
            Console.WriteLine(defaultStudent.GetExamsString());

            defaultStudent.SortExamsScore();
            Console.WriteLine(defaultStudent.GetExamsString());

            defaultStudent.SortExamsSubject();
            Console.WriteLine(defaultStudent.GetExamsString());

            collection.AddDefaults();
            collection.AddStudent(defaultStudent, new Student());
            Console.WriteLine(collection.ToString());

            Console.WriteLine(collection.AverageScore);

            Console.WriteLine("-----------------------");
            foreach(var i in collection.EducationForm(Education.Specialist))
            {
                Console.WriteLine(i.Value.ToString());
            }

            foreach (var group in collection.GroupEducation)
            {
                Console.WriteLine("\nAge group: " + group.Key);
                foreach(var student in group)
                {
                    Console.WriteLine(student.Value.ToShortString());
                }
            }
            TestCollections<Person, Student> collections = new TestCollections<Person, Student>(800000, generateElement);
            collections.testTime();
        }
            
    }
   
}