using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.MyObjects
{
    public class WickerCage : Container
    {
        public override void Initialize()
        {
            Name = "wicker cage";
            Synonyms.Are("cage", "small", "wicker");
            InitialDescription = "There is a small wicker cage discarded nearby.";
            Description = "It's a small wicker cage.";
            IsOpen = true;
            IsOpenable = true;
            IsTransparent = true;

            After<Open>(() =>
            {
                if (Contains<LittleBird>())
                {
                    Print("(releasing the little bird)"); 
                    Execute("release bird");
                    return true;
                }

                return false;
            });
        }
    }
}

//TODO: Object  -> wicker_cage "wicker cage"
//  with  name 'cage' 'small' 'wicker',
//        description "It's a small wicker cage.",
//        initial "There is a small wicker cage discarded nearby.",
//        after [;
//          Open:
//            if (little_bird notin self) rfalse;
//            print "(releasing the little bird)^";
//            <<Release little_bird>>;
//        ],
//  has   container open openable transparent;
