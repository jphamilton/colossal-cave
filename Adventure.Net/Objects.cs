using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public class Objects
    {
        private static IList<Object> objects = new List<Object>();

        public static void Load(IStory story)
        {
            objects.Clear();

            Assembly ax = story.GetType().Assembly;
            Type[] types = ax.GetTypes();

            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof (Object)) && !type.IsAbstract && !type.IsSubclassOf(typeof (Room)))
                {
                    var obj = Activator.CreateInstance(type) as Object;
                    if (obj != null)
                        objects.Add(obj);
                }
            }

        }

        public static IList<Object> All
        {
            get { return objects; }
        }

        public static Object GetByName(string name)
        {
            return objects.SingleOrDefault(x => x.Name == name || x.Synonyms.Contains(name));
        }

        public static IList<Object> WithName(string name)
        {
            return objects.Where(x => x.Name == name || x.Synonyms.Contains(name)).ToList();
        }

        public static T Get<T>() where T:Object
        {
            return Get(typeof (T)) as T;
        }

        public static Object Get(Type objectType)
        {
            foreach (var obj in objects)
            {
                Type objType = obj.GetType();
                if (objType == objectType)
                    return obj;
            }

            return null;
        }

        public static IList<Object> WithRunningDaemons()
        {
            return objects.Where(x => x.Daemon != null && x.DaemonStarted == true).ToList();
        }

        public static void Add(Object obj)
        {
            objects.Add(obj);
        }
    }
}
