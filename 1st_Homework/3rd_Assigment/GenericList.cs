using System;
using System.Collections;
using System.Collections.Generic;

namespace _3rd_Assigment
{
    public class GenericList<X>:IGenericList<X> 
    {

        private X[] _internalStorage;
        private int _indexOfLastElement;

        public GenericList()
        {
            _internalStorage = new X[4];
            _indexOfLastElement = -1;
        }

        public GenericList(int initialSize)
        {
            if (initialSize > 0)
            {
                _internalStorage = new X[initialSize];
            }

            else
            {
                Console.WriteLine("Invalid size. Setting size to default value.");
                _internalStorage = new X[4];
            }
            _indexOfLastElement = -1;
        }




        public void Add(X item)
        {
            if (_internalStorage.Length == Count)
            {
                X[] temp = new X[_internalStorage.Length * 2];

                for (int i = 0; i < _internalStorage.Length; i++)
                {
                    temp[i] = _internalStorage[i];
                }

                _internalStorage = temp;
            }

            _internalStorage[Count] = item;
            _indexOfLastElement++;
        }

        public bool Remove(X item)
        {


            for (int i = 0; i < Count; i++)
            {

                if (_internalStorage[i].Equals(item))
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

            for (int i = index; i < Count - 1; i++)
            {
                _internalStorage[i] = _internalStorage[i + 1];

            }

            _indexOfLastElement--;

            return true;
        }

        public X GetElement(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            return _internalStorage[index];
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public int Count => _indexOfLastElement + 1;

        public void Clear()
        {
            _indexOfLastElement = -1;
            int size = _internalStorage.Length;
            _internalStorage = new X[size];
        }

        public bool Contains(X item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }


        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}