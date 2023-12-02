using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Adventure.Net.Actions;

public class Restore : ForwardTokens
{
    public Restore()
    {
        Name = "restore";
        Synonyms.Are("load");
        GameVerb = true;
    }

    public override bool Handle(List<string> tokens)
    {
        if (tokens == null || tokens.Count == 0)
        {
            return Print("A file name is required.");
        }

        var file = Path.GetFileNameWithoutExtension(tokens[0]) + ".sav";
        var dir = Path.GetDirectoryName(Context.Story.GetType().Assembly.Location);
        var path = Path.Combine(dir, file);

        if (!File.Exists(path))
        {
            return Print("File not found.");
        }

        var json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            IgnoreReadOnlyProperties = true,
            PropertyNameCaseInsensitive = true,
        };

        var game = JsonSerializer.Deserialize<SaveGame>(json, options);

        foreach (var w in game.O)
        {
            SaveObjectConverter.Restore(w);
        }

        Context.Story.Location = (Room)Objects.All.Single(x => x.Id == game.L);
        Context.Story.Moves = game.M;
        Context.Story.CurrentScore = game.CS;

        CurrentRoom.Look(true);

        return true;
    }
}
