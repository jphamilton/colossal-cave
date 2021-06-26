using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public static class ObjectMap
    {
        // CONTAINERS?????

        private static readonly IDictionary<Object, IList<Room>> ObjectToRooms = new Dictionary<Object, IList<Room>>();
        private static readonly IDictionary<Room, IList<Object>> RoomToObjects = new Dictionary<Room, IList<Object>>();
        private static readonly IDictionary<Object, IList<Room>> Absent = new Dictionary<Object, IList<Room>>();

        public static void Add(Object obj, Room room)
        {
            if (obj.Absent)
            {
                HandleAbsent(obj, room);
                return;
            }

            if (!ObjectToRooms.ContainsKey(obj))
            {
                ObjectToRooms.Add(obj, new List<Room>());
            }

            ObjectToRooms[obj].Add(room);

            if (!RoomToObjects.ContainsKey(room))
            {
                RoomToObjects.Add(room, new List<Object>());
            }

            RoomToObjects[room].Add(obj);
        }

        private static void HandleAbsent(Object obj, Room room)
        {
            void addAbsent()
            {
                if (Absent.ContainsKey(obj))
                {
                    Absent[obj].Add(room);
                }
                else
                {
                    Absent.Add(obj, new List<Room> { room });

                    obj.AbsentToggled += (absent) =>
                    {
                        if (absent)
                        {
                            Remove(obj);

                            if (!Absent.ContainsKey(obj))
                            {
                                addAbsent();
                            }
                        }
                        else
                        {
                            if (Absent.ContainsKey(obj))
                            {
                                var rooms = Absent[obj];

                                foreach (var r in rooms)
                                {
                                    Add(obj, r);

                                }
                            }
                        }
                    };
                }
            }

            addAbsent();

        }

        /// <summary>
        /// remove item from object mapping
        /// </summary>
        /// <param name="obj"></param>
        public static void Remove(Object obj)
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

        public static T Remove<T>() where T : Object
        {
            var obj = ObjectToRooms.Keys.SingleOrDefault(x => x.GetType() == typeof(T));

            Remove(obj);

            return (T)obj;
        }

        public static bool Contains(Room room, Object obj)
        {
            if (RoomToObjects.ContainsKey(room))
            {
                return RoomToObjects[room].Contains(obj);
            }

            return false;
        }


        public static void MoveObject(Object obj, Room to)
        {
            Remove(obj);
            Add(obj, to);
        }

        public static IReadOnlyList<Object> GetObjects(Room room)
        {
            if (RoomToObjects.ContainsKey(room))
            {
                return (IReadOnlyList<Object>)RoomToObjects[room];
            }

            return new List<Object>();
        }

            
    }

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
