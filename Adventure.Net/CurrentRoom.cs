using Adventure.Net.Extensions;
using Adventure.Net.Places;
using Adventure.Net.Things;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net;

public static class CurrentRoom
{
    public static void Look(bool showFull)
    {
        // look is special in that it uses extra formatting like bold,
        // so this is sent directly to the console

        Output.PrintLine();

        Room room = IsLit() ? Player.Location : Rooms.Get<Darkness>();

        Output.Bold(room.Name);

        if (showFull || !Player.Location.Visited || Context.Story.Verbose)
        {
            Output.Print(room.Description);
        }

        DisplayRoomObjects();

    }

    public static bool IsLit()
    {
        if (Player.Location.Light)
        {
            return true;
        }

        // objects in room
        foreach (var obj in ObjectMap.GetObjects(Player.Location))
        {
            if (obj.Light)
            {
                return true;
            }

            // light is on a supporter - e.g lamp is on a table
            if (obj is Supporter supporter)
            {
                foreach (var supported in obj.Children)
                {
                    if (supported.Light)
                    {
                        return true;
                    }
                }
            }

            // light is in an open or transparent container
            if (obj is Container container && container.ContentsVisible)
            {
                foreach (var contained in container.Children)
                {
                    if (contained.Light)
                    {
                        return true;
                    }
                }
            }
        }

        // objects being carried
        foreach (var obj in Inventory.Items)
        {
            if (obj.Light)
            {
                return true;
            }

            // light is in a container - e.g. lamp is in the wicker cage
            if (obj is Container container && container.ContentsVisible)
            {
                foreach (var contained in container.Children)
                {
                    if (contained.Light)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    // TODO: this needs a good refactor
    private static void DisplayRoomObjects()
    {
        if (!IsLit())
        {
            return;
        }

        var ordinary = new List<Object>();
        int total = 0;

        var objects = ObjectMap.GetObjects(Player.Location);

        foreach (var obj in objects)
        {
            if (obj is Supporter supporter && supporter.Children.Count > 0)
            {
                Output.PrintLine();
                Output.Print(DisplaySupporter(supporter));
            }

            if (obj.Scenery && obj.Describe == null)
            {
                continue;
            }

            if (obj.Static)
            {
                if (obj is Door door)
                {
                    var doorDisplay = door.Display();
                    if (doorDisplay != null)
                    {
                        Output.Print(doorDisplay);
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
                group.Append(DisplayContainer(container));
            }
            else
            {
                group.Append($"{obj.IndefiniteArticle} {obj.Name}");
            }

        }

        group.Append(" here.");

        if (ordinary.Count > 0)
        {
            Output.PrintLine();
            Output.Print(group.ToString());
        }
    }

    private static string DisplayContainer(Container container)
    {
        var contentsVisible = container.Open || container.Transparent;

        if (contentsVisible)
        {
            if (container.Children.Count > 0)
            {
                return $"{container.IndefiniteArticle} {container.Name} (which contains {container.Children.DisplayList(false)})";
            }

            return $"{container.IndefiniteArticle} {container.Name} (which is empty)";
        }

        return $"{container.IndefiniteArticle} {container.Name} (which is closed)";
    }

    private static string DisplaySupporter(Supporter supporter)
    {
        if (supporter.Children.Count > 0)
        {
            var isAre = supporter.Children.Count == 1 ? "is" : "are";
            return $"On {supporter.DefiniteArticle} {supporter.Name} {isAre} {supporter.Children.DisplayList(definiteArticle: false)}.";
        }

        // for now supporters are static/scenic so don't show anything extra if nothing is on them
        return null;
    }

    public static IList<Object> ObjectsInRoom()
    {
        var result = new List<Object>();
        var objects = ObjectMap.GetObjects(Player.Location);

        result.AddRange(objects.Where(x => !x.Scenery && !x.Static));
        result.AddRange(objects.Where(x => x.Scenery || x.Static));
        AddContained(result);
        AddSupported(result);
        return result;
    }

    public static List<Object> ObjectsInScope()
    {
        var result = new List<Object>();

        var objects = ObjectMap.GetObjects(Player.Location);

        if (IsLit())
        {
            result.AddRange(objects.Where(x => !x.Scenery && !x.Static));
            result.AddRange(objects.Where(x => x.Scenery || x.Static));
        }

        result.AddRange(Inventory.Items);

        // note: location is added to scope to support things like Door
        result.Add(Player.Location);

        // add objects in containers
        AddContained(result);

        // add objects sitting on things
        AddSupported(result);

        return result;
    }

    private static void AddContained(List<Object> objects)
    {
        var contained = new List<Object>();

        foreach (var obj in objects)
        {
            if (obj is Container container && container.ContentsVisible)
            {
                contained.AddRange(container.Children);
            }
        }

        objects.AddRange(contained);
    }

    private static void AddSupported(List<Object> objects)
    {
        var supported = new List<Object>();

        foreach (var obj in objects.Where(x => x is Supporter))
        {
            supported.AddRange(obj.Children);
        }

        objects.AddRange(supported);
    }

    public static bool Has<T>() where T : Object
    {
        var objects = ObjectMap.GetObjects(Player.Location);

        return objects.Any(obj => obj is T);
    }
}
