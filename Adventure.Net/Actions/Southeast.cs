namespace Adventure.Net.Actions
{
    public class Southeast : DirectionalVerb
    {
        public Southeast()
        {
            SetDirection(room => room.SE(), "southeast", "se");
        }

    }
}