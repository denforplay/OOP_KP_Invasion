using Invasion.Core.Interfaces;
using SharpDX;

namespace Invasion.Models.Weapons;

public interface IWeapon
{
    void Attack(Vector2 direction);
    void Update();
}