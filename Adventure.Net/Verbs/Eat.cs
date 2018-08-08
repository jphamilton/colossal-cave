using System;

namespace Adventure.Net.Verbs
{

    public class Eat : Verb
    {
        public Eat()
        {
            Name = "eat";
            Grammars.Add("<held>", EatObject);
        }

        private bool EatObject()
        {
            if (!Object.IsEdible)
                Print(String.Format("{0} plainly inedible.", Object.TheyreOrThats));
            else
                Print(String.Format("You eat the {0}. Not bad.", Object.Name));
            
            Inventory.Remove(Object);
            
            return true;
        }

    }
}
