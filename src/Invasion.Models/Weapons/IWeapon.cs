using Invasion.Core.Interfaces;
using SharpDX;

namespace Invasion.Models.Weapons;

public interface IWeapon
{
    void GiveDamage(IHealthable healthable);
    float ReloadTime { get; }
    void Attack(Vector2 direction);
    void Update();
}