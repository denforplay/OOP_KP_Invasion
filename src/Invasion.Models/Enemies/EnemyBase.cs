using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Configurations;

namespace Invasion.Models.Enemies;

public abstract class EnemyBase : GameObject, IHealthable, ICostable
{
    public event Action<int>? OnHealthChanged;
    public int MaxHealthPoint { get; set; }
    public int CurrentHealthPoints { get; set; }
    
    public EnemyBase(List<IComponent> components, EnemyConfiguration enemyConfiguration, Layer layer = Layer.Default) : base(components, layer)
    {
        Cost = enemyConfiguration.Cost;
        MaxHealthPoint = enemyConfiguration.MaxHealth;
        CurrentHealthPoints = enemyConfiguration.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealthPoints -= damage;
        OnHealthChanged?.Invoke(CurrentHealthPoints);
    }

    public void SetHealth(int health)
    {
        CurrentHealthPoints = health;
        OnHealthChanged?.Invoke(health);
    }

    public int Cost { get; set; }
}