using System.Diagnostics;

namespace Adventure.Net.Things;

[DebuggerDisplay("(yourself) in {Location}")]
public class Player : Object
{
    private static Player _player;

    public Player()
    {
        Synonyms.Are("me");
        Animate = true;
    }

    public override void Initialize()
    {
        _player = Objects.Get<Player>();
    }

    public static new Room Location
    {
        get
        {
            return (Room)_player.Parent;
        }
        set
        {
            _player.Remove();
            _player.Parent = value;
            _player.Parent.Children.Add(_player);
        }
    }

    public static bool IsCarrying<T>() where T : Object => Inventory.Contains<T>();

    public static bool IsCarrying(Object obj) => Inventory.Contains(obj);
}
