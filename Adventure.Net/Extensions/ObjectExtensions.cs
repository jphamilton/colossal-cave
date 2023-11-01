using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net.Extensions;

public static class ObjectExtensions
{
    public static bool Is<T>(this Object obj) where T : Object
    {
        return obj != null && obj is T;
    }

    public static bool IsNot<T>(this Object obj) where T : Object
    {
        return obj != null && obj is not T;
    }

    public static string DisplayList(this IList<Object> objects, bool definiteArticle = true, string concat = "and")
    {
        string article(Object x) => definiteArticle ? x.DefiniteArticle : x.IndefiniteArticle;
        var list = objects.Select(x => $"{article(x)} {x.Name}").ToList();
        
        return list.Count < 3
            ? string.Join($" {concat} ", list)
            : string.Join(", ", list.GetRange(0, list.Count - 1)) + $" {concat} {list.Last()}";
    }
}
