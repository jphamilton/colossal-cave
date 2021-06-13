namespace Adventure.Net.Actions
{
    public class Northeast : DirectionalVerb
    {
        public Northeast()
        {
            SetDirection(room => room.NE(), "northeast", "ne");
        }

    }
}