using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace _3rd_Assigment
{
    internal class GenericListEnumerator<X> : IEnumerator<X>
    {
        private GenericList<X> _genericList;
        private int _curIndex;


        public GenericListEnumerator(GenericList<X> genericList)
        {
            this._genericList = genericList;
            _curIndex = -1;
            Current = default(X);
        }

        public X Current { get; set; }

        object IEnumerator.Current => Current;
        

        public void Dispose()
        {
           
        }

        public bool MoveNext()
        {
            if (++_curIndex >= _genericList.Count)
            {
                return false;
            }

            Current = _genericList.GetElement(_curIndex);

            return true;
        }

        public void Reset()
        {
            _curIndex = -1;
            Current = default(X);
        }
    }
}