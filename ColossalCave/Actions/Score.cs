using Adventure.Net;
using System;

namespace ColossalCave.Actions
{
    public class Score : Verb
    {
        public Score()
        {
            Name = "score";
        }

        public bool Expects()
        {
            Print($"You have so far scored {Context.Story.CurrentScore} out of a possible {Context.Story.PossibleScore}, in {Context.Story.Moves} turns, earning you the rank of {GetRank()}");
            return true;
        }

        public static string GetRank()
        {
            var score = Context.Story.CurrentScore;
            if (score >= 348) return "Grandmaster Adventurer!";
            if (score >= 330) return "Master, first class.";
            if (score >= 300) return "Master, second class.";
            if (score >= 200) return "Junior Master.";
            if (score >= 130) return "Seasoned Adventurer.";
            if (score >= 100) return "Experienced Adventurer.";
            if (score >= 35) return "Adventurer.";
            if (score >= 10) return "Novice.";
            
            return "Amateur.";
        }

        public static void Add(int value, bool display = false)
        {
            var currentScore = Context.Story.CurrentScore;

            Context.Story.CurrentScore += value;
            
            if (display)
            {
                var direction = Context.Story.CurrentScore > currentScore ? "up" : "down";
                var output = $"\r\n[the score has just gone {direction} by {Math.Abs(value)} points.]\r\n";
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
}
