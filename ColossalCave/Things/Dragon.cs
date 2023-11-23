using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Dragon : Object
{
    public bool IsBeingAttacked { get; set; }

    public override void Initialize()
    {
        Name = "dragon";
        Synonyms.Are("monster", "beast", "lizard", "huge", "green", "fierce", "scaly", "giant", "ferocious");
        Description = "I wouldn't mess with it if I were you.";
        InitialDescription = "A huge green fierce dragon bars the way!";
        Animate = true;

        FoundIn<SecretCanyon>();

        Before<Attack>(() =>
        {
            IsBeingAttacked = true;
            return Print("With what? Your bare hands?");
        });

        Before<Give>(() => Print("The dragon is implacable."));

        Before<ThrowAt>(() =>
        {
            if (Noun is Axe axe)
            {
                axe.MoveToLocation();
                return Print("The axe bounces harmlessly off the dragon's thick scales.");
            }
            
            return Print("You'd probably be better off using your bare hands than that thing!");
        });
    }
}
