namespace Adventure.Net.Actions
{
    public class North : Direction
    {
        public North()
        {
            SetDirection(room => room.N(), "north", "n");
        }

    }
}