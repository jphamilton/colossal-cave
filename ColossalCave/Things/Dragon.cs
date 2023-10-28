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
            Print("With what? Your bare hands?");
            return true;
        });

        Before<Give>(() =>
        {
            Print("The dragon is implacable.");
            return true;
        });

        Before<ThrowAt>(() =>
        {
            if (Noun is Axe)
            {
                Noun.MoveToLocation();
                Print("The axe bounces harmlessly off the dragon's thick scales.");
            }
            else
            {
                Print("You'd probably be better off using your bare hands than that thing!");
            }

            return true;
        });
    }
}
