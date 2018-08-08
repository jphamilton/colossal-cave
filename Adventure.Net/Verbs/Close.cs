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
            if (!Object.IsOpenable)
            {
                Print(String.Format("{0} not something you can close.", Object.TheyreOrThats));
            }
            else if (!Object.IsOpen)
            {
                Print(Object.TheyreOrThats + " already closed.");
            }
            else
            {
                Object.IsOpen = false;
                Print(String.Format("You close the {0}.", Object.Name));
            }

            return true;
        }
        
        private bool SwitchOffObject()
        {
            return RedirectTo<SwitchOff>("off <noun>");
        }

        
    }
}
