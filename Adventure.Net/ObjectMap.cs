﻿using Adventure.Net.Things;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;


public static class ObjectMap
{
    public static void Add(Object obj, Room room)
    {
        obj.Parent = room;
        room.Children.Add(obj);
    }

    public static Object Remove(Object obj)
    {
        if (obj == null)
        {
            return null;
        }

        obj.Parent?.Children.Remove(obj);

        obj.Parent = null;

        return obj;
    }

    public static T Remove<T>() where T : Object
    {
        return (T)Remove(Objects.Get<T>());
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
        return [.. room.Children.Where(x => !x.Absent && x is not Player)];
    }

    public static Room Location(Object obj)
    {
        var parent = obj.Parent;

        while (parent != null)
        {
            if (parent is Room)
            {
                return parent as Room;
            }

            parent = parent.Parent;
        }

        return null;
    }
}
