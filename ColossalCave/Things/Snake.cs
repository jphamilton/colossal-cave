using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class Snake : Item
    {
        public override void Initialize()
        {
            Name = "snake";
            Synonyms.Are("cobra", "asp", "huge", "fierce", "green", "ferocious", 
                "venemous", "venomous", "large", "big", "killer" );
            Description = "I wouldn't mess with it if I were you.";
            InitialDescription = "A huge green fierce snake bars the way!";
            IsAnimate = true;

            Before<Attack>(() =>
            {
                Print("Attacking the snake both doesn't work and is very dangerous.");
                return true;
            });

            Before<Give>(() =>
            {
                if (CurrentObject is LittleBird)
                {
                    CurrentObject.Remove();
                    Print("The snake has now devoured your bird.");
                }
                else
                {
                    Print("There's nothing here it wants to eat (except perhaps you).");
                }

                return true;
            });

            Before<Take>(() =>
            {
                Print("It takes you instead. Glrp!");
                AfterLife.GoTo();
                return true;
            });
        }
    }
}

/*
 Object  -> Snake "snake"
  with  name 'snake' 'cobra' 'asp' 'huge' 'fierce' 'green' 'ferocious'
             'venemous' 'venomous' 'large' 'big' 'killer',
        description "I wouldn't mess with it if I were you.",
        initial "A huge green fierce snake bars the way!",
        life [;
          Order, Ask, Answer:
            "Hiss!";
          ThrowAt:
            if (noun == axe) <<Attack self>>;
            <<Give noun self>>;
          Give:
            if (noun == little_bird) {
                remove little_bird;
                "The snake has now devoured your bird.";
            }
            "There's nothing here it wants to eat (except perhaps you).";
          Attack:
            "Attacking the snake both doesn't work and is very dangerous.";
          Take:
            deadflag = 1;
            "It takes you instead. Glrp!";
        ],
  has   animate;
 */