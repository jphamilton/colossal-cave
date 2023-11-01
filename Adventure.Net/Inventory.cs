﻿using Adventure.Net.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net;

public static class Inventory
{
    private static InventoryRoot root;

    private static InventoryRoot Inv
    {
        get
        {
            root ??= Objects.Get<InventoryRoot>();
            return root;
        }
    }

    public static void Add(Object obj)
    {
        Inv.Children.Add(obj);
        obj.Touched = true;
    }

    public static bool Contains<T>() where T : Object
    {
        Object obj = Objects.Get<T>();
        return Contains(obj);
    }

    public static bool Contains(Object obj)
    {
        foreach (var o in Inv.Children)
        {
            if (o is Container c && c.Contents.Contains(obj))
            {
                return true;
            }
        }

        return Inv.Children.Contains(obj);
    }

    public static bool Contains(string objName)
    {
        foreach (var obj in Inv.Children)
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
            if (!Inv.Children.Contains(obj))
            {
                return false;
            }
        }

        return true;
    }

    public static void Remove(Object obj)
    {
        Inv.Children.Remove(obj);
    }

    public static void Clear()
    {
        Inv.Children.Clear();
    }

    public static string Display()
    {
        if (Inv.Children.Count == 0)
        {
            return "You are carrying nothing.";
        }

        StringBuilder sb = new();
        sb.AppendLine("You are carrying:");

        var containers = new List<Object>();

        foreach (var obj in Inv.Children.OrderBy(x => x.Description))
        {
            if (obj is Container container)
            {
                containers.Add(container);
                sb.Append(DisplayContainer(container, 1));
            }
        }

        foreach (var obj in Inv.Children.Where(x => !containers.Contains(x)).OrderBy(x => x.Description))
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

    public static IList<Object> Items => Inv.Children;

    public static int Count
    {
        get { return Inv.Children.Count; }
    }

}
