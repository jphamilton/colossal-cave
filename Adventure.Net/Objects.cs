using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{

    public class Objects
    {
        private static readonly IList<Object> items = new List<Object>();

        public static void Load(IStory story)
        {
            items.Clear();

            Assembly ax = story.GetType().Assembly;
            Type[] types = ax.GetTypes();

            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(Object)) && 
                    !type.IsAbstract && 
                    !type.IsSubclassOf(typeof(Room)))
                {
                    if (Activator.CreateInstance(type) is Object obj)
                    {
                        items.Add(obj);
                    }
                }
            }

        }

        internal static IList<Object> All
        {
            get { return items; }
        }

        public static Object GetByName(string name)
        {
            return items.SingleOrDefault(x => x.Name == name || x.Synonyms.Contains(name));
        }

        public static IList<Object> WithName(string name)
        {
            return items.Where(x => x.Name == name || x.Synonyms.Contains(name)).ToList();
        }

        public static T Get<T>() where T:Object
        {
            return Get(typeof (T)) as T;
        }

        private static Object Get(Type objectType)
        {
            foreach (var obj in items)
            {
                Type objType = obj.GetType();
                if (objType == objectType)
                    return obj;
            }

            return null;
        }

        public static IList<Object> WithRunningDaemons()
        {
            return items.Where(x => x.Daemon != null && x.DaemonStarted == true).ToList();
        }

        /// <summary>
        /// This should only be used for testing
        /// </summary>
        /// <param name="obj"></param>
        public static void Add(Object obj, Room room = null)
        {
            items.Add(obj);

            if (room != null)
            {
                ObjectMap.Add(obj, room);
            }
        }
    }
}
