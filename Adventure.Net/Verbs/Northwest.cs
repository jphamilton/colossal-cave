namespace Adventure.Net.Verbs
{
    public class Northwest : DirectionalVerb
    {
        public Northwest()
        {
            SetDirection(room => room.NW(), "northwest", "nw");
        }

    }
}