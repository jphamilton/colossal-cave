using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net.Extensions;

public static class ObjectExtensions
{
    public static string DisplayList(this IList<Object> objects, bool definiteArticle = true, string concat = "and")
    {
        string article(Object x) => definiteArticle ? x.DefiniteArticle : x.IndefiniteArticle;
        var list = objects.Select(x => $"{article(x)} {x.Name}").ToList();
        return list.Join(concat);
    }
}
