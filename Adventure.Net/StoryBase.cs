using Adventure.Net.Utilities;
using System.Linq;

namespace Adventure.Net;

public abstract class StoryBase : IStory
{
    public string Story { get; set; }
    public Room Location { get; set; }
    public bool IsDone { get; set; }
    public int Moves { get; set; } = 0;
    public int CurrentScore { get; set; } = 0;
    public int PossibleScore { get; set; } = 0;

    protected abstract void Start();

    public virtual void AfterTurn()
    {

    }

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

    public virtual void Quit()
    {
        if (YesOrNo.Ask("Are you sure you want to quit?"))
        {
            IsDone = true;
        }
    }


}
