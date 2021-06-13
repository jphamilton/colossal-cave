namespace Adventure.Net.Actions
{
    public class Southwest : DirectionalVerb
    {
        public Southwest()
        {
            SetDirection(room => room.SW(), "southwest", "sw");
        }

    }
}