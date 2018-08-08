namespace Adventure.Net.Verbs
{
    public class Northeast : DirectionalVerb
    {
        public Northeast()
        {
            SetDirection(room => room.NE(), "northeast", "ne");
        }

    }
}