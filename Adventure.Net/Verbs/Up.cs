namespace Adventure.Net.Verbs
{
    public class Up : DirectionalVerb
    {
        public Up()
        {
            SetDirection(room => room.UP(), "up", "u");
        }

    }
}