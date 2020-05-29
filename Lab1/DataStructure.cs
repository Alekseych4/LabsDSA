namespace Lab1
{
    public class DataStructure<T>
    {
        private T _node;
        private DataStructure<T> _previous;

        public T Node
        {
            get => _node;
            set => _node = value;
        }

        public DataStructure<T> Previous
        {
            get => _previous;
            set => _previous = value;
        }
    }
}