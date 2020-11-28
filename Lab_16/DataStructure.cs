namespace Lab_16
{
    public class DataStructure<T>
    {
        private T _node;
        private DataStructure<T> _next;

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