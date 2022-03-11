using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Collisions
{
    public class CollisionRecords
    {
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided<GameObject, GameObject>((g1, g2) =>
            {
                
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}