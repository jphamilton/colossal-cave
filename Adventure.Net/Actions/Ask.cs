﻿namespace Adventure.Net.Actions
{
    public class Ask : Verb
    {
        public Ask()
        {
            Name = "ask";
            //Grammars.Add("<creature> 'about' <topic>", AttackObject);
            //Grammars.Add("<noun> about <topic>", OnAsk);
           // Grammars.Add("<noun> for <noun>", OnAskFor);

        }

        public bool Expects(Object obj, Preposition.About about, Object indirect)
        {
            return true;
        }

        //private bool OnAsk()
        //{
        //    return Default();
        //}

        //private bool Default()
        //{
        //    if (!Item.IsAnimate)
        //    {
        //        Print("You can only do that to something animate.");
        //        return true;
        //    }
        //    Print("There was no reply.");
        //    return true;
        //}

        //private bool OnAskFor()
        //{
        //    return Default();
        //}

    }
}