using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using SharpDX;

namespace Invasion.Models.Collisions
{
    public class CollisionRecords
    {
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided<GameObject, GameObject>((g1, g2) =>
            {
                if (g1.Layer == Layer.Player && g2.Layer == Layer.Border)
                {
                    if (g1.TryTakeComponent(out RigidBody2D rigidBody2D))
                    {
                        rigidBody2D.SetSpeed(Vector2.Zero);
                    }
                }
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}