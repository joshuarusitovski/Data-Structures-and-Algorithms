using System;
using System.Collections.Generic;
using System.Text;

namespace Task_3._1P
{
    public class Vector<T> where T : IComparable<T>
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

        public override string ToString()
        {
            string contents = "[ ";

            for (int i = 0; i < Count; i++)
            {
                contents = contents + data[i];
                if (i < Count - 1) contents += ", ";
            }

            contents += "]";
            return contents;
        }

    }

    // ------------- THE FOLLOWING CLASSES WERE IMPLEMENTED IN TASK 3.1P ---------------------------------------------------

    class BubbleSort : ISorter
    {
        public BubbleSort() { }

        public void Sort<K>(K[] sequence, IComparer<K> compares) where K : IComparable<K>
        {
            if (sequence is null) throw new ArgumentNullException();
            else if (sequence.Length <= 1) return;

            if (compares == null) compares = Comparer<K>.Default;

            for (int i = 1; i < sequence.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < sequence.Length - 1; j++)
                {
                    if (compares.Compare(sequence[j], sequence[j + 1]) > 0)
                    {
                        K hold = sequence[j];
                        sequence[j] = sequence[j + 1];
                        sequence[j + 1] = hold;
                        count++;
                    }
                }

                if (count == 0) break;
            }

        }
    }


    class InsertionSort : ISorter
    {
        public InsertionSort() { }

        public void Sort<K>(K[] sequence, IComparer<K> compares) where K : IComparable<K>
        {
            if (sequence == null) throw new ArgumentNullException();
            if (sequence.Length <= 1) return;

            if (compares == null) compares = Comparer<K>.Default;

            for (int i = 1; i < sequence.Length; i++)
            {
                K held = sequence[i];
                int j;

                for (j = i - 1; j >= 0 && (compares.Compare(sequence[j], held) > 0); j--)
                {
                    sequence[j + 1] = sequence[j];
                }

                sequence[j + 1] = held;

            }
        }
    }

    class SelectionSort : ISorter
    {
        public SelectionSort() { }

        public void Sort<K>(K[] sequence, IComparer<K> compares) where K : IComparable<K>
        {
            if (sequence == null) throw new ArgumentNullException();
            if (sequence.Length <= 1) return;

            if (compares == null) compares = Comparer<K>.Default;

            for (int i = 0; i < sequence.Length - 1; i++)
            {
                K held;
                int min = i;
                int j;
                for (j = min + 1; j < sequence.Length; j++)
                {
                    if (compares.Compare(sequence[min], sequence[j]) > 0)
                    {
                        min = j;
                    }
                }
                held = sequence[min];
                sequence[min] = sequence[i];
                sequence[i] = held;
            }
        }
    }

    // class RandomizedQuickSort : ISorter
    // {
    //     public RandomizedQuickSort() { }

    //     private Random Random { get; set; } = new Random();
    //     private int RandPivot(int a, int b)
    //     {
    //         return Random.Next(a, b);
    //     }

    //     public void Sort<K>(K[] sequence, IComparer<K> compares) where K : IComparable<K>
    //     {
    //         if (sequence == null) throw new ArgumentNullException();
    //         if (sequence.Length <= 1) return;

    //         if (compares == null) compares = Comparer<K>.Default;
    //         Sort(sequence, compares, 0, sequence.Length - 1);
    //     }


    //     public void Sort<K>(K[] sequence, IComparer<K> compares, int a, int b) where K : IComparable<K>
    //     { 
    //         if (a >= b) return;

    //         int left = a;
    //         int right = b - 1;
    //         K pivot = sequence[b];
    //         K temp;

    //         while (left <= right)
    //         {
    //             while (left <= right && (compares.Compare(sequence[left], pivot) < 0)) left++;
    //             while (left <= right && (compares.Compare(sequence[right], pivot) > 0)) right--;

    //             if (left <= right)
    //             {
    //                 temp = sequence[left];
    //                 sequence[left] = sequence[right];
    //                 sequence[right] = temp;

    //                 left++; right--;
    //             }
    //         }

    //         temp = sequence[left];
    //         sequence[left] = sequence[b];
    //         sequence[b] = temp;

    //         Sort(sequence, compares, a, left - 1);
    //         Sort(sequence, compares, left + 1, b);
    //     }
    // }

    // class MergeSortTopDown : ISorter
    // {
    //     public MergeSortTopDown() { }

    //     public void Sort<K>(K[] sequence, IComparer<K> compares) where K : IComparable<K>
    //     {
    //         if (sequence == null) throw new ArgumentNullException();
    //         if (sequence.Length < 2) return;

    //         if (compares == null) compares = Comparer<K>.Default;

    //         int mipoint = (sequence.Length) / 2;

    //         K[] bottom = sequence[0..mipoint];
    //         K[] top = sequence[mipoint..sequence.Length];

    //         Sort(bottom, compares);
    //         Sort(top, compares);

    //         Merge(bottom, top, sequence, compares); 

    //     }

    //     public void Merge<K>(K[] bottom, K[] top, K[] sequence, IComparer<K> compares)
    //     {
    //         int a = 0; 
    //         int b = 0;

    //         while (a + b < sequence.Length)
    //         {
    //             if (b == top.Length || (a < bottom.Length && compares.Compare(bottom[a], top[b]) < 0 ))
    //             {
    //                 sequence[a+b] = bottom[a++];
    //             }
    //             else
    //             {
    //                 sequence[a+b] = top[b++];
    //             }
    //         }
    //     }
    // }

    // class MergeSortBottomUp : ISorter
    // {
    //     public void Sort<K>(K[] sequence, IComparer<K> compares) where K : IComparable<K>
    //     {
    //         if (sequence == null) throw new ArgumentNullException();
    //         if (sequence.Length < 2) return;

    //         if (compares == null) compares = Comparer<K>.Default;

    //         K[] temp;
    //         K[] holder = sequence;
    //         K[] End = new K[sequence.Length];


    //         for (int i = 1; i < sequence.Length; i *= 2)
    //         {
    //             for (int j = 0; j < sequence.Length - i; j += 2 * i)
    //             {
    //                 Merge(holder, End, compares, j, i);
    //             }
    //             temp = holder;
    //             holder = End; 
    //             End = temp;


    //         }
    //         if (sequence != holder) Array.Copy(holder, 0, sequence, 0, sequence.Length);


    //     }


    //     private void Merge<K>(K[] input, K[] output, IComparer<K> compares, int starting, int number)
    //     {


    //         int topEnd = Math.Min(starting + number, input.Length);
    //         int bottomEnd = Math.Min(starting + 2 * number, input.Length);

    //         int a = starting;
    //         int b = starting + number;
    //         int c = starting;

    //         while (a < topEnd && b < bottomEnd)
    //             if (compares.Compare(input[a], input[b]) < 0)
    //                 output[c++] = input[a++];
    //             else
    //                 output[c++] = input[b++];
    //         if (a < topEnd)
    //             Array.Copy(input, a, output, c, topEnd - a);
    //         else if (b < bottomEnd)
    //             Array.Copy(input, b, output, c, bottomEnd - b);
    //     }
    // }

}