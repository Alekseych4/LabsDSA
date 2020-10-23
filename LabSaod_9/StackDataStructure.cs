namespace LabSaod_9
{
    public class StackDataStructure<T>
    {
        private T _node;
        private StackDataStructure<T> _previous;

        public T Node
        {
            get => _node;
            set => _node = value;
        }

        public StackDataStructure<T> Previous
        {
            get => _previous;
            set => _previous = value;
        }
    }
}