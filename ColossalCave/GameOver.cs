using Adventure.Net;
using ColossalCave.Actions;

namespace ColossalCave;

public static class GameOver
{
    public static void Win()
    {
        Context.Story.IsDone = true;
        Display("*** Congratulations! ***");
    }

    public static void Display(string message)
    {
        var score = Context.Story.CurrentScore;
        var possible = Context.Story.PossibleScore;

        Output.Print("\n\n");
        Output.Print($"\t{message}");
        Output.Print("\n\n");
        Output.Print($"In that game you scored {score} points out of a possible {possible}, earning you the rank of {Score.GetRank()}");
    }
}
