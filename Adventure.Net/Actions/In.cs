namespace Adventure.Net.Actions
{
    public class In : DirectionalVerb
    {
        public In()
        {
            SetDirection(room => room.IN(), "in");
        }

    }
}