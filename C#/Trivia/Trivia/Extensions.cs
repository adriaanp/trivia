using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }
    }
}
