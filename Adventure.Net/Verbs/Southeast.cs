namespace Adventure.Net.Verbs
{
    public class Southeast : DirectionalVerb
    {
        public Southeast()
        {
            SetDirection(room => room.SE(), "southeast", "se");
        }

    }
}