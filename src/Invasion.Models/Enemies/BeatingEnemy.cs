using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Enemies
{
    /// <summary>
    /// Represents enemy who uses melee weapon
    /// </summary>
    public class BeatingEnemy : EnemyBase
    {
        /// <summary>
        /// Beating enemy constructor
        /// </summary>
        /// <param name="components">List of components</param>
        /// <param name="enemyConfiguration">Enemy configuration</param>
        /// <param name="layer">Enemy layer</param>
        public BeatingEnemy(List<IComponent> components, EnemyConfiguration enemyConfiguration, Layer layer = Layer.Default) : base(components, enemyConfiguration, layer)
        {
        }
    }
}
