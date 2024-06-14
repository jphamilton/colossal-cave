namespace Adventure.Net.Actions;

public class Out : Direction, IDirectional
{
    public Out()
    {
        SetDirection(room => room.OUT(), "out");
    }

}