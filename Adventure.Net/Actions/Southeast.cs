namespace Adventure.Net.Actions
{
    public class Southeast : Direction
    {
        public Southeast()
        {
            SetDirection(room => room.SE(), "southeast", "se");
        }

    }
}