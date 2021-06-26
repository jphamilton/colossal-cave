using Adventure.Net;

namespace ColossalCave.Places
{
    public class DeadEndCrawl : BelowGround
    {
        public override void Initialize()
        {
            Name = "Dead End Crawl";
            Synonyms.Are("dead", "end", "crawl");
            Description = "This is a dead end crawl.";
            SouthTo<LargeLowRoom>();
            OutTo<LargeLowRoom>();
        }
    }
}
