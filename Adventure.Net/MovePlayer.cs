using Adventure.Net.Places;
using Adventure.Net.Things;

namespace Adventure.Net;

public static class MovePlayer
{
    public static bool To<T>() where T : Room
    {
        var room = Rooms.Get<T>();
        To(room);
        return true;
    }

    public static void To(Room room)
    {
        if (Context.Current != null)
        {
            Context.Current.Move = () => Move(room);
        }
        else
        {
            Player.Location = Move(room);
        }
    }

    private static Room Move(Room room)
    {
        Room nextRoom = room;

        if (!CurrentRoom.IsLit() && !room.Light)
        {
            room = Rooms.Get<Darkness>();
        }

        Player.Location = room;

        if (!room.Visited)
        {
            CurrentRoom.Look(true);

            room.Initial?.Invoke();
        }
        else
        {
            if (!CurrentRoom.IsLit() && room.Visited)
            {
                nextRoom.DarkToDark();
            }

            CurrentRoom.Look(false);
        }


        room.Visited = true;

        return nextRoom;
    }
}
