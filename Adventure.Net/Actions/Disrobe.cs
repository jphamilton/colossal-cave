﻿namespace Adventure.Net.Actions
{
    public class Disrobe : Verb
    {
        public Disrobe()
        {
            Name = "disrobe";
            Synonyms.Are("doff", "shed");
        }

    }
}