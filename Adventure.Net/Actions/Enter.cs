namespace Adventure.Net.Actions
{
    // TODO: implement
    //Verb 'enter' 'cross'
    //    *                                           -> GoIn
    //    * noun                                      -> Enter;
    public class Enter : DirectionalVerb, IDirectionProxy
    {
        public Enter()
        {
            Name = "enter";
            SetDirection(room => room.IN(), "enter", "in");
        }

        public bool Expects(Item obj)
        {
            // Object must have Before<Enter> routine
            Print("That's not something you can enter.");
            return true;
        }
    }
}
