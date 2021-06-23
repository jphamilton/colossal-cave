namespace ColossalCave.Places
{
    public class DeadEnd14 : BelowGround
    {
        public override void Initialize()
        {
            Name = "Dead End, near Vending Machine";
            Synonyms.Are("dead", "end", "near", "vending", "machine");
            Description =
                "You have reached a dead end. There is a massive vending machine here.\r\n\r\n" +
                "Hmmm... There is a message here scrawled in the dust in a flowery script.";

            NoDwarf = true;

            //UpTo<DifferentMaze2>();
            //OutTo<DifferentMaze2>();

            Scenic(x =>
            {
                x.Name = "message in the dust";
                x.Synonyms.Are("message", "scrawl", "writing", "script", "scrawled", "flowery");
                x.Description = "The message reads, \"This is not the maze where the pirate leaves his treasure chest.\"";
            });
        }
    }

}
