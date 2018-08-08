namespace Adventure.Net.Verbs
{
    public class North : DirectionalVerb
    {
        public North()
        {
            SetDirection(room => room.N(), "north", "n");
        }

    }
}