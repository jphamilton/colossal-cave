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
            {
                Print($"{Object.TheyreOrThats} plainly inedible.");
            }
            else if (Inventory.Contains(Object))
            {
                Print($"You eat the {Object.Name}. Not bad.");
            }
            else
            {
                Context.Story.Location.Objects.Remove(Object);
                Inventory.Add(Object);
                Print($"(first taking the {Object.Name})");
            }

            Inventory.Remove(Object);
            
            return true;
        }

    }
}
