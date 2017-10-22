using System;

namespace _1st_Assigment
{
    public class IntegerList : IIntegerList
    {
        private int[] _internalStorage;
        private int _indexOfLastElement;

      

        public IntegerList()
        {
            _internalStorage=new int[4];
            //Count = 0;
            _indexOfLastElement = -1;
        }

        public IntegerList(int initialSize)
        {
            if (initialSize >= 0)
            {
                _internalStorage = new int[initialSize];
                
            }

            else
            {
                Console.WriteLine("Initial size can't be negative. Initializing to default value.");
                _internalStorage = new int[4];
            }

            //Count = 0;
            _indexOfLastElement = -1;
        }

        public void Add(int item)
        {
            if (_internalStorage.Length == Count)
            {
                int[] temp= new int[_internalStorage.Length*2];

                for (int i = 0; i < _internalStorage.Length; i++)
                {
                    temp[i] = _internalStorage[i];
                }

                _internalStorage = temp;
            }

            _internalStorage[Count] = item;
            _indexOfLastElement++;

        }

        public bool Remove(int item)
        {
            

            for (int i = 0; i < Count; i++)
            {
                if (_internalStorage[i] == item)
                {
                    int position = i;
                    return RemoveAt(position); 
                }
            }

            return false;

        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
              
            }

            for (int i = index; i < Count-1; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];

            }

            _internalStorage[Count-1] = new int();

            _indexOfLastElement--;

            return true;
        }

        public int GetElement(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            return _internalStorage[index];
        }

        public int IndexOf(int item)
        {
            for (int i=0; i < Count; i++)
            {
                if (_internalStorage[i] == item)
                {
                    return i;
                }
            }

            return -1;
        }

        public int Count => _indexOfLastElement + 1;

        public void Clear()
        {
            int size = _internalStorage.Length;
            _internalStorage=new int[size];
           // Count = 0;
            _indexOfLastElement = -1;
        }

        public bool Contains(int item)
        {
            for(int i=0; i<Count;i++)
            {
                if (_internalStorage[i]== item)
                {
                    return true;
                }
            }

            return false;
        }
    }
}