namespace Adventure.Net.Actions
{
    public class East : Direction
    {
        public East()
        {
            SetDirection(room => room.E(), "east", "e");
        }

    }
}
