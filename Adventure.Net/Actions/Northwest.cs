namespace Adventure.Net.Actions
{
    public class Northwest : DirectionalVerb
    {
        public Northwest()
        {
            SetDirection(room => room.NW(), "northwest", "nw");
        }

    }
}