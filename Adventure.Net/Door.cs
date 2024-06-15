using Adventure.Net.ActionRoutines;
using System;

namespace Adventure.Net;

public abstract class Door : Room
{
    private Func<Room> _doorTo;
    private Func<Direction> _doorDirection;

    protected Door()
    {
        Static = true;
        Openable = true;

        Before<Enter>(() =>
        {
            var dir = _doorDirection();
            return dir.Handler();
        });
    }

    public string WhenOpen { get; set; }

    public string WhenClosed { get; set; }

    public bool TryDisplay(out string message)
    {
        message = null;

        if (WhenOpen != null && Open)
        {
            message = $"\n{WhenOpen}";
        }
        else if (WhenClosed != null && !Open)
        {
            message = $"\n{WhenClosed}";
        }

        return message != null;
    }

    public void DoorTo(Func<Room> action)
    {
        _doorTo = action;
    }

    public Room DoorTo()
    {
        return _doorTo();
    }

    public void DoorDirection(Func<Direction> action)
    {
        _doorDirection = action;
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
            return _doorTo();
        }

        if (_doorDirection() is Down)
        {
            Print($"You are unable to descend by the {Name}.");
        }
        else if (_doorDirection() is Up)
        {
            Print($"You are unable to ascend by the {Name}");
        }
        else if (_doorDirection() != null)
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

