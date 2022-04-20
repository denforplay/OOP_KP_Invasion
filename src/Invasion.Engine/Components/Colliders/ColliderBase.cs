using Invasion.Engine.Collisions;
using Invasion.Engine.Interfaces;
using Invasion.Models.Collisions;
using System.Numerics;

namespace Invasion.Engine.Components.Colliders
{
    /// <summary>
    /// Represents base collider component
    /// </summary>
    public abstract class ColliderBase : IComponent
    {
        /// <summary>
        /// Collider gameobject
        /// </summary>
        public GameObject ColliderObject { get; private set; }

        /// <summary>
        /// Collisions controller
        /// </summary>
        public CollisionController Collisions { get; private set; }

        /// <summary>
        /// Collider base constructor
        /// </summary>
        /// <param name="collisions">Collisions controller</param>
        /// <param name="colliderObject">Collider object</param>
        public ColliderBase(CollisionController collisions, GameObject colliderObject)
        {
            ColliderObject = colliderObject;
            Collisions = collisions;
        }

        /// <summary>
        /// Get collision point between two colliders
        /// </summary>
        /// <param name="toPoint">Direction in which find collide point</param>
        /// <returns>Collision point</returns>
        public abstract Vector2 GetCollisionPoint(Vector2 toPoint);

        /// <summary>
        /// Checks if one collider inside another
        /// </summary>
        /// <param name="other">Other collider</param>
        /// <returns>True if colliders inside each another</returns>
        public abstract bool IsInside(ColliderBase other);

        /// <summary>
        /// Checks if point is inside collider
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>True if point inside collider, other returns false</returns>
        public abstract bool IsPointInsideCollider(Vector2 point);

        /// <summary>
        /// Try to collide with other collider
        /// </summary>
        /// <param name="other">Other collider</param>
        public void TryCollide(ColliderBase other)
        {
            if (IsInside(other))
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

                        g1transform.Position = new Vector3(g1transform.Position.X + x * 0.1f,
                            g1transform.Position.Y + 0.1f * y, g1transform.Position.Z);
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
