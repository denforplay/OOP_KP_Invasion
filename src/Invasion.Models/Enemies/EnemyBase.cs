using System.Diagnostics;
using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Enemies;

public abstract class EnemyBase : GameObject, IHealthable
{
    public EnemyBase(List<IComponent> components, Layer layer = Layer.Default) : base(components, layer)
    {
    }

    public event Action<int>? OnHealthChanged;
    public int MaxHealthPoint { get; set; }
    public int CurrentHealthPoints { get; set; }
    public void TakeDamage(int damage)
    {
        CurrentHealthPoints -= damage;
        OnHealthChanged?.Invoke(CurrentHealthPoints);
    }

    public void SetHealth(int health)
    {
        throw new NotImplementedException();
    }
}