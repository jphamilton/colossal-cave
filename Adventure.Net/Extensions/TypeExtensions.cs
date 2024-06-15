using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net.Extensions;

public static class TypeExtensions
{
    public static bool IsObject(this Type type)
    {
        return type.IsSubclassOf(typeof(Object)) && type.IsPublic && !type.IsAbstract;
    }

    public static IList<Type> GetObjectTypes(this Assembly ax)
    {
        return ax.GetTypes().Where(t => t.IsObject()).ToList();
    }
}
