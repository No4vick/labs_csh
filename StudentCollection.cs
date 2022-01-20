using System;
using System.Collections.Generic;
using System.Linq;

namespace lab3sh
{
    class StudentCollection<TKey>
    {
        private Dictionary<TKey, Student> dictionary;
        private KeySelector<TKey> deleg;

        public StudentCollection(KeySelector<TKey> newDeleg)
        {
            deleg = newDeleg;
        }

        public void AddDefaults()
        {
            int number = 5;
            dictionary = new Dictionary<TKey, Student>();
            for (int i = 0; i < number; i++)
            {
                dictionary.Add(deleg(new Student()), new Student());
            }
        }
        
        public override string ToString()
        {
            string res = new string("");
            foreach (KeyValuePair<TKey, Student> student in dictionary)
            {
                res += $"{student.Key} : {student.Value}\n";
            }
            return res;
        }

        public string ToShortString()
        {
            string res = new string("");
            foreach (KeyValuePair<TKey, Student> student in dictionary)
            {
                res += $"{student.Key} : {student.Value.ToShortString()}\n";
            }
            return res;
        }

        public double AverageScore
        {
            get
            {
                double avg = 0;
                if (dictionary.Count == 0)
                {
                    return avg;
                }

                return Enumerable.Max(getAverageScores());
            }
        }
        
        public IEnumerable<double> getAverageScores()
        {
            foreach (KeyValuePair<TKey, Student> keyValue in dictionary)
            {
                yield return keyValue.Value.AverageScore;
            }
        }

        public IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education value)
        {
            return dictionary.Where(elem => elem.Value.EducationType == value);
        }

        public IEnumerable<IGrouping<Education, KeyValuePair<TKey, Student>>> GroupEducation
        {
            get
            {
                return dictionary.GroupBy(student => student.Value.EducationType);
            }
        }

        public void AddStudent(params Student[] values)
        {
            foreach (var student in values)
            {
                dictionary.Add(deleg(student), student);
            }
        }
        
        public string CollectionName { get; set; }

        public bool Remove(Student st)
        {
            if (!dictionary.ContainsValue(st)) return false;
            
            foreach(var item in dictionary.Where(elem => elem.Value == st).ToList())
            {
                dictionary.Remove(item.Key);
            }
            return true;
        }

        public event StudentsChangedHandler<TKey> StudentChanged;

        private void onStudentCollectionPropertyChanged(Action action, string name, TKey changedKey)
        {
            StudentChanged?.Invoke(this, new StudentsChangedEventArgs<TKey>(CollectionName, action, name, changedKey));
        }
    }
}