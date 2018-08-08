namespace Adventure.Net.Verbs
{
    public class South : DirectionalVerb
    {
        public South()
        {
            SetDirection(room => room.S(), "south", "s");
        }

    }
}