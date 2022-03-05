using Invasion.Core.Interfaces;
using Invasion.Engine.Collisions;
using Invasion.Models.Collisions;
using SharpDX;

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
                {
                    if (ColliderObject.TryTakeComponent(out RigidBody2D thisRigidBody) &&
                        other.ColliderObject.TryTakeComponent(out RigidBody2D otherRigidBody))
                    {
                        if (CollisionMatrix.IsCollided(ColliderObject.Layer, other.ColliderObject.Layer))
                        {
                            if (ColliderObject.TryTakeComponent(out Transform g1transform) 
                                && ColliderObject.TryTakeComponent(out RigidBody2D rigidBody))
                            {
                                int x = 0, y = 0;
                                if (rigidBody.Speed.X < 0)
                                    x = 1;
                                else if (rigidBody.Speed.X > 0)
                                    x = -1;

                                if (rigidBody.Speed.Y < 0)
                                    y = 1;
                                else if (rigidBody.Speed.Y > 0)
                                    y = -1;

                                g1transform.Position = new Vector3(g1transform.Position.X + (other as BoxCollider2D).Size.Width/20f * x, g1transform.Position.Y + (other as BoxCollider2D).Size.Height/20f * y, g1transform.Position.Z);
                            }
                        }
                    }
                    else
                    {
                        Collisions.TryCollide((ColliderObject, other.ColliderObject));
                    }
                }
            }
        }
    }
}
