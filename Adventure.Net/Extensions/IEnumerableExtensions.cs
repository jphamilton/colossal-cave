using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net.Extensions;

public static class IEnumerableExtensions
{
    public static IEnumerable<(T item, int index)> ForEach<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        int index = 0;

        foreach (var item in source)
        {
            action(item, index);
            index++;
        }
    }
}
