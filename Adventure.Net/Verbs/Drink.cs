﻿namespace Adventure.Net.Verbs
{
    public class Drink : Verb
    {
        // TODO: implement
        public Drink()
        {
            Name = "drink";
            Synonyms.Are("sip", "swallow");
            //Grammars.Add("<noun>", DrinkObject);
        }

        //public bool DrinkObject()
        //{
        //    Print("You can't drink that.");
        //    return true;
        //}
    }
}
