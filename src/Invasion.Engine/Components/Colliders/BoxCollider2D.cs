using Invasion.Models.Collisions;

namespace Invasion.Engine.Components.Colliders
{
    public class BoxCollider2D : ColliderBase
    {
        private Size _size;

        public BoxCollider2D(CollisionController collisions, GameObject colliderObject, Size size) : base(collisions, colliderObject)
        {
            _size = size;
        }

        public override bool IsInside(ColliderBase other)
        {
            if (ColliderObject.TryTakeComponent(out Transform transform) && other.ColliderObject.TryTakeComponent(out Transform otherTransform))
            {
                return otherTransform.Position.X <= transform.Position.X + _size.Width / 2f && otherTransform.Position.X >= transform.Position.X - _size.Width / 2f
                    && otherTransform.Position.Y <= transform.Position.Y + _size.Height / 2f && otherTransform.Position.Y >= transform.Position.Y - _size.Height / 2f;
            }

            return false;
        }
    }
}
