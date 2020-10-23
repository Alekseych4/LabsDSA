namespace LabSaod_9
{
    public class TreeNodeForPrint<T>
    {
        private int _level;
        private T _node;

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public T Node
        {
            get => _node;
            set => _node = value;
        }
    }
}