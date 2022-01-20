using System;
using System.Collections.Generic;

//1 var

namespace lab3sh
{
    delegate TKey KeySelector<TKey>(Student st);
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    delegate void StudentsChangedHandler<TKey>(object source, StudentsChangedEventArgs<TKey> args);
    class Program
    {
        static void Main(string[] args)
        {
            KeySelector<String> delegateKeySelector = st => st.GetHashCode().ToString();

            StudentCollection<string> collection1 = new(delegateKeySelector)
            {
                CollectionName = "PIN-1"
            };
            StudentCollection<string> collection2 = new(delegateKeySelector)
            {
                CollectionName = "PIN-2"
            };

            Journal journal = new();
            collection1.StudentChanged += journal.OnStudentChange;
            collection2.StudentChanged += journal.OnStudentChange;

            Random rand = new();

            Student student1 = new Student(rand.Next());
            Student student2 = new Student(rand.Next());
            Student studentDefault = new();
            
            // Addition
            collection1.AddStudent(student1);
            
            collection2.AddStudent(student2);
            collection2.AddStudent(studentDefault);
            
            // Change
            student1.Name = "Ivan";
            student2.Tests.Add(new Test());

            // Deletion
            collection2.Remove(studentDefault);
            
            // Change of properties of deleted element
            studentDefault.Name = "Zakhar";
            studentDefault.PassedExams.Add(new Exam());
            
            Console.WriteLine(journal.ToString());
        }

    }
   
}