using System;

namespace Adventure.Net.Verbs
{

    public class Open : Verb
    {
        public Open()
        {
            Name = "open";
            Synonyms.Are("open, uncover, undo, unwrap");
            Grammars.Add("<noun>", OpenObject);
            Grammars.Add("<noun> with <held>", UnlockObject);
        }

        private bool OpenObject()
        {
            if (!Item.IsOpenable)
            {
                Print(String.Format("{0} not something you can open.", Item.TheyreOrThats));
            }
            else if (Item.IsLocked)
            {
                string seems = Item.HasPluralName ? "They seem" : "It seems";
                Print(String.Format("{0} to be locked.", seems));
            }
            else if (Item.IsOpen)
            {
                Print(Item.TheyreOrThats + " already open.");
            }
            else
            {
                Item.IsOpen = true;
                Print(String.Format("You open the {0}.", Item.Name));
            }

            return true;
        }

        private bool UnlockObject()
        {
            return RedirectTo<Unlock>("<noun> with <held>");
        }

    }
}
