﻿namespace Adventure.Net.Verbs
{
    //Verb 'catch' 'capture'
    //    * creature              -> Catch
    //    * creature 'with' held  -> Catch;
    public class Catch : Verb
    {
        public Catch()
        {
            Name = "catch";
            Synonyms.Are("capture");
        }

        public bool Expects(Item obj)
        {
            return YouCantCatchThat(obj);
        }

        public bool Expects(Item obj, Preposition prep, Item indirect)
        {
            return YouCantCatchThat(obj);
        }

        private bool YouCantCatchThat(Item obj)
        {
            if (!obj.IsAnimate)
            {
                Print("You can only do that to something animate.");
            }
            else
            {
                Print("You can't catch that.");
            }

            return true;
        }
    }
}
