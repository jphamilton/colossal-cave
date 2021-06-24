﻿namespace Adventure.Net.Actions
{
    public class Search : Verb
    {
        public Search()
        {
            Name = "search";
        }

        public bool Expects(Object obj)
        {
            return Print("You find nothing of interest.");
        }
    }
}
