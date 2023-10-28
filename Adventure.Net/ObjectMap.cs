using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;

public static class ObjectMap
{
    // why is this random comment here???
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

        if (!ObjectToRooms[obj].Contains(room))
        {
            ObjectToRooms[obj].Add(room);
        }

        if (!RoomToObjects.ContainsKey(room))
        {
            RoomToObjects.Add(room, new List<Object>());
        }

        if (!RoomToObjects[room].Contains(obj))
        {
            RoomToObjects[room].Add(obj);
        }
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

                obj.AbsentToggled = (absent) =>
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

        if (obj != null)
        {
            Inventory.Remove(obj);
        }
    }

    public static T Remove<T>() where T : Object
    {
        var obj = Objects.Get<T>();
        Remove(obj);
        return obj;
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

    public static IList<Room> Location(Object obj)
    {
        if (ObjectToRooms.ContainsKey(obj))
        {
            return ObjectToRooms[obj];
        }

        return new List<Room>();
    }
}
