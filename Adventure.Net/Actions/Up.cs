namespace Adventure.Net.Actions
{
    public class Up : DirectionalVerb
    {
        public Up()
        {
            SetDirection(room => room.UP(), "up", "u");
        }

    }
}