namespace Adventure.Net.Actions
{
    public class Out : Direction
    {
        public Out()
        {
            SetDirection(room => room.OUT(), "out");
        }

    }
}