using System;

namespace Adventure.Net.Actions
{
    //Verb 'empty'
    //    * noun                                      -> Empty
    //    * 'out' noun                                -> Empty
    //    * noun 'out'                                -> Empty
    //    * noun 'to'/'into'/'on'/'onto' noun         -> EmptyT;
    public class Empty : Verb
    {
        public Empty()
        {
            Name = "empty";
        }

        public bool Expects(Item obj)
        {
            return EmptyObject(obj);   
        }

        public bool Expects(Item obj, Preposition.Out @out)
        {
            return EmptyObject(obj);
        }

        public bool Expects(Item obj, Preposition.To to, Item indirect)
        {
            return Pour(obj, indirect);
        }

        public bool Expects(Item obj, Preposition.Into into, Item indirect)
        {
            return Pour(obj, indirect);
        }

        public bool Expects(Item obj, Preposition.On on, Item indirect)
        {
            return Pour(obj, indirect);
        }
        public bool Expects(Item obj, Preposition.Onto onto, Item indirect)
        {
            return Pour(obj, indirect);
        }

        private bool EmptyObject(Item obj)
        {
            Container container = obj as Container;

            if (container == null)
            {
                Print($"The {obj.Name} can't contain things.");
            }
            else if (!container.Open)
            {
                Print($"The {container.Name} {container.IsOrAre} closed.");
            }
            else if (container.IsEmpty)
            {
                Print($"The {container.Name} {container.IsOrAre} empty already.");
            }
            else
            {
                Print("That would scarcely empty anything.");
            }

            return true;
        }

        private bool Pour(Item obj, Item indirect)
        {
            throw new NotImplementedException();
        }
    }
}

