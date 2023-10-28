namespace Adventure.Net.Actions;

public class In : Direction
{
    public In()
    {
        SetDirection(room => room.IN(), "in");
    }

}