namespace Adventure.Net.Actions
{
    public class Out : DirectionalVerb
    {
        public Out()
        {
            SetDirection(room => room.OUT(), "out");
        }

    }
}