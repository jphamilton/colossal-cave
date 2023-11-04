using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net.Extensions;

public static class ListStringExtensions
{
    public static string Join(this List<string> list, string concat = "and")
    {
        return list.Count < 3
            ? string.Join($" {concat} ", list)
            : string.Join(", ", list.GetRange(0, list.Count - 1)) + $" {concat} {list.Last()}";
    }
}
