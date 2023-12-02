using Adventure.Net;
using System;

namespace ColossalCave.Actions;

public class Score : Verb
{
    public Score()
    {
        Name = "score";
        GameVerb = true;
    }

    public bool Expects()
    {
        return Print($"You have so far scored {Context.Story.CurrentScore} out of a possible {Context.Story.PossibleScore}, in {Context.Story.Moves} turns, earning you the rank of {GetRank()}");
    }

    public static string GetRank()
    {
        var score = Context.Story.CurrentScore;
        switch (score)
        {
            case >= 348:
                return "Grandmaster Adventurer!";
            case >= 330:
                return "Master, first class.";
            case >= 300:
                return "Master, second class.";
            case >= 200:
                return "Junior Master.";
            case >= 130:
                return "Seasoned Adventurer.";
            case >= 100:
                return "Experienced Adventurer.";
            case >= 35:
                return "Adventurer.";
            case >= 10:
                return "Novice.";
            default:
                return "Amateur.";
        }
    }

    public static void Add(int value, bool display = false)
    {
        var currentScore = Context.Story.CurrentScore;

        Context.Story.CurrentScore += value;

        if (display)
        {
            var direction = Context.Story.CurrentScore > currentScore ? "up" : "down";
            var output = $"\n[the score has just gone {direction} by {Math.Abs(value)} points.]\n";
            if (Context.Current != null)
            {
                Context.Current.Print(output);
            }
            else
            {
                Output.Print(output);
            }
        }
    }

    public static int Current
    {
        get
        {
            return Context.Story.CurrentScore;
        }
    }
}
