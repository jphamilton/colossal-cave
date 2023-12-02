namespace Adventure.Net;

public interface IStory
{
    bool IsDone { get; set; }
    string Story { get; set; }
    int Moves { get; set; }
    int CurrentScore { get; set; }
    int PossibleScore { get; set; }
    void Initialize();
    void Quit();
}