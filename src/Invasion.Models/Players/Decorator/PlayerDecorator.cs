using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Decorator;

/// <summary>
/// Class represents player decorator
/// </summary>
public class PlayerDecorator : Player
{
    protected Player _player;
    public Player Player => _player;
    public override float Speed { get => _player.Speed; }
    public override int CurrentHealthPoints { get; set; }

    public override bool CanDamage { get => _player.CanDamage; }

    /// <summary>
    /// Player decorator constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="components">Components</param>
    /// <param name="playerConfig">Player configuration</param>
    /// <param name="layer">Layer</param>
    public PlayerDecorator(Player player, List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(components, playerConfig, layer)
    {
        _player = player;
    }

    /// <summary>
    /// Set new player
    /// </summary>
    /// <param name="player">Player</param>
    public void SetPlayer(Player player)
    {
        _player = player;
    }
}