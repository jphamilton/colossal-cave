using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net;

public class Rooms
{
    private static readonly List<Room> rooms = new();

    public static void Load(IStory story)
    {
        rooms.Clear();

        static void Add(Assembly a)
        {
            var r = a.GetTypes().Where(x => x.IsSubclassOf(typeof(Room)) && !x.IsAbstract).Select(x => (Room)Activator.CreateInstance(x)).ToList();
            rooms.AddRange(r);
        }

        Add(story.GetType().Assembly);
        Add(Assembly.GetExecutingAssembly());
    }

    public static IList<Room> All
    {
        get { return rooms; }
    }

    public static Object Get(Type type)
    {
        return rooms.Where(x => x.GetType() == type).FirstOrDefault();
    }

    public static T Get<T>() where T : Object
    {
        return (T)Get(typeof(T));
    }

    public static Room GetByName(string name)
    {
        return rooms.Where(x => x.Name == name || x.Synonyms.Contains(name)).FirstOrDefault();
    }

    public static IList<Room> WithName(string name)
    {
        return rooms.Where(x => x.Name == name || x.Synonyms.Contains(name)).ToList();
    }


}
