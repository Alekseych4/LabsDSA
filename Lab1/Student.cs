namespace Lab1
{
    public class Student
    {
        private string _name;
        private string _surname;
        private string _mark;

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
    }
}