using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Adventure.Net.Actions;
public class Save : ForwardTokens
{
    public Save()
    {
        Name = "save";
        GameVerb = true;
    }

    public override bool Handle(List<string> tokens)
    {
        if (tokens == null || tokens.Count == 0)
        {
            Print("A file name is required.");
            return true;
        }

        try
        {
            var file = Path.GetFileNameWithoutExtension(tokens[0]) + ".sav";
            var dir = Path.GetDirectoryName(Context.Story.GetType().Assembly.Location);
            var path = Path.Combine(dir, file);

            var saveGame = new SaveGame
            {
                O = Objects.All.Select(x => new SaveObject(x)).ToList(),
                L = Context.Story.Location.Id,
                M = Context.Story.Moves,
                CS = Context.Story.CurrentScore
            };

            var options = new JsonSerializerOptions
            {
                Converters = { new SaveObjectConverter() },
                IgnoreReadOnlyProperties = true,
                PropertyNameCaseInsensitive = true,
            };

            var json = JsonSerializer.Serialize(saveGame, options);

            File.WriteAllText(path, json);

            return Print("Ok.");
        }
        catch
        {
            return Print("Unable to save game.");
        }

    }
}
