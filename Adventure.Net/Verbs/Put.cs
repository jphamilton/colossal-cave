using System;

namespace Adventure.Net.Verbs
{

    public class Put : Verb
    {
        public Put()
        {
            Name = "put";
            Grammars.Add("<multi> in <noun>", InsertObject);
            Grammars.Add("<multiheld> on <noun>", PutOnObject);
            Grammars.Add("on <held>", WearObject);
            Grammars.Add("down <multiheld>", DropObject);
            Grammars.Add("<multiheld> down", DropObject);
        }

        private bool InsertObject()
        {
            return RedirectTo<Insert>("<multi> in <noun>");
        }
        
        private bool PutOnObject()
        {
            if (!Object.InInventory)
            {
                Print($"You need to be holding the {Object.Name} before you can put it on top of something else.");
                return true;
            }

            throw new Exception("Not implemented");
        }

        private bool WearObject()
        {
            throw new Exception("This is not implemented!!!!!");
        }

        private bool DropObject()
        {
            return RedirectTo<Drop>("<multiheld>");
        }

        
    }
}
