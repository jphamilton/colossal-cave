namespace Adventure.Net.Verbs
{
    public class Take : Verb
    {
        public Take()
        {
            Name = "take";
            Synonyms.Are("take", "carry", "hold");
            Grammars.Add("<multi>", TakeObject);
        }

        private bool TakeObject()
        {
            bool result = false;

            if (Object.IsScenery)
            {
                Print("That's hardly portable.");
            }
            else if (Object.IsStatic)
            {
                Print("That's fixed in place.");
            }
            else if (Inventory.Contains(Object))
            {
                Print("You already have that.");
            }
            else
            {
                Context.Story.Location.Objects.Remove(Object);
                Inventory.Add(Object);
                Print("Taken.");
                result = true;
            }

            return result;
        }

        public bool TakeObject(Object obj)
        {
            Object = obj;
            return TakeObject();
        }


    }
}
