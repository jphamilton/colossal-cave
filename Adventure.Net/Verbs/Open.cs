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
            if (!Object.IsOpenable)
            {
                Print(String.Format("{0} not something you can open.", Object.TheyreOrThats));
            }
            else if (Object.IsLocked)
            {
                string seems = Object.HasPluralName ? "They seem" : "It seems";
                Print(String.Format("{0} to be locked.", seems));
            }
            else if (Object.IsOpen)
            {
                Print(Object.TheyreOrThats + " already open.");
            }
            else
            {
                Object.IsOpen = true;
                Print(String.Format("You open the {0}.", Object.Name));
            }

            return true;
        }

        private bool UnlockObject()
        {
            return RedirectTo<Unlock>("<noun> with <held>");
        }

    }
}
