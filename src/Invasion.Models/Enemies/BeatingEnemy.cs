using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Configurations;

namespace Invasion.Models.Enemies
{
    public class BeatingEnemy : EnemyBase
    {
        public BeatingEnemy(List<IComponent> components, EnemyConfiguration enemyConfiguration, Layer layer = Layer.Enemy) : base(components, enemyConfiguration, layer)
        {
        }
    }
}
