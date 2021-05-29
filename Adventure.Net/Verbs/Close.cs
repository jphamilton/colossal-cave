namespace Adventure.Net.Verbs
{
    //Verb 'close' 'cover' 'shut'
    //    * noun                                      -> Close
    //    * 'up' noun                                 -> Close
    //    * 'off' noun                                -> SwitchOff;
    //
    // TODO: Implicit close? not sure when it happens
    public class Close : Verb
    {
        public Close()
        {
            Name = "close";
            Synonyms.Are("cover", "shut");
            //Grammars.Add("<noun>", CloseObject);
            //Grammars.Add("up <noun>", CloseObject);
            //Grammars.Add("off <noun>", SwitchOffObject);
        }

        public bool Expects(Item obj)
        {
            return CloseObject(obj);
        }

        public bool Expects(Item obj, Preposition.Off off)
        {
            return Redirect<SwitchOff>(obj, v => v.Expects(obj, off));
        }

        public bool Expects(Item obj, Preposition.Up up)
        {
            return CloseObject(obj);
        }

        private bool CloseObject(Item obj)
        {
            if (!obj.IsOpenable)
            {
                Print($"{obj.TheyreOrThats} not something you can close.");
            }
            else if (!obj.IsOpen)
            {
                Print($"{obj.TheyreOrThats} already closed.");
            }
            else
            {
                obj.IsOpen = false;
                Print($"You close the {obj.Name}.");
            }

            return true;
        }

    }
}
