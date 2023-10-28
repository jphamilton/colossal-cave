namespace Adventure.Net.Actions;

public class Up : Direction
{
    public Up()
    {
        SetDirection(room => room.UP(), "up", "u");
    }

}