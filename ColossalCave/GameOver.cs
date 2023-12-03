using Adventure.Net;
using Adventure.Net.Things;
using Adventure.Net.Utilities;
using ColossalCave.Actions;
using ColossalCave.Places;
using ColossalCave.Things;
using System;

namespace ColossalCave;

public static class GameOver
{
    public static void Finished()
    {
        Context.Story.IsDone = true;
        Display("*** Congratulations! ***");
        Console.ReadKey();
    }

    public static void Dead()
    {
        const string youDied = "*** You have died ***";

        Global.State.Deaths++; // this is models after the inform source which is why it's not just a private variable here
        int score = -10;

        Output.Print("\n\n");

        //TODO: implement caves_closed
        //if (caves_closed)
        //    "It looks as though you're dead. Well, seeing as how it's so close to closing time anyway,
        //    I think we'll just call it a day.";
        switch (Global.State.Deaths)
        {
            case 1:
                Output.Print("Oh dear, you seem to have gotten yourself killed. " +
                      "I might be able to help you out, but I've never really done this before. " +
                      "Do you want me to try to reincarnate you?");
                break;
            case 2:
                Output.Print("You clumsy oaf, you've done it again! " +
                      "I don't know how long I can keep this up. " +
                      "Do you want me to try reincarnating you again?");
                break;
            case 3:
                Output.Print("Now you've really done it! I'm out of orange smoke! " +
                      "You don't expect me to do a decent reincarnation without any orange smoke, do you?");
                break;

        }

        Output.Print("\n\n");

        var yes = YesOrNo.Ask();

        if (yes)
        {
            switch (Global.State.Deaths)
            {
                case 1:
                    Output.Print(
                        "All right. But don't blame me if something goes wr......" +
                        "\n\n\n\n---POOF!!---" +
                        "\n\r\nYou are engulfed in a cloud of orange smoke." +
                        "Coughing and gasping, you emerge from the smoke and find that you're....\n");
                    break;
                case 2:
                    Output.Print("Okay, now where did I put my orange smoke?.... >POOF!<" +
                    "\n\r\nEverything disappears in a dense cloud of orange smoke.\n");
                    break;
                case 3:
                    Output.Print("Okay, if you're so smart, do it yourself! I'm leaving!");
                    Display(youDied);
                    Context.Story.IsDone = true;
                    return;
            }

            // lamp is moved to At End of Road
            var lamp = ObjectMap.Remove<BrassLantern>();
            lamp.On = false;
            lamp.Light = false;

            var endOfRoad = Rooms.Get<EndOfRoad>();

            ObjectMap.MoveObject(lamp, endOfRoad);

            // remaining inventory items are dropped in the room where you died
            foreach (var obj in Inventory.Items)
            {
                if (obj is Treasure)
                {
                    score -= 5;
                }
            }

            foreach (var item in Inventory.Items)
            {
                ObjectMap.MoveObject(item, Player.Location);
            }

            Inventory.Items.Clear();

            MovePlayer.To<InsideBuilding>();

            Score.Add(score, true);

            // Not sure what this is doing   
            // remove dwarf;
        }
        else
        {
            switch (Global.State.Deaths)
            {
                case 1:
                    Output.Print("Very well.");
                    break;
                case 2:
                    Output.Print("Probably a wise choice.");
                    break;
                case 3:
                    Output.Print("I thought not!");
                    break;
            }

            Display(youDied);

            Context.Story.IsDone = true;
        }
    }

    private static void Display(string message)
    {
        var score = Context.Story.CurrentScore;
        var possible = Context.Story.PossibleScore;

        Output.Print("\n\n");
        Output.Print($"\t{message}");
        Output.Print("\n\n");
        Output.Print($"In that game you scored {score} points out of a possible {possible}, earning you the rank of {Score.GetRank()}");
    }
}
