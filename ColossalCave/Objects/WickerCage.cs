using Adventure.Net;
using Adventure.Net.Verbs;

namespace ColossalCave.Objects
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
                    
                    var bird = Get<LittleBird>();
                   // bird.Release();
                    Redirect<Release>(bird, v => v.Expects(bird)); 
                }

            });
        }
    }
}