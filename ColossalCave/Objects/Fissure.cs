using Adventure.Net.Verbs;
using System;

namespace ColossalCave.Objects
{
    public class Fissure : Scenic
    {
        public override void Initialize()
        {
            Name = "fissure";
            Synonyms.Are("wide", "fissure");
            Description = "The fissure looks far too wide to jump.";

            //Scenic  "fissure"
            //with name 'wide' 'fissure',
            //description "The fissure looks far too wide to jump.",
            //found_in On_East_Bank_Of_Fissure West_Side_Of_Fissure;

            
        }
    }
}
