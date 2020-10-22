namespace LabSaod_9
{
    public class DataStructure<T>
    {
        private T _node;
        private DataStructure<T> _leftDesc;
        private DataStructure<T> _rightDesc;

        public T Node
        {
            get => _node;
            set => _node = value;
        }

        public DataStructure<T> LeftDesc
        {
            get => _leftDesc;
            set => _leftDesc = value;
        }

        public DataStructure<T> RightDesc
        {
            get => _rightDesc;
            set => _rightDesc = value;
        }
    }
}