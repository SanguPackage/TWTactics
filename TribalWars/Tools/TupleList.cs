using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TribalWars.Tools
{
    /// <summary>
    /// Allow use of collection initializers for list of tuples
    /// </summary>
    public class TupleList<T1, T2> : List<Tuple<T1, T2>>
    {
        public void Add(T1 item, T2 item2)
        {
            Add(new Tuple<T1, T2>(item, item2));
        }
    }
}
