using System;
using System.Collections.Generic;
using System.Reflection;

namespace Adventure.Net
{
    public static class AssemblyExtensions
    {
        public static IList<T> SubclassOf<T>(this Assembly ax) where T : class
        {
            List<T> result = new List<T>();

            Type[] types = ax.GetTypes();

            foreach (Type type in types)
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(Room)))
                {
                    T obj = Activator.CreateInstance(type) as T;
                    result.Add(obj);
                }
            }

            return result;
        }
    }
}
