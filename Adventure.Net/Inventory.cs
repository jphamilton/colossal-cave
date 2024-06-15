using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net;

public static class Inventory
{
    private static Player Player => Objects.Get<Player>();

    // these can be replaced by games with more complicated rules for inventory
    public static Func<bool> CanAdd = () => Count < 8;
    
    public static void Add(Object obj)
    {
        // CanAdd is not called here because debugging verbs like Purloin will work no matter
        // how many objects are in inventory
        obj.Remove();
        obj.Parent = Player;
        Player.Children.Add(obj);
        obj.Touched = true;
    }

    public static Object Add<T>() where T : Object
    {
        Object obj = Objects.Get<T>();
        Add(obj);
        return obj;
    }

    public static bool Contains<T>() where T : Object
    {
        Object obj = Objects.Get<T>();
        return Contains(obj);
    }

    public static bool Contains(Object obj)
    {
        foreach (var o in Player.Children)
        {
            if (o is Container c && c.ContentsVisible && c.Children.Contains(obj))
            {
                return true;
            }
        }

        return Player.Children.Contains(obj);
    }

    public static bool Contains(string objName)
    {
        foreach (var obj in Player.Children)
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
            if (!Contains(obj))
            {
                return false;
            }
        }

        return true;
    }

    public static void Clear()
    {
        foreach(var obj in Player.Children.ToList())
        {
            obj.Remove();
        }
    }

    public static string Display()
    {
        if (Player.Children.Count == 0)
        {
            return "You are carrying nothing.";
        }

        StringBuilder sb = new();
        sb.AppendLine("You are carrying:");

        var containers = new List<Object>();

        foreach (var obj in Player.Children.OrderBy(x => x.Description))
        {
            if (obj is Container container && container.ContentsVisible)
            {
                containers.Add(container);
                sb.Append(DisplayContainer(container, 1));
            }
        }

        foreach (var obj in Player.Children.Where(x => !containers.Contains(x)).OrderBy(x => x.Description))
        {
            sb.Append($"\t{DisplayObject(obj)}");
        }

        return sb.ToString();
    }

    private static string DisplayObject(Object obj)
    {
        var asides = new List<string>();

        if (obj.Light && !Player.Location.Light)
        {
            asides.Add("providing light");
        }

        if (obj.Clothing && obj.Worn)
        {
            asides.Add("being worn");
        }

        if (obj is Container container && container.Openable)
        {
            string clause = asides.Count == 0 ? "which is " : "";

            if (container.Open)
            {
                asides.Add(container.Children.Count > 0 ? $"{clause}open" : $"{clause}open but empty");
            }
            else
            {
                asides.Add($"{clause}closed");
            }
        }

        var aside = asides.Count > 0 ? $" ({string.Join(" and ", asides)})" : "";

        return $"{obj.IName}{aside}\n";
    }

    private static string DisplayContainer(Container container, int level)
    {
        StringBuilder sb = new();

        sb.Indent(level);

        sb.Append(DisplayObject(container));

        if (container.Open || container.Transparent)
        {
            foreach (var child in container.Children)
            {
                if (child is Container c)
                {
                    sb.Append($"{DisplayContainer(c, level + 1)}");
                }
                else
                {
                    sb.Indent(level + 1);
                    sb.Append(DisplayObject(child));
                }
            }
        }

        return sb.ToString();
    }

    public static IList<Object> Items => Player.Children;

    public static int Count
    {
        get
        {
            return GetCount(Player.Children);
        }
    }

    public static bool ProvidingLight()
    {
        foreach (var obj in Items)
        {
            if (obj.Light || obj is Container container && container.ProvidingLight())
            {
                return true;
            }
        }
        
        return false;
    }

    private static int GetCount(IList<Object> objects)
    {
        var count = 0;

        // This includes contained items in count. Inform 6 doesn't seem to do this,
        // so the wicker cage in Colossal Cave can be used as a bag of holding.
        foreach(var obj in objects)
        {
            count += GetCount(obj.Children) + 1;
        }

        return count;
    }
}
