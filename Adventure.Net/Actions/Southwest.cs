namespace Adventure.Net.Actions
{
    public class Southwest : Direction
    {
        public Southwest()
        {
            SetDirection(room => room.SW(), "southwest", "sw");
        }

    }
}