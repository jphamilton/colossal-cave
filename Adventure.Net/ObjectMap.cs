using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;

public static class ObjectMap
{
    // why is this random comment here???
    // CONTAINERS?????

    private static readonly IDictionary<Object, IList<Room>> ObjectToRooms = new Dictionary<Object, IList<Room>>();
    private static readonly IDictionary<Room, IList<Object>> RoomToObjects = new Dictionary<Room, IList<Object>>();

    public static void Add(Object obj, Room room)
    {
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
        var objects = GetObjects(room);
        return objects.Contains(obj);
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
            return RoomToObjects[room].Where(x => !x.Absent).ToList();
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
