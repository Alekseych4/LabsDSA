using System;

namespace LabSaod_9
{
    public class DataStructure<T> : IDisposable
    {
        private T _value;
        private int _key;
        private int _keyEqualityCounter;
        private DataStructure<T> _leftDesc;
        private DataStructure<T> _rightDesc;

        public T Value
        {
            get => _value;
            set => _value = value;
        }
        
        public int Key
        {
            get => _key;
            set => _key = value;
        }
        
        public int KeyEqualityCounter
        {
            get => _keyEqualityCounter;
            set => _keyEqualityCounter = value;
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

        public void Dispose()
        {
            _value = default;
            _leftDesc = null;
            _rightDesc = null;
            _key = -1;
            _keyEqualityCounter = -1;
        }
    }
}