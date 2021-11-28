using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3sh
{
    class Person : IDateAndCopy
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
            return String.Format("Name: {0}, Surname: {1}, Bday: {2}", name, surname, date.ToShortDateString());                      
        }
        public virtual string ToShortString()
        {
            return name + " " + surname;
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
