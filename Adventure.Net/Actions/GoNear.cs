using Adventure.Net.Things;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net.Actions;

// useful for testing
public class GoNear : ResolveObjects
{
    public GoNear()
    {
        Name = "gonear";
        InScopeOnly = false;
    }

    public override bool Handle(List<Object> objects)
    {
        var obj = objects[0];

        if (objects.Count > 1)
        {
            // favor room
            var rooms = objects.Where(x => x is Room).ToList();
            if (rooms.Count == 1)
            {
                obj = rooms[0];
                Print($"({obj.DefiniteArticle} {obj.Name})");
            }
        }

        var room = FindRoom(obj);

        if (room != null)
        {
            Player.Location = room;
            CurrentRoom.Look(true);
            return true;
        }

        return false;
    }

    private Room FindRoom(Object obj)
    {
        if (obj is Room)
        {
            return (Room)obj;
        }

        var parent = obj.Parent;

        while (parent != null)
        {
            if (parent is Room room)
            {
                return room;
            }

            parent = parent.Parent;
        }

        return null;
    }

}
