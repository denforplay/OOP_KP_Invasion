using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Decorator;

public class PlayerDecorator : Player
{
    protected Player _player;
    public Player Player => _player;
    public override float Speed { get => _player.Speed; }

    public PlayerDecorator(Player player, List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(components, playerConfig, layer)
    {
        _player = player;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}