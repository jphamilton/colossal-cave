namespace Adventure.Net.Actions
{
    public class West : Direction
    {
        public West()
        {
            SetDirection(room => room.W(), "west", "w");
        }
        
    }
}
