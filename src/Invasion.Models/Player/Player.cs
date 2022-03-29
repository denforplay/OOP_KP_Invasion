using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Configurations;

namespace Invasion.Models;

public class Player : GameObject, IHealthable
{
    public event Action<int>? OnHealthChanged;
    public int MaxHealthPoint { get; set; }
    public int CurrentHealthPoints { get; set; }
    public virtual float Speed { get; set; }
    
    public Player(List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(components, layer)
    {
        Speed = 1f;
        CurrentHealthPoints = playerConfig.MaxHealth;
        MaxHealthPoint = playerConfig.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealthPoints -= damage;
        OnHealthChanged?.Invoke(CurrentHealthPoints);
    }

    public void SetHealth(int health)
    {
        CurrentHealthPoints = health;
        OnHealthChanged?.Invoke(CurrentHealthPoints);
    }
}