using Adventure.Net.Things;
using System;

namespace Adventure.Net.ActionRoutines;

public abstract class Direction : Routine
{
    private Func<Room, Room> getRoom;

    protected void SetDirection(Func<Room, Room> mover)
    {
        getRoom = mover;
    }

    public override bool Handler(Object first, Object second = null)
    {
        var room = getRoom(Player.Location);

        if (room == Player.Location)
        {
            // do nothing, so why is this block here?
        }
        else if (room == null)
        {
            Print(Player.Location.CantGo);
        }
        else
        {
            MovePlayer.To(room);
        }

        return true;
    }

    public DirectionObject ToObject()
    {
        return new DirectionObject(Verbs[0]);
    }

    // Direction as Object (should be rare used) for commands like "look west"
    public class DirectionObject : Object
    {
        public DirectionObject(string direction)
        {
            Name = direction;
        }

        public override void Initialize() { }
    }
}