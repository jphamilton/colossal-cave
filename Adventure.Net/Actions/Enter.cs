namespace Adventure.Net.Actions
{
    public class Enter : Direction, IDirectionProxy
    {
        public Enter()
        {
            Name = "enter";
            SetDirection(room => room.IN(), "enter", "in");
        }

        public bool Expects(Object obj)
        {
            // Object must have Before<Enter> routine
            Print("That's not something you can enter.");
            return true;
        }
    }
}
