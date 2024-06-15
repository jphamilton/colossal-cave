namespace Adventure.Net.ActionRoutines;

public class North : Direction
{
    public North()
    {
        Verbs = ["north", "n"];
        SetDirection(room => room.TryMove(this));
    }
}

public class Northeast : Direction
{
    public Northeast()
    {
        Verbs = ["northeast", "ne"];
        SetDirection(room => room.TryMove(this));
    }

}

public class Northwest : Direction
{
    public Northwest()
    {
        Verbs = ["northwest", "nw"];
        SetDirection(room => room.TryMove(this));
    }
}

public class East : Direction
{
    public East()
    {
        Verbs = ["east", "e"];
        SetDirection(room => room.TryMove(this));
    }
}

public class West : Direction
{
    public West()
    {
        Verbs = ["west", "w"];
        SetDirection(room => room.TryMove(this));
    }
}

public class South : Direction
{
    public South()
    {
        Verbs = ["south", "s"];
        SetDirection(room => room.TryMove(this));
    }
}

public class Southeast : Direction
{
    public Southeast()
    {
        Verbs = ["southeast", "se"];
        SetDirection(room => room.TryMove(this));
    }
}

public class Southwest : Direction
{
    public Southwest()
    {
        Verbs = ["southwest", "sw"];
        SetDirection(room => room.TryMove(this));
    }
}

public class Up : Direction
{
    public Up()
    {
        Verbs = ["up", "u"];
        SetDirection(room => room.TryMove(this));
    }
}

public class Down : Direction
{
    public Down()
    {
        Verbs = ["down", "d"];
        SetDirection(room => room.TryMove(this));
    }
}

public class In : Direction
{
    public In()
    {
        Verbs = ["in", "enter"];
        SetDirection(room => room.TryMove(this));
    }
}
public class Enter : Direction
{
    public Enter()
    {
        Verbs = ["enter", "in"];
        SetDirection(room => room.TryMove(this));
        Requires = [O.Noun];
    }

    public override bool Handler(Object _, Object __)
    {
        return Fail("That's not something you can enter.");
    }
}

public class Out : Direction
{
    public Out()
    {
        Verbs = ["out", "exit", "leave"];
        SetDirection(room => room.TryMove(this));
    }
}

public class Exit : Direction
{
    public Exit()
    {
        Verbs = ["exit", "leave", "out"];
        SetDirection(room => room.TryMove(this));
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Fail($"You aren't in {obj.DName}.");
    }
}
