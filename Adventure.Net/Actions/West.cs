namespace Adventure.Net.Actions
{
    public class West : DirectionalVerb
    {
        public West()
        {
            SetDirection(room => room.W(), "west", "w");
        }
        
    }
}
