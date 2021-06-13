namespace Adventure.Net.Actions
{
    public class North : DirectionalVerb
    {
        public North()
        {
            SetDirection(room => room.N(), "north", "n");
        }

    }
}