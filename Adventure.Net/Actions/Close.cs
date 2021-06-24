namespace Adventure.Net.Actions
{
    //Verb 'close' 'cover' 'shut'
    //    * noun                                      -> Close
    //    * 'up' noun                                 -> Close
    //    * 'off' noun                                -> SwitchOff;
    //
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

        public bool Expects(Object obj)
        {
            return CloseObject(obj);
        }

        public bool Expects(Object obj, Preposition.Off off)
        {
            return Redirect<SwitchOff>(obj, v => v.Expects(obj, off));
        }

        public bool Expects(Object obj, Preposition.Up up)
        {
            return CloseObject(obj);
        }

        private bool CloseObject(Object obj)
        {
            if (!obj.Openable)
            {
                Print($"{obj.TheyreOrThats} not something you can close.");
            }
            else if (!obj.Open)
            {
                Print($"{obj.TheyreOrThats} already closed.");
            }
            else
            {
                obj.Open = false;
                Print($"You close the {obj.Name}.");
            }

            return true;
        }

    }
}
