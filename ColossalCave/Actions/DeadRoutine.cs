using Adventure.Net;
using Adventure.Net.ActionRoutines;
using Adventure.Net.Things;
using Adventure.Net.Utilities;
using ColossalCave.Places;
using ColossalCave.Things;
using System.Linq;

namespace ColossalCave.Actions;

public class DeadRoutine : Dead
{
    public override bool Handler(Object _, Object __ = null)
    {
        const string YouDied = "*** You have died ***";

        Global.State.Deaths++; // routine members don't get saved/restored

        Output.PrintLine();

        int deathStage = Global.State.CavesClosed ? 4 : Global.State.Deaths;

        if (HandleDeathMessage(deathStage, YouDied))
        {
            return false;
        }

        var yes = YesOrNo.Ask();

        if (yes)
        {
            if (HandleReincarnationMessage(Global.State.Deaths, YouDied))
            {
                return false;
            }

            Reincarnate();
            CurrentRoom.Look(true);
            return true;
        }
        else
        {
            HandleDeclineReincarnation(Global.State.Deaths, YouDied);
            return false;
        }
    }

    private bool HandleDeathMessage(int deathStage, string youDied)
    {
        switch (deathStage)
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
            case 4:
                Output.Print("It looks as though you're dead. Well, seeing as how it's so close to closing time anyway, I think we'll just call it a day.");
                GameOver.Display(youDied);
                Context.Story.IsDone = true;
                return true;
        }

        return false;
    }

    private bool HandleReincarnationMessage(int deaths, string youDied)
    {
        switch (deaths)
        {
            case 1:
                Output.Print(
                    "All right. But don't blame me if something goes wr......" +
                    "\n\n---POOF!!---\n\n" +
                    "You are engulfed in a cloud of orange smoke. " +
                    "Coughing and gasping, you emerge from the smoke and find that you're....");
                break;
            case 2:
                Output.Print("Okay, now where did I put my orange smoke?.... >POOF!<" +
                "\n\nEverything disappears in a dense cloud of orange smoke.");
                break;
            case 3:
                Output.Print("Okay, if you're so smart, do it yourself! I'm leaving!");
                GameOver.Display(youDied);
                Context.Story.IsDone = true;
                return true;
        }

        return false;
    }

    private void HandleDeclineReincarnation(int deaths, string youDied)
    {
        switch (deaths)
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

        GameOver.Display(youDied);
        Context.Story.IsDone = true;
    }

    private static void Reincarnate()
    {
        int score = -10;

        var dwarf = Objects.Get<Dwarf>();
        
        if (dwarf.Location == Player.Location)
        {
            dwarf.Remove();
        }

        // lamp is turned off and moved to "At End of Road"
        var lamp = Objects.Get<BrassLantern>();
        lamp.On = false;
        lamp.Light = false;
        lamp.MoveTo<EndOfRoad>();

        // remaining inventory items are dropped in the room where you died
        foreach (var obj in Inventory.Items.ToList())
        {
            if (obj is Treasure)
            {
                score -= 5;
            }

            obj.MoveToLocation();
        }

        Player.Location = Rooms.Get<InsideBuilding>();

        Score.Add(score, true);
    }
}
