namespace Adventure.Net.Actions
{
    public class Northwest : Direction
    {
        public Northwest()
        {
            SetDirection(room => room.NW(), "northwest", "nw");
        }

    }
}