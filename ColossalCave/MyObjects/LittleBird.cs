using Adventure.Net;
using Adventure.Net.Verbs;
//using Object = Adventure.Net.Object;

namespace ColossalCave.MyObjects
{
    public class LittleBird : Object
    {
        public override void Initialize()
        {
            Name = "little bird";
            Synonyms.Are("cheerful", "mournful", "little", "bird");
            IsAnimate = true;

            Before<Examine>(() =>
            {
                var cage = Objects.Get<WickerCage>();
                var bird = Objects.Get<LittleBird>();

                if (cage.Contents.Contains(bird))
                {
                    Print("The little bird looks unhappy in the cage.");
                }
                else
                {
                    Print("The cheerful little bird is sitting here singing.");
                }
                
                return true;

            });

            Before<Release>(() =>
            {
                var cage = Objects.Get<WickerCage>();
                var bird = Objects.Get<LittleBird>();

                if (!cage.Contents.Contains(bird))
                {
                    Print("The bird is not caged now.");
                    return true;
                }

                if (cage.InInventory)
                {
                    cage.IsOpen = true;
                    cage.Remove(bird);
                    bird.MoveToLocation();

                    var snake = Objects.Get<Snake>();

                    if (snake.AtLocation)
                    {
                        Print("The little bird attacks the green snake,");
                        Print("and in an astounding flurry drives the snake away.");
                        Location.Objects.Remove(snake);
                        return true;
                    }

                    var dragon = Objects.Get<Dragon>();
                    
                    if (dragon.AtLocation)
                    {
                        Print("The little bird attacks the green dragon,");
                        Print("and in an astounding flurry gets burnt to a cinder.");
                        Print("The ashes blow away.");
                        bird.Remove();
                        return true;
                    }

                    Print("The little bird flies free.");
                    return true;
                }

                return false;
            });

            Before<Drop>(() =>
                {
                    var cage = Objects.Get<WickerCage>();
                    if (cage.Contains<LittleBird>())
                    {
                        Print("(The bird is released from the cage.)");
                        var beforeRelease = Before<Release>();
                        return beforeRelease();
                    }

                    return false;
                });

            Before<Remove>(Before<Drop>());

            Before<Take>(() =>
            {

                var cage = Objects.Get<WickerCage>();
                var bird = Objects.Get<LittleBird>();
                var blackRod = Objects.Get<BlackRod>();

                if (blackRod.InInventory)
                {
                    Print("The bird was unafraid when you entered,");
                    Print("but as you approach it becomes disturbed and you cannot catch it.");
                    return true;
                }

                if (cage.InInventory)
                {
                    if (cage.Contents.Contains(bird))
                    {
                        Print("You already have the little bird.");
                        Print("If you take it out of the cage it will likely fly away from you.");
                        return true;
                    }
                    
                    Print("You catch the bird in the wicker cage.");
                    cage.IsOpen = false;
                    Location.Objects.Remove(bird);
                    cage.Add(bird);
                    return true;
                }
                
                Print("You can catch the bird, but you cannot carry it.");
                return true;

            });

            Before<Catch>(Before<Take>());

            Before<Insert>(() =>
                {
                    var cage = Objects.Get<WickerCage>();
                    var second = Context.IndirectObject;
                    if (second != null && second.IsContainer && second != cage)
                    {
                        Print("Don't put the poor bird in {0} {1}!", second.Article, second.Name);
                        return true;
                    }
                    
                    //Execute("take bird");
                    var beforeTake = Before<Take>();
                    return beforeTake(); 
                });

            Before<Attack>(() =>
                {
                    var cage = Objects.Get<WickerCage>();
                    var bird = Objects.Get<LittleBird>();

                    if (cage.Contents.Contains(bird))
                    {
                        Print("Oh, leave the poor unhappy bird alone.");
                        return true;
                    }

                    Print("The little bird is now dead. Its body disappears.");
                    bird.Remove();
                    return true;
                });

            Before<Ask>(() =>
                {
                    Print("Cheep! Chirp!");
                    return true;
                });
        }
    }

//Object  -> little_bird "little bird"
//with  name 'cheerful' 'mournful' 'little' 'bird',
//    initial "A cheerful little bird is sitting here singing.",
//    before [;
//      
//      Insert:
//        if (second == wicker_cage)
//            <<Catch self>>;
//        else
//            "Don't put the poor bird in ", (the) second, "!";
//      Release:
//        if (self notin wicker_cage)
//            "The bird is not caged now.";
//        give wicker_cage open;
//        move self to location;
//        if (Snake in location) {
//            remove Snake;
//            "The little bird attacks the green snake,
//             and in an astounding flurry drives the snake away.";
//        }
//        if (Dragon in location) {
//            remove self;
//            "The little bird attacks the green dragon,
//             and in an astounding flurry gets burnt to a cinder.
//             The ashes blow away.";
//        }
//        "The little bird flies free.";
//    ],
//    life [;
//      Give:
//        "It's not hungry. (It's merely pinin' for the fjords).
//         Besides, I suspect it would prefer bird seed.";
//      Order, Ask, Answer:
//        "Cheep! Chirp!";
//      Attack:
//        if (self in wicker_cage)
//            "Oh, leave the poor unhappy bird alone.";
//        remove self;
//        "The little bird is now dead. Its body disappears.";
//    ],
//has   animate;

}
