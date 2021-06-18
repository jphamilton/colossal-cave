using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public static class ObjectMap
    {
        // CONTAINERS?????

        private static readonly IDictionary<Item, IList<Room>> ObjectToRooms = new Dictionary<Item, IList<Room>>();
        private static readonly IDictionary<Room, IList<Item>> RoomToObjects = new Dictionary<Room, IList<Item>>();

        // get rid of Has<>
        // FoundIn<>
        
        public static void Add(Item obj, Room room)
        {
            if (!ObjectToRooms.ContainsKey(obj))
            {
                ObjectToRooms.Add(obj, new List<Room>());
            }

            ObjectToRooms[obj].Add(room);

            if (!RoomToObjects.ContainsKey(room))
            {
                RoomToObjects.Add(room, new List<Item>());
            }

            RoomToObjects[room].Add(obj);
        }

        /// <summary>
        /// remove item from object mapping
        /// </summary>
        /// <param name="obj"></param>
        public static void Remove(Item obj)
        {
            // item may not necessarily be anywhere 
            if (ObjectToRooms.TryGetValue(obj, out IList<Room> rooms))
            {
                foreach (var room in rooms)
                {
                    RoomToObjects[room].Remove(obj);
                }

                ObjectToRooms.Remove(obj);
            }
        }

        public static T Remove<T>() where T : Item
        {
            var obj = ObjectToRooms.Keys.SingleOrDefault(x => x.GetType() == typeof(T));

            Remove(obj);

            return (T)obj;
        }

        public static bool Contains(Room room, Item obj)
        {
            return RoomToObjects[room].Contains(obj);
        }


        public static void MoveObject(Item obj, Room to)
        {
            Remove(obj);
            Add(obj, to);
        }

        public static IReadOnlyList<Item> GetObjects(Room room)
        {
            return (IReadOnlyList<Item>)RoomToObjects[room];
        }

            
    }

    public class Objects
    {
        private static readonly IList<Item> items = new List<Item>();

        public static void Load(IStory story)
        {
            items.Clear();

            Assembly ax = story.GetType().Assembly;
            Type[] types = ax.GetTypes();

            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(Item)) && 
                    !type.IsAbstract && 
                    !type.IsSubclassOf(typeof(Room)))
                {
                    if (Activator.CreateInstance(type) is Item obj)
                    {
                        items.Add(obj);
                    }
                }
            }

        }

        internal static IList<Item> All
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

        /// <summary>
        /// This should only be used for testing
        /// </summary>
        /// <param name="obj"></param>
        public static void Add(Item obj, Room room = null)
        {
            items.Add(obj);

            if (room != null)
            {
                ObjectMap.Add(obj, room);
            }
        }
    }
}
