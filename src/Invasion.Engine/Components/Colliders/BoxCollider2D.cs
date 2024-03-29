﻿using Invasion.Models.Collisions;
using System.Numerics;

namespace Invasion.Engine.Components.Colliders
{
    /// <summary>
    /// Represents 2d box collider to interact with other colliders
    /// </summary>
    public class BoxCollider2D : ColliderBase
    {
        private Size _size;

        /// <summary>
        /// ColliderSize
        /// </summary>
        public Size Size => _size;

        /// <summary>
        /// Box collider 2d constructor
        /// </summary>
        /// <param name="collisions">Collisions controller</param>
        /// <param name="colliderObject">Collider object</param>
        /// <param name="size">Collider size</param>
        public BoxCollider2D(CollisionController collisions, GameObject colliderObject, Size size) : base(collisions, colliderObject)
        {
            _size = size;
        }

        public override bool IsInside(ColliderBase other)
        {
            if (ColliderObject.TryTakeComponent(out Transform transform) && other.ColliderObject.TryTakeComponent(out Transform otherTransform))
            {
                return other.IsPointInsideCollider(new Vector2(transform.Position.X, transform.Position.Y));
            }

            return false;
        }

        public override bool IsPointInsideCollider(Vector2 point)
        {
            if (ColliderObject.TryTakeComponent(out Transform transform))
            {
                return transform.Position.X + Size.Width / 2f >= point.X &&
                       transform.Position.X - Size.Width / 2f <= point.X &&
                       transform.Position.Y + Size.Height / 2f >= point.Y &&
                       transform.Position.Y - Size.Height / 2f <= point.Y;
            }

            return false;
        }
    }
}
