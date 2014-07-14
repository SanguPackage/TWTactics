using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TribalWars.Tools
{
    public static class Linqs
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }
    }
}
