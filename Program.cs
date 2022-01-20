using System;
using System.Collections.Generic;
using System.IO;

//1 var

namespace lab3sh
{
    delegate TKey KeySelector<TKey>(Student st);
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    delegate void StudentsChangedHandler<TKey>(object source, StudentsChangedEventArgs<TKey> args);
    class Program
    {
        private static void Mark(string str)
        {
            Console.WriteLine("\n>>>>> " + str + " <<<<<\n");
        }

        private static bool CheckFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("File not found. Creating new.");
                Console.ResetColor();
                FileStream fs = File.Create(filename);
                fs.Close();
                return false;
            }
            
            return true;
        }

        static void Main(string[] args)
        {
            Student student = new Student(new Person("Ivan", "Ivanov", DateTime.Now), Education.Specialist, 150);
            student.AddExams(new Exam("Calculus", 4, DateTime.Now));

            // 1. Deep copying
            Student dcStudent = student.DeepCopy();
            Mark("Original");
            Console.WriteLine(student.ToString());
            Mark("Deep copy with serializing");
            Console.WriteLine(dcStudent.ToString());

            // 2. Loading from file
            Console.WriteLine("Enter file name to read:");
            string filename = Console.ReadLine();
            if (CheckFile(filename))
            {
                // 3.
                Mark("Read student:");
                if (student.Load(filename))
                    Console.WriteLine(student);
            }
            
            // 4. Adding from console and saving
            student.AddFromConsole();
            student.Save(filename);
            Mark("Saved student");
            Console.WriteLine(student.ToString());
            
            // 5. Static saving / loading
            Student.Load(filename, student);
            student.AddFromConsole();
            Student.Save(filename, student);
            
            //6.
            Mark("Saved student");
            Console.WriteLine(student.ToString());
        }
    }
}