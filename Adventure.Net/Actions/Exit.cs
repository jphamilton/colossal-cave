namespace Adventure.Net.Actions;
public class Exit : Direction, IDirectional
{
    public Exit()
    {
        Name = "exit";
        SetDirection(room => room.OUT(), "exit", "leave");
    }

    public bool Expects(Object obj)
    {
        return Print($"You aren't in {obj.DName}.");
    }
}
