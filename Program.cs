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
            GenerateElement<Person, Student> generateElement =
                n => new KeyValuePair<Person, Student>(new Person(n), new Student(n));
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
            collection1.StudentChanged += journal.OnStudentChange;

            
        }

    }
   
}