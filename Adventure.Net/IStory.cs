using System.Collections.Generic;

namespace Adventure.Net
{
    public interface IStory
    {
        bool IsDone { get; set; }
        Room Location { get; set; }
        string Story { get; set; }
        int Moves { get; set; }
        int CurrentScore { get; set; }
        int PossibleScore { get; set; }
        Dictionary<string, bool> Flags { get; }

        void Initialize();

        void Quit();

        void AfterTurn();
    }
}