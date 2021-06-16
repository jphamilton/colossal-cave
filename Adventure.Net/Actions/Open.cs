using System;

namespace Adventure.Net.Actions
{

    public class Open : Verb
    {
        public Open()
        {
            Name = "open";
            Synonyms.Are("open, uncover, undo, unwrap");
        }

        public bool Expects(Item obj)
        {
            if (!obj.IsOpenable)
            {
                Print($"{obj.TheyreOrThats} not something you can open.");
            }
            else if (obj.IsLocked)
            {
                string seems = obj.HasPluralName ? "They seem" : "It seems";
                Print($"{seems} to be locked.");
            }
            else if (obj.IsOpen)
            {
                Print($"{obj.TheyreOrThats} already open.");
            }
            else
            {
                obj.IsOpen = true;
                Print($"You open the {obj.Name}.");
            }

            return true;
        }

        public bool Expects(Item obj, Preposition.With with, Item indirect)
        {
            return Redirect<Unlock>(obj, v => v.Expects(obj, with, indirect));
        }

    }
}
