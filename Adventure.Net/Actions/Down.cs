namespace Adventure.Net.Actions
{
    public class Down : Direction
    {
        public Down()
        {
            SetDirection(room => room.DOWN(), "down", "d");
        }

    }
}