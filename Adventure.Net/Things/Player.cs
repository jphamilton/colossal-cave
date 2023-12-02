using Adventure.Net.Places;

namespace Adventure.Net.Things;


public class Player : Object
{
    private static Player _player;

    public override void Initialize()
    {
        _player = Objects.Get<Player>();
    }

    public static Room Room => (Room)_player.Parent;
}
