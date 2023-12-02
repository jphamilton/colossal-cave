using Adventure.Net.Places;

namespace Adventure.Net.Things;


public class Player : Object
{
    private static Player _player;

    public override void Initialize()
    {
        _player = Objects.Get<Player>();
    }


    public static Room Room => (Room)_player.Parent;

    public static bool MoveTo<T>() where T : Room
    {
        var room = Rooms.Get<T>();
        return MoveTo(room);
    }

    public static bool MoveTo(Room room)
    {
        if (Context.Current != null)
        {
            Context.Current.Move = () => Move(room);
        }
        else
        {
            _player.Parent = Move(room);
        }

        return true;
    }

    private static Room Move(Room room)
    {
        Room nextRoom = room;

        if (!CurrentRoom.IsLit() && !room.Light)
        {
            room = Rooms.Get<Darkness>();
        }

        _player.Parent = room;

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
