namespace Lab1
{
    public class DataStructure<T>
    {
        private T _node;
        private DataStructure<T> _next;
        private DataStructure<T> _previous;

        public DataStructure<T> Previous
        {
            get => _previous;
            set => _previous = value;
        }

        public T Node
        {
            get => _node;
            set => _node = value;
        }

        public DataStructure<T> Next
        {
            get => _next;
            set => _next = value;
        }
    }
}