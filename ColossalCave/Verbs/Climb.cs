using Adventure.Net;

namespace ColossalCave.Verbs
{
    //Verb 'climb' 'scale'
    //* noun                                      -> Climb
    //* 'up'/'over' noun                          -> Climb;

    public class Climb : Verb
    {
        public Climb()
        {
            Name = "climb";
            Synonyms.Are("climb", "scale");
        }

        public bool Expects(Item obj)
        {
            Print("Climbing that would achieve little.");
            return true;
        }

        public bool Expects(Preposition.Up up, Item obj)
        {
            Print("Climbing that would achieve little.");
            return true;
        }

        public bool Expects(Preposition.Over over, Item obj)
        {
            Print("Climbing that would achieve little.");
            return true;
        }
    }
}
