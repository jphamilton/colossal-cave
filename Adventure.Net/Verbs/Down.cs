namespace Adventure.Net.Verbs
{
    public class Down : DirectionalVerb
    {
        public Down()
        {
            SetDirection(room => room.DOWN(), "down", "d");
        }

    }
}