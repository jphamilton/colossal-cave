using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public class Objects
    {
        private static IList<Item> items = new List<Item>();

        public static void Load(IStory story)
        {
            items.Clear();

            Assembly ax = story.GetType().Assembly;
            Type[] types = ax.GetTypes();

            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(Item)) && !type.IsAbstract && !type.IsSubclassOf(typeof(Room)))
                {
                    var obj = Activator.CreateInstance(type) as Item;
                    if (obj != null)
                    {
                        items.Add(obj);
                    }
                }
            }

        }

        public static IList<Item> All
        {
            get { return items; }
        }

        public static Item GetByName(string name)
        {
            return items.SingleOrDefault(x => x.Name == name || x.Synonyms.Contains(name));
        }

        public static IList<Item> WithName(string name)
        {
            return items.Where(x => x.Name == name || x.Synonyms.Contains(name)).ToList();
        }

        public static T Get<T>() where T:Item
        {
            return Get(typeof (T)) as T;
        }

        private static Item Get(Type objectType)
        {
            foreach (var obj in items)
            {
                Type objType = obj.GetType();
                if (objType == objectType)
                    return obj;
            }

            return null;
        }

        public static IList<Item> WithRunningDaemons()
        {
            return items.Where(x => x.Daemon != null && x.DaemonStarted == true).ToList();
        }

        public static void Add(Item obj)
        {
            items.Add(obj);
        }
    }
}
