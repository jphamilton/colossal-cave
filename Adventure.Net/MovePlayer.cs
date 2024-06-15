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
        var currentLocation = Player.Location;
        bool wasLit = currentLocation.Light;
        
        // this is the actual room the player is in,
        // even though he could be moving between "Darkness" rooms
        Room nextRoom = room;

        if (!Inventory.ProvidingLight() && !room.Light)
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
                try
                {
                    nextRoom.DarkToDark();
                }
                catch (DeathException)
                {
                    // player died from moving around in the dark
                    // so set location to the room they would
                    // have been in had the lights been on
                    Player.Location = nextRoom;
                    throw;
                }
            }
            else
            {
                CurrentRoom.Look(false);
            }
        }


        room.Visited = true;

        return nextRoom;
    }
}
