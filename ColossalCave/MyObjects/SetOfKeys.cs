using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class SetOfKeys : Object
    {
        public override void Initialize()
        {
            Name = "set of keys";
            Synonyms.Are("keys", "key", "keyring", "set", "of", "bunch");
            Description = "It's just a normal-looking set of keys.";
            InitialDescription = "There are some keys on the ground here.";
            
            Before<Count>(() =>
                {
                    Print("A dozen or so keys.");
                    return true;
                });
            
        }
    }
}

