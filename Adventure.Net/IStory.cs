namespace Adventure.Net;

public interface IStory
{
    bool IsDone { get; set; }
    string Name { get; set; }
    int Moves { get; set; }
    int CurrentScore { get; set; }
    int PossibleScore { get; set; }
    void Initialize();
    bool Verbose { get; set; } // will use for verbose/brief modes for now. Not sure I will support superbrief
}