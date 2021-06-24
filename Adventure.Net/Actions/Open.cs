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

        public bool Expects(Object obj)
        {
            if (!obj.Openable)
            {
                Print($"{obj.TheyreOrThats} not something you can open.");
            }
            else if (obj.Locked)
            {
                string seems = obj.PluralName ? "They seem" : "It seems";
                Print($"{seems} to be locked.");
            }
            else if (obj.Open)
            {
                Print($"{obj.TheyreOrThats} already open.");
            }
            else
            {
                obj.Open = true;
                Print($"You open the {obj.Name}.");
            }

            return true;
        }

        public bool Expects(Object obj, Preposition.With with, Object indirect)
        {
            return Redirect<Unlock>(obj, v => v.Expects(obj, with, indirect));
        }

    }
}
