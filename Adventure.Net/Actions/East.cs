namespace Adventure.Net.Actions
{
    public class East : DirectionalVerb
    {
        public East()
        {
            SetDirection(room => room.E(), "east", "e");
        }

    }
}
