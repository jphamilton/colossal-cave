using System;

namespace Adventure.Net.Verbs
{

    // TODO: implement
    public class Open : Verb
    {
        //Verb 'open' 'uncover' 'undo' 'unwrap'
        //    * noun                                      -> Open
        //    * noun 'with' held                          -> Unlock;
        public Open()
        {
            Name = "open";
            Synonyms.Are("open, uncover, undo, unwrap");
            //Grammars.Add("<noun>", OpenObject);
            //Grammars.Add("<noun> with <held>", UnlockObject);
        }

        // open
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

        // Unlock: <noun> with <held>
        public bool Expects(Item obj, Preposition prep, Item indirect)
        {
            throw new MissingMethodException("Open (Unlock) not implemented");
        }

        //private bool OpenObject()
        //{
        //    if (!Item.IsOpenable)
        //    {
        //        Print(String.Format("{0} not something you can open.", Item.TheyreOrThats));
        //    }
        //    else if (Item.IsLocked)
        //    {
        //        string seems = Item.HasPluralName ? "They seem" : "It seems";
        //        Print(String.Format("{0} to be locked.", seems));
        //    }
        //    else if (Item.IsOpen)
        //    {
        //        Print(Item.TheyreOrThats + " already open.");
        //    }
        //    else
        //    {
        //        Item.IsOpen = true;
        //        Print(String.Format("You open the {0}.", Item.Name));
        //    }

        //    return true;
        //}

        //private bool UnlockObject()
        //{
        //    return RedirectTo<Unlock>("<noun> with <held>");
        //}

    }
}
