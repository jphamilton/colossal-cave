using Adventure.Net.Actions;
using System;

namespace Adventure.Net;

public abstract class Door : Room
{
    private Func<Room> doorTo;
    private Func<Direction> doorDirection;

    protected Door()
    {
        Static = true;
        Openable = true;

        Before<Enter>(() =>
        {
            var dir = doorDirection();
            return dir.Expects();
        });
    }

    public string WhenOpen { get; set; }

    public string WhenClosed { get; set; }

    public string Display()
    {
        if (WhenOpen != null && Open)
        {
            return $"\n{WhenOpen}";
        }
        else if (WhenClosed != null && !Open)
        {
            return $"\n{WhenClosed}";
        }

        return null;
    }

    public void DoorTo(Func<Room> action)
    {
        doorTo = action;
    }

    public Room DoorTo()
    {
        return doorTo();
    }

    public void DoorDirection(Func<Direction> action)
    {
        doorDirection = action;
    }

    protected T Direction<T>() where T : Direction
    {
        Type t = typeof(T);
        return Activator.CreateInstance(t) as T;
    }

    protected override Room HandleMove()
    {
        if (!Locked && Open)
        {
            return doorTo();
        }

        if (doorDirection() is Down)
        {
            Print($"You are unable to descend by the {Name}.");
        }
        else if (doorDirection() is Up)
        {
            Print($"You are unable to ascend by the {Name}");
        }
        else if (doorDirection() != null)
        {
            Print("You can't go that way.");
        }
        else
        {
            string lead = PluralName ? "leads" : "lead";
            Print($"You can't since the {Name} {lead} to nowhere.");
        }

        return null;

    }
}

