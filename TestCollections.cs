using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace lab3sh
{
    class TestCollections<TKey, TValue>
    {
        private List<TKey> Tlist;
        private List<string> strList;
        private Dictionary<TKey, TValue> TDict;
        private Dictionary<string, TValue> strDict;
        private GenerateElement<TKey, TValue> deleg;
        private static Student generatedStudent;

        public TestCollections(int n, GenerateElement<TKey, TValue> method)
        {
            Tlist = new List<TKey>(n);
            strList = new List<String>(n);
            TDict = new Dictionary<TKey, TValue>(n);
            strDict = new Dictionary<string, TValue>(n);
            for (int i = 0; i < n; i++)
            {
                var item = method(i);
                Tlist.Add(item.Key);
                strList.Add(item.Key.ToString());
                TDict.Add(item.Key, item.Value);
                strDict.Add(item.ToString(), item.Value);
            }

            deleg = method;
        }
        
        // static ref KeyValuePair<TKey, TValue> generateStudent(int n)
        // {
        //     Random rand = new Random();
        //     ref Student student = ref generatedStudent;
        //     student = new Student(new Person("Student", $"-{n}-", new DateTime()),
        //         Education.SecondEducation, rand.Next(100, 599));
        //     return ref student;
        // }

        void testSearchTimeListGeneric<T>(List<T> list)
        {
            var first = list[0];
            var middle = list[list.Count / 2];
            var last = list[list.Count - 1];
            var none = (T)(object)deleg(list.Count + 1).Key;

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
            var none = deleg(list.Count + 1).Key.ToString();

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
            var none = (T)(object)deleg(dict.Count + 1).Key;

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
            var none = deleg(dict.Count + 1).ToString();

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
            var none = (TK)(object)deleg(dict.Count + 1).Value;

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
            testSearchTimeListGeneric(Tlist);
        }

        public void testSearchTimeListStr()
        {
            testSearchTimeListGeneric(strList);
        }

        public void testSearchTimeDictPersonKey()
        {
            testSearchTimeDictGenericKey(TDict);
        }

        public void testSearchTimeDictPersonValue()
        {
            testSearchTimeDictGenericValue(TDict);
        }
        
        public void testSearchTimeDictStrKey()
        {
            testSearchTimeDictGenericKey(strDict);
        }

        public void testSearchTimeDictStrValue()
        {
            testSearchTimeDictGenericValue(strDict);
        }

        public void testTime()
        {
            testSearchTimeListPerson();
            testSearchTimeListStr();
            testSearchTimeDictPersonKey();
            testSearchTimeDictPersonValue();
            testSearchTimeDictStrKey();
            testSearchTimeDictStrValue();
        }
    }
}