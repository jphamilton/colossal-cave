using Adventure.Net.Things;

namespace Adventure.Net.ActionRoutines;

// useful for testing
public class GoNear : Routine
{
    public GoNear()
    {
        Verbs = ["gonear"];
        InScopeOnly = false;
    }

    public override bool Handler(Object obj, Object second = null)
    {
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

