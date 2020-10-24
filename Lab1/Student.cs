
using System;

namespace Lab1
{
    public class Student : IDisposable
    {
        private string _name;
        private string _surname;
        private string _mark;

        public Student()
        {
            
        }

        public Student(string name, string surname, string mark)
        {
            Name = name;
            Surname = surname;
            Mark = mark;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public string Mark
        {
            get => _mark;
            set => _mark = value;
        }

        public override string ToString()
        {
            return Name + " " + Surname + " " + Mark;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                return Name.Equals(((Student) obj).Name);
            }

            return false;
        }

        public void Dispose()
        {
            var weakStudent = new WeakReference(this);
            _mark = null;
            _name = null;
            _surname = null;
        }
    }
}