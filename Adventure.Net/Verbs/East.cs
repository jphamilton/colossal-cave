namespace Adventure.Net.Verbs
{
    public class East : DirectionalVerb
    {
        public East()
        {
            SetDirection(room => room.E(), "east", "e");
        }

    }
}
