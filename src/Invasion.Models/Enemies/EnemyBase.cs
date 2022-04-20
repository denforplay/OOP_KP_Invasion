using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Interfaces;

namespace Invasion.Models.Enemies;

/// <summary>
/// Represent base enemy entity
/// </summary>
public abstract class EnemyBase : GameObject, IHealthable, ICostable
{
    /// <summary>
    /// Event called when enemy health changed
    /// </summary>
    public event Action<int>? OnHealthChanged;

    /// <summary>
    /// Max enemy health
    /// </summary>
    public int MaxHealthPoint { get; set; }

    /// <summary>
    /// Current enemy health
    /// </summary>
    public int CurrentHealthPoints { get; set; }

    /// <summary>
    /// Enemy speed
    /// </summary>
    public float Speed { get; set; }
    
    /// <summary>
    /// Enemy base constructir
    /// </summary>
    /// <param name="components">Enemy components</param>
    /// <param name="enemyConfiguration">Enemy configuration</param>
    /// <param name="layer">Enemy layer</param>
    public EnemyBase(List<IComponent> components, EnemyConfiguration enemyConfiguration, Layer layer = Layer.Default) : base(components, layer)
    {
        Cost = enemyConfiguration.Cost;
        MaxHealthPoint = enemyConfiguration.MaxHealth;
        CurrentHealthPoints = enemyConfiguration.MaxHealth;
        Speed = enemyConfiguration.Speed;
    }

    /// <summary>
    /// Take damage method
    /// </summary>
    /// <param name="damage">Damage value</param>
    public void TakeDamage(int damage)
    {
        CurrentHealthPoints -= damage;
        OnHealthChanged?.Invoke(CurrentHealthPoints);
    }

    /// <summary>
    /// Set enemy health
    /// </summary>
    /// <param name="health">Health value</param>
    public void SetHealth(int health)
    {
        CurrentHealthPoints = health;
        OnHealthChanged?.Invoke(health);
    }

    /// <summary>
    /// Enemy cost
    /// </summary>
    public int Cost { get; set; }
}