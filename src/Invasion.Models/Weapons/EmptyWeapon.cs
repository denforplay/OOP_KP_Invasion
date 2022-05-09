using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons
{
    /// <summary>
    /// Represents empty weapon
    /// </summary>
    public class EmptyWeapon : WeaponBase
    {
        /// <summary>
        /// Empty weapon constructor
        /// </summary>
        /// <param name="configuration">Weapon configuration</param>
        /// <param name="components">Components</param>
        /// <param name="layer">Layer</param>
        public EmptyWeapon(WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(configuration, components, layer)
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
