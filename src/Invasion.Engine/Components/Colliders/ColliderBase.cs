using Invasion.Core.Interfaces;
using Invasion.Models.Collisions;

namespace Invasion.Engine.Components.Colliders
{
    public abstract class ColliderBase : IComponent
    {
        public GameObject ColliderObject { get; private set; }
        public CollisionController Collisions { get; private set; }

        public ColliderBase(CollisionController collisions, GameObject colliderObject)
        {
            ColliderObject = colliderObject;
            Collisions = collisions;
        }

        public abstract bool IsInside(ColliderBase other);
        public void TryCollide(ColliderBase other)
        {
            if (other.ColliderObject.TryTakeComponent<Transform>(out var transform))
            {
                if (IsInside(other))
                    Collisions.TryCollide((ColliderObject, other.ColliderObject));
            }
        }
    }
}
