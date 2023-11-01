using Adventure.Net.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net;

public static class CurrentRoom
{
    public static Room Location
    {
        get { return Context.Story.Location; }
    }

    public static void Look(bool showFull)
    {
        // look is special in that it uses extra formatting like bold,
        // so this is sent directly to the console

        Output.PrintLine();

        Room room = IsLit() ? Location : Rooms.Get<Darkness>();

        Output.Bold(room.Name);

        if (showFull || !Location.Visited)
        {
            Output.Print(room.Description);
        }

        DisplayRoomObjects();

    }

    public static bool IsLit()
    {
        if (Location.Light)
        {
            return true;
        }

        var objects = ObjectMap.GetObjects(Location);

        if (objects.Any(obj => obj.Light))
        {
            return true;
        }

        foreach (var obj in Inventory.Items)
        {
            if (obj.Light)
            {
                return true;
            }

            if (obj is not Container container || (!container.Open && !container.Transparent))
            {
                continue;
            }

            foreach (var containedObj in container.Children)
            {
                if (containedObj.Light)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static void DisplayRoomObjects()
    {
        if (!IsLit())
        {
            return;
        }

        var ordinary = new List<Object>();
        int total = 0;

        var objects = ObjectMap.GetObjects(Location);

        foreach (var obj in objects)
        {
            if (obj.Scenery && obj.Describe == null)
            {
                continue;
            }

            if (obj.Static)
            {
                if (obj is Door door)
                {
                    if (door.WhenOpen != null && door.Open)
                    {
                        Output.Print($"\n{door.WhenOpen}");
                        continue;
                    }
                    else if (door.WhenClosed != null && !door.Open)
                    {
                        Output.Print($"\n{door.WhenClosed}");
                        continue;
                    }
                }

                if (obj.Describe == null && obj.InitialDescription == null)
                {
                    continue;
                }
            }

            total++;

            if (!obj.Touched && !string.IsNullOrEmpty(obj.InitialDescription))
            {
                Output.PrintLine();
                Output.Print(obj.InitialDescription);
            }
            else if (obj.Describe != null && obj is not Container)
            {
                Output.PrintLine();
                Output.Print(obj.Describe());
            }
            else
            {
                ordinary.Add(obj);
            }
        }

        var group = new StringBuilder();

        if (total > ordinary.Count)
        {
            group.Append("You can also see ");
        }
        else
        {
            group.Append("You can see ");
        }

        for (int i = 0; i < ordinary.Count; i++)
        {
            Object obj = ordinary[i];

            if (i == ordinary.Count - 1 && i > 0)
            {
                group.Append(" and ");
            }
            else if (i > 0)
            {
                group.Append(", ");
            }

            if (obj is Container container)
            {
                if (container.Children.Count > 0)
                {
                    group.Append($"{obj.IndefiniteArticle} {obj.Name} (which contains {container.Children.DisplayList(definiteArticle: false)})");
                }
                else
                {
                    group.Append($"{obj.IndefiniteArticle} {obj.Name} (which is empty)");
                }
            }
            else
            {
                group.Append($"{obj.IndefiniteArticle} {obj.Name} (which is empty)");
            }

        }

        group.Append(" here.");

        if (ordinary.Count > 0)
        {
            Output.PrintLine();
            Output.Print(group.ToString());
        }
    }

    private static void AddContained(List<Object> objects)
    {
        var contained = new List<Object>();

        foreach (var obj in objects)
        {
            if (obj is Container container)
            {
                contained.AddRange(container.Children);
            }
        }

        objects.AddRange(contained);
    }

    public static IList<Object> ObjectsInRoom()
    {
        var result = new List<Object>();
        var objects = ObjectMap.GetObjects(Location);

        result.AddRange(objects.Where(x => !x.Scenery && !x.Static));
        result.AddRange(objects.Where(x => x.Scenery || x.Static));
        AddContained(result);
        return result;
    }

    public static List<Object> ObjectsInScope()
    {
        var result = new List<Object>();

        var objects = ObjectMap.GetObjects(Location);

        result.AddRange(objects.Where(x => !x.Scenery && !x.Static));
        result.AddRange(objects.Where(x => x.Scenery || x.Static));

        result.AddRange(Inventory.Items);

        // note: location is added to scope to support things like Door
        result.Add(Location);

        AddContained(result);

        return result;
    }

    public static bool Is<T>() where T : Room
    {
        return Location.GetType() == typeof(T);
    }

    public static bool Has<T>() where T : Object
    {
        var objects = ObjectMap.GetObjects(Location);

        return objects.Any(obj => obj is T);
    }
}
