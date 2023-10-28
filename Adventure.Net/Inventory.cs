using Adventure.Net.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net;

public class Inventory
{
    private static readonly List<Object> objects = new();

    public static void Add(Object obj)
    {
        objects.Add(obj);
        obj.Touched = true;
    }

    public static bool Contains<T>() where T : Object
    {
        Object obj = Objects.Get<T>();
        return Contains(obj);
    }

    public static bool Contains(Object obj)
    {
        foreach (var o in objects)
        {
            if (o is Container c && c.Contents.Contains(obj))
            {
                return true;
            }
        }

        return objects.Contains(obj);
    }

    public static bool Contains(string objName)
    {
        foreach (var obj in objects)
        {
            if (obj.Name == objName || obj.Synonyms.Contains(objName))
            {
                return true;
            }
        }

        return false;
    }

    public static bool Contains(params Object[] args)
    {
        if (args.Length == 0)
        {
            return false;
        }

        foreach (Object obj in args)
        {
            if (!objects.Contains(obj))
            {
                return false;
            }
        }

        return true;
    }

    public static void Remove(Object obj)
    {
        objects.Remove(obj);
    }

    public static void Clear()
    {
        objects.Clear();
    }

    public static string Display()
    {
        if (objects.Count == 0)
        {
            return "You are carrying nothing.";
        }

        StringBuilder sb = new();
        sb.AppendLine("You are carrying:");

        var containers = new List<Object>();

        foreach (var obj in objects.OrderBy(x => x.Description))
        {
            if (obj is Container container)
            {
                containers.Add(container);
                sb.Append(DisplayContainer(container, 1));
            }
        }

        foreach (var obj in objects.Where(x => !containers.Contains(x)).OrderBy(x => x.Description))
        {
            sb.Append($"\t{obj.IndefiniteArticle} {obj.Name}\n");
        }

        return sb.ToString();
    }

    private static string DisplayContainer(Container container, int level)
    {
        StringBuilder sb = new();

        sb.Indent(level);

        if (container.Openable)
        {
            sb.Append($"{container.IndefiniteArticle} {container.Name} ({container.State})\n");
        }
        else
        {
            sb.Append($"{container.IndefiniteArticle} {container.Name}\n");
        }

        if (container.Open || container.Transparent)
        {
            foreach (var child in container.Contents)
            {
                if (child is Container c)
                {
                    sb.Append($"{DisplayContainer(c, level + 1)}");
                }
                else
                {
                    sb.Indent(level + 1);
                    sb.Append($"{child.IndefiniteArticle} {child.Name}\n");
                }
            }
        }

        return sb.ToString();
    }

    public static List<Object> Items => objects;

    public static int Count
    {
        get { return objects.Count; }
    }

    public static T Get<T>() where T : Object
    {
        return (T)objects.Where(x => x.GetType() == typeof(T)).FirstOrDefault();
    }
}
