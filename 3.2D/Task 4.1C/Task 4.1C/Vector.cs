using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task_4._1C
{
    public class Vector<T> : IEnumerable<T>  where T : IComparable<T>
    {
        // This constant determines the default number of elements in a newly created vector.
        // It is also used to extended the capacity of the existing vector
        private const int DEFAULT_CAPACITY = 10;

        // This array represents the internal data structure wrapped by the vector class.
        // In fact, all the elements are to be stored in this private  array. 
        // You will just write extra functionality (methods) to make the work with the array more convenient for the user.
        private T[] data;

        // This property represents the number of elements in the vector
        public int Count { get; private set; } = 0;

        // This property represents the maximum number of elements (capacity) in the vector
        public int Capacity
        {
            get { return data.Length; }
        }

        // This is an overloaded constructor
        public Vector(int capacity)
        {
            data = new T[capacity];
        }

        // This is the implementation of the default constructor
        public Vector() : this(DEFAULT_CAPACITY) { }

        // An Indexer is a special type of property that allows a class or structure to be accessed the same way as array for its internal collection. 
        // For example, introducing the following indexer you may address an element of the vector as vector[i] or vector[0] or ...
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index >= Count || index < 0) throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        // This private method allows extension of the existing capacity of the vector by another 'extraCapacity' elements.
        // The new capacity is equal to the existing one plus 'extraCapacity'.
        // It copies the elements of 'data' (the existing array) to 'newData' (the new array), and then makes data pointing to 'newData'.
        private void ExtendData(int extraCapacity)
        {
            T[] newData = new T[Capacity + extraCapacity];
            for (int i = 0; i < Count; i++) newData[i] = data[i];
            data = newData;
        }

        // This method adds a new element to the existing array.
        // If the internal array is out of capacity, its capacity is first extended to fit the new element.
        public void Add(T element)
        {
            if (Count == Capacity) ExtendData(DEFAULT_CAPACITY);
            data[Count++] = element;
        }

        // This method searches for the specified object and returns the zero‐based index of the first occurrence within the entire data structure.
        // This method performs a linear search; therefore, this method is an O(n) runtime complexity operation.
        // If occurrence is not found, then the method returns –1.
        // Note that Equals is the proper method to compare two objects for equality, you must not use operator '=' for this purpose.
        public int IndexOf(T element)
        {
            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(element)) return i;
            }
            return -1;
        }

        public ISorter Sorter { set; get; } = new DefaultSorter();

        internal class DefaultSorter : ISorter
        {
            public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
            {
                if (comparer == null) comparer = Comparer<K>.Default;
                Array.Sort(sequence, comparer);
            }
        }

        public void Sort()
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            Sorter.Sort(data, null);
        }

        public void Sort(IComparer<T> comparer)
        {
            if (Sorter == null) Sorter = new DefaultSorter();
            Array.Resize(ref data, Count);
            if (comparer == null) Sorter.Sort(data, null);
            else Sorter.Sort(data, comparer);
        }

        public int BinarySearch(T element, IComparer<T> compares)
        {
            if (this.Count == 0) return -1;

            if (compares == null) compares = Comparer<T>.Default;
            return BinarySearch(element, compares, 0, data.Length -1 );
        }

        private int BinarySearch(T item, IComparer<T> compares, int left, int right)
        {
            if (left > right) return -1;

            int mid = (left + right);

            if (compares.Compare(data[mid], item) == 0) return mid;
            else if (compares.Compare(data[mid], data[right]) < 0)
            {
                return BinarySearch(item, compares, mid + 1, right);
            }
            else
            {
                return BinarySearch(item, compares, left, mid - 1);
            }
 
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Iterator(this);

        }
            

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Iterator : IEnumerator<T> 
        {
            private Vector<T> _vector; 

            private int _curretIndex = -1;

            public Iterator(Vector<T> vector) {  _vector = vector; }

            public bool MoveNext()
            {
                _curretIndex++;
                return (_curretIndex < _vector.Count);
            }

            public T Current {

                get 
                {
                    try { return _vector.data[_curretIndex]; }
                    catch (IndexOutOfRangeException) 
                    { return default(T); } 
                }

            }
            object IEnumerator.Current
            {
                get
                {
                    try
                    {
                        return _vector.data[_curretIndex];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return default(T);
                    }

                }
            }
            public void Reset()
            {
                _curretIndex = -1;
            }

            public void Dispose() { }

        }
       

    }
}
