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
            if (!Item.IsEdible)
            {
                Print($"{Item.TheyreOrThats} plainly inedible.");
            }
            else if (Inventory.Contains(Item))
            {
                Print($"You eat the {Item.Name}. Not bad.");
            }
            else
            {
                Context.Story.Location.Objects.Remove(Item);
                Inventory.Add(Item);
                Print($"(first taking the {Item.Name})");
            }

            Inventory.Remove(Item);
            
            return true;
        }

    }
}
