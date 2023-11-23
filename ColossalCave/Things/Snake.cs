using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public class Snake : Object
{
    private const string TooDangerous = "Attacking the snake both doesn't work and is very dangerous.";
    private const string NothingToEat = "There's nothing here it wants to eat (except perhaps you).";

    public override void Initialize()
    {
        Name = "snake";
        Synonyms.Are("cobra", "asp", "huge", "fierce", "green", "ferocious",
            "venemous", "venomous", "large", "big", "killer");
        Description = "I wouldn't mess with it if I were you.";
        InitialDescription = "A huge green fierce snake bars the way!";
        Animate = true;

        FoundIn<HallOfMtKing>();

        Before<Attack>(() => TooDangerous);

        Before<Order, Ask, Answer>(() => Print("Hiss!"));

        Before<ThrowAt>(() =>
        {
            if (Noun is Axe)
            {
                return TooDangerous;
            }

            return NothingToEat;
        });

        Before<Give>(() =>
        {
            if (Noun is LittleBird bird)
            {
                bird.Remove();
                Print("The snake has now devoured your bird.");
            }
            else
            {
                Print(NothingToEat);
            }

            return true;
        });

        Before<Take>(() =>
        {
            Print("It takes you instead. Glrp!");
            GameOver.Dead();
            return true;
        });
    }
}
