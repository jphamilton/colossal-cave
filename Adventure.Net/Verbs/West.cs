namespace Adventure.Net.Verbs
{
    public class West : DirectionalVerb
    {
        public West()
        {
            SetDirection(room => room.W(), "west", "w");
        }
        
    }
}
