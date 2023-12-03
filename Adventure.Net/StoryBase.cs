using Adventure.Net.Things;
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
        // derived classes should modify any static library primitives or values in their constructor.
        // For example, adding values to IgnoredWords or ReplacedWords or new Compass directions.

        // instance variables should NOT be set here
    }

    public void Initialize()
    {
        IsDone = false;

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

        MovePlayer.To(Player.Location);
    }

}
