using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace lab3sh
{
    class TestCollections
    {
        private List<Person> personList;
        private List<string> strList;
        private Dictionary<Person, Student> personDict;
        private Dictionary<string, Student> strDict;
        private static Student generatedStudent;

        public TestCollections(int n)
        {
            personList = new List<Person>(n);
            strList = new List<String>(n);
            personDict = new Dictionary<Person, Student>(n);
            strDict = new Dictionary<string, Student>(n);
            for (int i = 0; i < n; i++)
            {
                Student student = generateStudent(i);
                personList.Add(student);
                strList.Add(student.ToString());
                personDict.Add(student, student);
                strDict.Add(student.ToString(), student);
            }
        }
        
        static ref Student generateStudent(int n)
        {
            Random rand = new Random();
            ref Student student = ref generatedStudent;
            student = new Student(new Person("Student", $"-{n}-", new DateTime()),
                Education.SecondEducation, rand.Next(100, 599));
            return ref student;
        }

        void testSearchTimeListGeneric<T>(List<T> list)
        {
            var first = list[0];
            var middle = list[list.Count / 2];
            var last = list[list.Count - 1];
            var none = (T)(object)generateStudent(list.Count + 1);

            Stopwatch stopwatch = Stopwatch.StartNew();
            
            var watch = Stopwatch.StartNew();
            list.Contains(first);
            stopwatch.Stop();
            Console.WriteLine("First element: " + stopwatch.Elapsed.Ticks);

            stopwatch.Restart();
            list.Contains(middle);
            stopwatch.Stop();
            Console.WriteLine("Middle element: " + stopwatch.Elapsed.Ticks);

            stopwatch.Restart();
            list.Contains(last);
            stopwatch.Stop();
            Console.WriteLine("Last element: " + stopwatch.Elapsed.Ticks);

            stopwatch.Restart();
            list.Contains(none);
            stopwatch.Stop();
            Console.WriteLine("Absent element: " + stopwatch.Elapsed.Ticks);
        }
        
        void testSearchTimeListGeneric(List<string> list)
        {
            var first = list[0];
            var middle = list[list.Count / 2];
            var last = list[list.Count - 1];
            var none = generateStudent(list.Count + 1).ToString();

            Stopwatch stopwatch = Stopwatch.StartNew();
            
            var watch = Stopwatch.StartNew();
            list.Contains(first);
            stopwatch.Stop();
            Console.WriteLine("First element: " + stopwatch.Elapsed.Ticks);

            stopwatch.Restart();
            list.Contains(middle);
            stopwatch.Stop();
            Console.WriteLine("Middle element: " + stopwatch.Elapsed.Ticks);

            stopwatch.Restart();
            list.Contains(last);
            stopwatch.Stop();
            Console.WriteLine("Last element: " + stopwatch.Elapsed.Ticks);

            stopwatch.Restart();
            list.Contains(none);
            stopwatch.Stop();
            Console.WriteLine("Absent element: " + stopwatch.Elapsed.Ticks);
        }
        
        void testSearchTimeDictGenericKey<T, TK>(Dictionary<T, TK> dict)
        {
            var first = dict.ElementAt(0).Key;
            var middle = dict.ElementAt(dict.Count / 2).Key;
            var last = dict.ElementAt(dict.Count - 1).Key;
            var none = (T)(object)generateStudent(dict.Count + 1);

            var watch = Stopwatch.StartNew();
            dict.ContainsKey(first);
            watch.Stop();
            Console.WriteLine("First element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsKey(middle);
            watch.Stop();
            Console.WriteLine("Middle element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsKey(last);
            watch.Stop();
            Console.WriteLine("Last element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsKey(none);
            watch.Stop();
            Console.WriteLine("Absent element: " + watch.Elapsed.Ticks);
        }
        
        void testSearchTimeDictGenericKey<T>(Dictionary<string, T> dict)
        {
            var first = dict.ElementAt(0).Key;
            var middle = dict.ElementAt(dict.Count / 2).Key;
            var last = dict.ElementAt(dict.Count - 1).Key;
            var none = generateStudent(dict.Count + 1).ToString();

            var watch = Stopwatch.StartNew();
            dict.ContainsKey(first);
            watch.Stop();
            Console.WriteLine("First element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsKey(middle);
            watch.Stop();
            Console.WriteLine("Middle element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsKey(last);
            watch.Stop();
            Console.WriteLine("Last element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsKey(none);
            watch.Stop();
            Console.WriteLine("Absent element: " + watch.Elapsed.Ticks);
        }

        void testSearchTimeDictGenericValue<T, TK>(Dictionary<T, TK> dict)
        {
            var first = dict.ElementAt(0).Value;
            var middle = dict.ElementAt(dict.Count / 2).Value;
            var last = dict.ElementAt(dict.Count - 1).Value;
            var none = (TK)(object)generateStudent(dict.Count + 1);

            var watch = Stopwatch.StartNew();
            dict.ContainsValue(first);
            watch.Stop();
            Console.WriteLine("First element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsValue(middle);
            watch.Stop();
            Console.WriteLine("Middle element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsValue(last);
            watch.Stop();
            Console.WriteLine("Last element: " + watch.Elapsed.Ticks);

            watch.Restart();
            dict.ContainsValue(none);
            watch.Stop();
            Console.WriteLine("Absent element: " + watch.Elapsed.Ticks);
        }

        public void testSearchTimeListPerson()
        {
            testSearchTimeListGeneric(personList);
        }

        public void testSearchTimeListStr()
        {
            testSearchTimeListGeneric(strList);
        }

        public void testSearchTimeDictPersonKey()
        {
            testSearchTimeDictGenericKey(personDict);
        }

        public void testSearchTimeDictPersonValue()
        {
            testSearchTimeDictGenericValue(personDict);
        }
        
        public void testSearchTimeDictStrKey()
        {
            testSearchTimeDictGenericKey(strDict);
        }

        public void testSearchTimeDictStrValue()
        {
            testSearchTimeDictGenericValue(strDict);
        }
    }
}