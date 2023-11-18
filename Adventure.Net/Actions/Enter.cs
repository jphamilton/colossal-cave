namespace Adventure.Net.Actions;

public class Enter : Direction, IDirectional
{
    public Enter()
    {
        Name = "enter";
        SetDirection(room => room.IN(), "enter", "in");
    }

    public bool Expects(Object obj)
    {
        return Print("That's not something you can enter.");
    }
}
