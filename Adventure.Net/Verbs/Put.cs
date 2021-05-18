using System;

namespace Adventure.Net.Verbs
{
    // TODO: implement

    //Verb 'put'
    //    * multiexcept 'in'/'inside'/'into' noun     -> Insert
    //    * multiexcept 'on'/'onto' noun              -> PutOn
    //    * 'on' held                                 -> Wear
    //    * 'down' multiheld                          -> Drop
    //    * multiheld 'down'                          -> Drop;
    public class Put : Verb
    {
        public Put()
        {
            Name = "put";
            Multi = true;
            MultiHeld = true;

            //Grammars.Add("<multi> in <noun>", InsertObject);
            //Grammars.Add("<multiheld> on <noun>", PutOnObject);
            //Grammars.Add("on <held>", WearObject);
            //Grammars.Add("down <multiheld>", DropObject);
            //Grammars.Add("<multiheld> down", DropObject);
        }

        // insert
        public bool Expects(Item obj, Preposition prep, Item indirect)
        {
            if (prep == Preposition.In)
            {
                return Redirect<Insert>(obj, v => v.Expects(obj, prep, indirect)); //Insert(obj, indirect);
            }
            else if (prep == Preposition.On)
            {
                return PutOn(obj, indirect);
            }
            return true;

            // TODO: maybe exception?
            // throw new PartialUnderstandingException()
        }

        private bool PutOn(Item obj, Item indirect)
        {
            if (!obj.InInventory)
            {
                Print($"You need to be holding the {obj.Name} before you can put it on top of something else.");
                return true;
            }

            // TODO: Implement "put on"
            throw new NotImplementedException("put on <object> (wear)");    
        }

        //private bool InsertObject()
        //{
        //    return RedirectTo<Insert>("<multi> in <noun>");
        //}

        //private bool PutOnObject()
        //{

        //}

        //private bool WearObject()
        //{
        //    // TODO: Implement "wear cape"
        //    throw new Exception("This is not implemented!!!!!");
        //}

        //private bool DropObject()
        //{
        //    return RedirectTo<Drop>("<multiheld>");
        //}


    }
}
