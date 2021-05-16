using System;

namespace Adventure.Net.Verbs
{

    public class Close : Verb
    {
        public Close()
        {
            Name = "close";
            Synonyms.Are("cover", "shut");
            Grammars.Add("<noun>", CloseObject);
            Grammars.Add("up <noun>", CloseObject);
            Grammars.Add("off <noun>", SwitchOffObject);
        }

        private bool CloseObject()
        {
            if (!Item.IsOpenable)
            {
                Print(String.Format("{0} not something you can close.", Item.TheyreOrThats));
            }
            else if (!Item.IsOpen)
            {
                Print(Item.TheyreOrThats + " already closed.");
            }
            else
            {
                Item.IsOpen = false;
                Print(String.Format("You close the {0}.", Item.Name));
            }

            return true;
        }
        
        private bool SwitchOffObject()
        {
            return RedirectTo<SwitchOff>("off <noun>");
        }

        
    }
}
