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
        Output.PrintLine();

        bool isLit = IsLit();

        Room room = isLit ? Player.Location : Rooms.Get<Darkness>();

        Output.Bold(room.Name);

        if (showFull || !Player.Location.Visited || Context.Story.Verbose)
        {
            Output.Print(room.Description);
        }

        if (isLit)
        {
            DisplayRoomObjects();
        }
    }

    public static void Look(Room originalRoom, bool wasLit)
    {
        // player was in darkness, did not move, and light source was turned on
        if (!wasLit && IsLit() && Player.Location == originalRoom)
        {
            Look(true);
            return;
        }

        // player had light, did not move, and light source was turned off
        if (wasLit && Player.Location == originalRoom && !IsLit())
        {
            Output.Print("\nIt is now pitch dark in here!");
        }
    }

    public static bool IsLit()
    {
        var tree = new ObjectTree(Player.Location, false);
        return tree.Any(x => x.Light);
    }

    private static void DisplayRoomObjects()
    {
        var ordinary = new List<Object>();
        int total = 0;

        foreach (var obj in ObjectMap.GetObjects(Player.Location))
        {
            DisplaySupporter(obj);

            if (TryDisplayScenery(obj))
            {
                continue;
            }

            if (TryDisplayStatic(obj))
            {
                continue;
            }

            total++;

            if (!DisplayInitialOrDescribe(obj))
            {
                ordinary.Add(obj);
            }
        }

        DisplayAdditionalObjects(ordinary, total);
    }

    private static bool DisplayInitialOrDescribe(Object obj)
    {
        if (!obj.Touched && !string.IsNullOrEmpty(obj.InitialDescription))
        {
            Output.PrintLine();
            Output.Print(obj.InitialDescription);
            return true;
        }
        else if (obj.Describe != null && obj is not Container)
        {
            var describe = obj.Describe();
            if (describe != null)
            {
                Output.PrintLine();
                Output.Print(describe);
            }
            
            return true;
        }

        return false;
    }

    private static bool TryDisplayScenery(Object obj)
    {
        if (obj.Scenery && obj.Describe == null)
        {
            return true;
        }

        return false;
    }

    private static bool TryDisplayStatic(Object obj)
    {
        if (obj.Static)
        {
            if (obj is Door door && door.TryDisplay(out string doorMessage))
            {
                Output.Print(doorMessage);
                return true;
            }

            if (obj.Describe == null && obj.InitialDescription == null)
            {
                return true;
            }
        }

        return false;
    }

    private static void DisplaySupporter(Object obj)
    {
        if (obj is Supporter supporter && supporter.TryDisplay(out string supporterMessage))
        {
            Output.PrintLine();
            Output.Print(supporterMessage);
        }
    }

    private static void DisplayAdditionalObjects(List<Object> objects, int total)
    {
        if (objects.Count == 0)
        {
            return;
        }

        var group = new StringBuilder();

        if (total > objects.Count)
        {
            group.Append("You can also see ");
        }
        else
        {
            group.Append("You can see ");
        }

        for (int i = 0; i < objects.Count; i++)
        {
            Object obj = objects[i];

            if (i == objects.Count - 1 && i > 0)
            {
                group.Append(" and ");
            }
            else if (i > 0)
            {
                group.Append(", ");
            }

            if(obj is Container container && container.TryDisplay(out string containerMessage))
            {
                group.Append(containerMessage);
            }
            else
            {
                group.Append($"{obj.IName}");
            }

        }

        group.Append(" here.");

        Output.PrintLine();
        Output.Print(group.ToString());
    }

    public static List<Object> ObjectsInScope(bool includeAbsent = true)
    {
        var tree = new ObjectTree(Player.Location, includeAbsent);
        var objects = tree.GetObjects(out bool lit);

        objects = [.. objects.Where(x => x != Player.Location)];

        if (!lit)
        {
            objects = [.. objects.Where(Inventory.Contains)];
        }

        return objects;
    }

    public static bool Has<T>() where T : Object
    {
        var objects = ObjectMap.GetObjects(Player.Location);

        return objects.Any(obj => obj is T);
    }
}
