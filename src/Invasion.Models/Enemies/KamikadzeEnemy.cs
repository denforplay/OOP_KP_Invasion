using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Enemies
{
    /// <summary>
    /// Represents kamikadze enemy who push player without weapon
    /// </summary>
    public class KamikadzeEnemy : EnemyBase
    {
        /// <summary>
        /// Kamikadze enemy constructor
        /// </summary>
        /// <param name="components">List of components</param>
        /// <param name="enemyConfiguration"></param>
        /// <param name="layer">Enemy layer</param>
        public KamikadzeEnemy(List<IComponent> components, EnemyConfiguration enemyConfiguration, Layer layer = Layer.Enemy) : base(components, enemyConfiguration, layer)
        {
        }
    }
}
