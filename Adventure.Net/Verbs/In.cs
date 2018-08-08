namespace Adventure.Net.Verbs
{
    public class In : DirectionalVerb
    {
        public In()
        {
            SetDirection(room => room.IN(), "in");
        }

    }
}