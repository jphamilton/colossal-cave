﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net;


public static class Objects
{
    // all game objects
    private static readonly IList<Object> objects = new List<Object>();

    public static void Load(IStory story)
    {
        objects.Clear();

        Assembly ax = story.GetType().Assembly;
        
        foreach (var type in ax.GetTypes())
        {
            if (type.IsSubclassOf(typeof(Object)) && !type.IsAbstract && Activator.CreateInstance(type) is Object obj)
            {
                objects.Add(obj);
            }
        }

        objects.Add(new InventoryRoot());
    }

    public static IList<Object> All => objects;

    public static Object GetByName(string name)
    {
        return objects.SingleOrDefault(x => x.Name == name || x.Synonyms.Contains(name));
    }

    public static IList<Object> WithName(string name)
    {
        return objects.Where(x => x.Name == name || x.Synonyms.Contains(name)).ToList();
    }

    public static T Get<T>() where T : Object
    {
        return Get(typeof(T)) as T;
    }

    private static Object Get(Type objectType)
    {
        foreach (var obj in objects)
        {
            Type objType = obj.GetType();
            if (objType == objectType)
            {
                return obj;
            }
        }

        return null;
    }

    public static IList<Object> WithRunningDaemons()
    {
        return objects.Where(x => x.Daemon != null && x.DaemonStarted).ToList();
    }

    /// <summary>
    /// This should only be used for testing
    /// </summary>
    /// <param name="obj"></param>
    public static void Add(Object obj, Room room = null)
    {
        objects.Add(obj);

        if (room != null)
        {
            ObjectMap.Add(obj, room);
        }
    }
}
