namespace Adventure.Net.Actions
{
    public class South : Direction
    {
        public South()
        {
            SetDirection(room => room.S(), "south", "s");
        }

    }
}