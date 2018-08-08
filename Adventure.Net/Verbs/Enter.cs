namespace Adventure.Net.Verbs
{
    public class Enter : DirectionalVerb
    {
        public Enter()
        {
            Name = "enter";
            SetDirection(room => room.IN(), "in");
            Grammars.Add("<noun>", EnterObject);
        }

        public bool EnterObject()
        {
            Print("That's not something you can enter");
            return true;
        }
    }
}
