namespace Lab1
{
    public class DataStructure<T>
    {
        private int _next;
        private int _previous;

        public int Previous
        {
            get => _previous;
            set => _previous = value;
        }

        private T _node;

        public int Next
        {
            get => _next;
            set => _next = value;
        }

        public T Node
        {
            get => _node;
            set => _node = value;
        }
    }
}