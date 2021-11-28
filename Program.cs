using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization; //1 var
using System.Runtime.InteropServices;

namespace lab3sh
{
    class Program
    {
        static void Mark(string text)
        {
            Console.WriteLine(">>>>>>>>>>>>>>> {0} <<<<<<<<<<<<<<<", text);
        }
        static void SubMark(string text)
        {
            Console.WriteLine("******* {0}", text);
        }
        
        static void Main(string[] args)
        {
            //"Создание StudentCollection"
            StudentCollection studentCollection = new StudentCollection();
            studentCollection.AddDefaults();
            List<Exam> exams = new List<Exam>();
            Random rand = new Random();
            List<Student> newStds = studentCollection.ListStudents;
            newStds[0].Name = "Alex";
            newStds[0].Surname = "Alexander";
            newStds[4].Name = "Zed";
            newStds[4].Surname = "Johnson";
            newStds[3].Date = DateTime.Now;
            newStds[2].Date = new DateTime(1990, 12, 3);
            newStds[1].EducationType = Education.Specialist;
            for (int i = 0; i < newStds.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    exams.Add(new Exam("Exam #" + j, rand.Next(1, 5), new DateTime()));
                }
                newStds[i].AddExams(exams.ToArray());
                exams.Clear();
            }
            studentCollection = new StudentCollection();
            studentCollection.ListStudents = newStds;
            Mark("1");
            Console.WriteLine(studentCollection.ToString());
            
            Mark("2");
            StudentCollection sortable = studentCollection;
            
            SubMark("По фамилии:\n");
            sortable.sortBySurname();
            Console.WriteLine(sortable.ToShortString());
            
            SubMark("По дате рождения:\n");
            sortable.sortByBday();
            Console.WriteLine(sortable.ToString());
            
            SubMark("По среднему баллу:\n");
            sortable.sortByScoreAverage();
            Console.WriteLine(sortable.ToShortString());
            
            Mark("3");
            SubMark("1");
            Console.WriteLine(studentCollection.MaxAverage);
            
            SubMark("2");
            foreach(var student in studentCollection.Specialists)
            {
                Console.WriteLine(student.ToString());
            }
            
            SubMark("3");
            double avg = newStds[0].AverageScore;
            SubMark("Equals");
            foreach (Student student in studentCollection.AverageMarkGroup(avg)[0])
            {
                Console.WriteLine(student.ToShortString());
            }
            SubMark("Not equals");
            foreach (Student student in studentCollection.AverageMarkGroup(avg)[1])
            {
                Console.WriteLine(student.ToShortString());
            }
            
            Mark("4");
            TestCollections testCollections = new TestCollections(10);
            SubMark("Person List");
            testCollections.testSearchTimeListPerson();
            SubMark("String List");
            testCollections.testSearchTimeListStr();
            SubMark("Person Dict, key");
            testCollections.testSearchTimeDictPersonKey();
            SubMark("Person Dict, value");
            testCollections.testSearchTimeDictPersonValue();
            SubMark("String Dict, key");
            testCollections.testSearchTimeDictStrKey();
            SubMark("String Dict, value");
            testCollections.testSearchTimeDictStrValue();
        }
    }
}
