using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons
{
    public class EmptyWeapon : WeaponBase
    {
        public EmptyWeapon(List<IComponent> components, Layer layer = Layer.Default) : base(components, layer)
        {
        }

        public override GameObject Parent => null;

        public override void Attack(Vector2 direction)
        {
        }

        public override void GiveDamage(IHealthable healthable)
        {
        }

        public override void Update()
        {
        }
    }
}
