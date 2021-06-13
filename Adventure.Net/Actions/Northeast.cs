namespace Adventure.Net.Actions
{
    public class Northeast : Direction
    {
        public Northeast()
        {
            SetDirection(room => room.NE(), "northeast", "ne");
        }

    }
}