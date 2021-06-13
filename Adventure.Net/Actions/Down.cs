namespace Adventure.Net.Actions
{
    public class Down : DirectionalVerb
    {
        public Down()
        {
            SetDirection(room => room.DOWN(), "down", "d");
        }

    }
}