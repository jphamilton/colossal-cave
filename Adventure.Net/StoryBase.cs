using Adventure.Net.Utilities;
using System.Linq;

namespace Adventure.Net;

public abstract class StoryBase : IStory
{
    public string Story { get; set; }
    public bool IsDone { get; set; }
    public int Moves { get; set; } = 0;
    public int CurrentScore { get; set; } = 0;
    public int PossibleScore { get; set; } = 0;

    protected abstract void Start();

    protected StoryBase()
    {
        IsDone = false;
    }

    public void Initialize()
    {
        Objects.Load(this);
        Verbs.Load();

        foreach (var obj in Objects.All.Where(x => x is Room))
        {
            obj.Initialize();
        }

        foreach (var obj in Objects.All.Where(x => x is not Room))
        {
            obj.Initialize();
        }

        Start();
    }

}
