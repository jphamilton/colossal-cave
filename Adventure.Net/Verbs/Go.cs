namespace Adventure.Net.Verbs
{
    // TODO: implement
    public class Go : Verb, IDirectionProxy
    {
        /*
         Verb 'go' 'run' 'walk'
            *                                           -> VagueGo
            * noun=ADirection                           -> Go
            * noun                                      -> Enter
            * 'out'                                     -> Exit     
            * 'in'                                      -> GoIn     
            * 'in'/'through' noun                       -> Enter;
        */
        public Go()
        {
            Name = "go";
            Synonyms.Are("walk", "run");
           // Grammars.Add(K.DIRECTION_TOKEN, ()=> false);
            //Grammars.Add(K.NOUN_TOKEN, EnterIt);
        }

        //private bool EnterIt()
        //{
        //    return RedirectTo<Enter>(K.NOUN_TOKEN);
        //}
        
        // TODO: This is new
        public bool Expects()
        {
            Print("You'll have to say which compass direction to go in.");
            return true;
        }

        public bool Expects(Item obj)
        {
            return Redirect<Enter>(obj, v => v.Expects(obj));
        }
    }
}
