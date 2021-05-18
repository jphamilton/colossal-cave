﻿namespace Adventure.Net.Verbs
{
    public class Quit : Verb
    {
        public Quit()
        {
            Name = "quit";
            Synonyms.Are("q");
        }

        public bool Expects()
        {
            Context.Story.Quit();
            return true;
        }
        
    }
}
