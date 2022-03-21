using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Configurations;
using Invasion.Models.Weapons;

namespace Invasion.Models;

public class Player : GameObject, IHealthable
{
    private IWeapon _weapon;
    public IWeapon Weapon;
    public event Action<int>? OnHealthChanged;
    public int MaxHealthPoint { get; set; }
    public int CurrentHealthPoints { get; set; }
    
    public Player(List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Default) : base(components, layer)
    {
        CurrentHealthPoints = playerConfig.MaxHealth;
        MaxHealthPoint = playerConfig.MaxHealth;
    }

    public void SetWeapon(IWeapon weapon)
    {
        if (weapon is null)
        {
            throw new ArgumentNullException();
        }

        _weapon = weapon;
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