namespace Adventure.Net.Verbs
{

    // Verb 'jump' 'hop' 'skip'
    //*                                           -> Jump
    //* 'in' noun                                 -> JumpIn
    //* 'into' noun                               -> JumpIn
    //* 'on' noun                                 -> JumpOn
    //* 'upon' noun                               -> JumpOn
    //* 'over' noun                               -> JumpOver;

    public class Jump : Verb
    {
        public Jump()
        {
            Name = "jump";
            Synonyms.Are("hop", "skip");
        }

        public bool Expects()
        {
            Print("You jump on the spot, fruitlessly.");
            return true;
        }

        public bool Expects(Preposition.Over over, Item obj)
        {
            Print($"Jumping over {obj} would achieve nothing here.");
            return true;
        }
    }
}
