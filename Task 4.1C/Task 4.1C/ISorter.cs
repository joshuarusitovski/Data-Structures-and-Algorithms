using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_4._1C
{
    public interface ISorter
    {
        void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>;
    }
}