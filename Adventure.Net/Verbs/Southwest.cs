namespace Adventure.Net.Verbs
{
    public class Southwest : DirectionalVerb
    {
        public Southwest()
        {
            SetDirection(room => room.SW(), "southwest", "sw");
        }

    }
}