namespace Adventure.Net.Verbs
{
    public class Out : DirectionalVerb
    {
        public Out()
        {
            SetDirection(room => room.OUT(), "out");
        }

    }
}