using Adventure.Net.Things;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Adventure.Net.ActionRoutines;

public class Save : ForwardTokens
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        Converters = { new SaveObjectConverter() },
        IgnoreReadOnlyProperties = true,
        PropertyNameCaseInsensitive = true,
    };

    public Save()
    {
        Verbs = ["save"];
        IsGameVerb = true;
    }

    public override bool Handler(Object first, Object second = null) => false;

    public override bool Handle(List<string> tokens)
    {
        if (tokens == null || tokens.Count <= 1)
        {
            Print("A file name is required.");
            return true;
        }

        try
        {
            var file = Path.GetFileNameWithoutExtension(tokens[1]) + ".sav";
            var dir = Path.GetDirectoryName(Context.Story.GetType().Assembly.Location);
            var path = Path.Combine(dir, file);

            var objects = Objects.All.Where(x => x.Id != 0).Select(x => new SaveObject(x)).ToList();

            var saveGame = new SaveGame
            {
                O = objects,
                I = [.. Inventory.Items.Select(x => x.Id)],
                L = Player.Location.Id,
                M = Context.Story.Moves,
                CS = Context.Story.CurrentScore,
            };

            var json = JsonSerializer.Serialize(saveGame, SerializerOptions);

            File.WriteAllText(path, json);

            return Print("Ok.");
        }
        catch
        {
            return Print("Unable to save game.");
        }

    }

}
