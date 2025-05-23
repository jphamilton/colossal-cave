using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Adventure.Net;

public static class Objects
{
    // all game objects
    private static List<Object> _objects = [];

    public static void Load(Story story)
    {
        _objects = [];//.Clear();
        int id = 0;

        void Add(IList<Type> types)
        {
            foreach (var type in types)
            {
                if (Activator.CreateInstance(type) is Object obj)
                {
                    obj.Parent = null;
                    obj.Id = ++id;
                    
                    if (!All.Contains(obj))
                    {
                        _objects.Add(obj);
                    }
                    
                }
            }
        }

        Assembly ax = typeof(Objects).Assembly;
        
        Add(ax.GetObjectTypes());
            
        ax = story.GetType().Assembly;

        Add(ax.GetObjectTypes());
    }

    public static List<Object> All => _objects;

    public static T Get<T>() where T : Object
    {
        return (T)_objects.Single(x => x is T);
    }

    /// <summary>
    /// This should only be used for testing
    /// </summary>
    /// <param name="obj"></param>
    public static void Add(Object obj, Room room = null)
    {
        _objects.Add(obj);

        if (room != null)
        {
            ObjectMap.Add(obj, room);
        }
    }
}
