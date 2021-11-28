using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3sh
{
    class Person : IDateAndCopy, IComparable, IComparer<Person>
    {
        protected string name;
        protected string surname;
        protected DateTime date;

        public Person(string newName, string newSurname, DateTime newDate)
        {
            name = newName;
            surname = newSurname;
            date = newDate;
        }
        public Person():this("John", "Smith", new DateTime(2000, 1, 1))
        {

        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public int BdayYear
        {
            get { return date.Year; }
            set { date = new DateTime(value, date.Month, date.Day); }
        }
        public override string ToString()
        {
            return $"Name: {name}, Surname: {surname}, Bday: {date.ToShortDateString()}";                      
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Person otherPerson = obj as Person;
            if (otherPerson != null)
                return this.date.CompareTo(otherPerson.date);
            else
                throw new ArgumentException("Object is not a Person");
        }

        public virtual string ToShortString()
        {
            return name + " " + surname;
        }

        public int Compare(Person x, Person y)
        {
            if (x is null || y is null)
            {
                throw new NullReferenceException();
            } 
            return x.date.CompareTo(y.date) != 0 ? x.date.CompareTo(y.date) : 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            Person person = obj as Person;
            return IsEqual(person);
        }
        public bool Equals(Person obj)
        {
            if (obj is null)
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return IsEqual(obj);
        }
        public static bool operator==(Person lhs, Person rhs)
        {
            return lhs.Equals(rhs); 
        }
        public static bool operator !=(Person lhs, Person rhs)
        {
            return !lhs.Equals(rhs);
        }
        private bool IsEqual(Person obj)
        {
            return (name == obj.name) && (surname == obj.surname) && (date == obj.date);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(name, surname, date);
        }
        public virtual object DeepCopy()
        {
            Person person = new();
            person.name = name;
            person.surname = surname;
            person.date = date;
            return person;
        }
    }
}
