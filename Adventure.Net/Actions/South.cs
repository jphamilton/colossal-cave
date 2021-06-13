namespace Adventure.Net.Actions
{
    public class South : DirectionalVerb
    {
        public South()
        {
            SetDirection(room => room.S(), "south", "s");
        }

    }
}