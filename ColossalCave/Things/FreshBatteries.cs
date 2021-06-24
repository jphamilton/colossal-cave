using Adventure.Net;
using ColossalCave.Actions;

namespace ColossalCave.Things
{
    public class FreshBatteries : Object
    {
        public bool HaveBeenUsed { get; set; }

        public override void Initialize()
        {
            Name = "fresh batteries";
            Synonyms.Are("batteries", "battery", "fresh");
            Description = "They look like ordinary batteries. (A sepulchral voice says, \"Still going!\")";
            InitialDescription = "There are fresh batteries here.";
            
            Before<Count>(() => "A pair.");
        }
    }

}
