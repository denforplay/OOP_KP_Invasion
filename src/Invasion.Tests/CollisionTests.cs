using Invasion.Engine.Components.Colliders;
using Invasion.Models;
using Invasion.Models.Collisions;
using Xunit;
using Moq;
using Invasion.Models.Systems;
using Invasion.Engine;
using Invasion.Engine.Components;
using System.Collections.Generic;
using Invasion.Engine.Interfaces;
using System.Numerics;

namespace Invasion.Tests
{
    public class CollisionTests
    {
        private CollisionController _collisionController;
        private CollisionRecords _collisionRecords;
        private GameObject _firstObject;
        private GameObject _secondObject;

        public CollisionTests()
        {
            _collisionRecords = new CollisionRecords(new Mock<BulletSystem>().Object, new Mock<EnemySystem>().Object, new ModificatorSystem());
            _collisionController = new CollisionController(_collisionRecords.StartCollideValues);
            _firstObject = new GameObject(new List<IComponent>
            {
                new RigidBody2D(),
                new Transform
                {
                    Position = new Vector3(5f, 5f, 0)
                },
            }, Layer.Player);

            _secondObject = new GameObject(new List<IComponent>
            {
                new RigidBody2D(),
                new Transform
                {
                    Position = new Vector3(4.5f, 4.5f, 0)
                },
            });;
        }

        [Fact]
        public void TestColliderCreation()
        {
            ColliderBase firstCollider = new BoxCollider2D(_collisionController, _firstObject, new System.Drawing.Size(2, 2));
            Assert.True(firstCollider is not null);
        }

        [Fact]
        public void IsInside_ValidData_ReturnsTrue()
        {
            ColliderBase firstCollider = new BoxCollider2D(_collisionController, _firstObject, new System.Drawing.Size(2, 2));
            ColliderBase secondCollider = new BoxCollider2D(_collisionController, _secondObject, new System.Drawing.Size(2, 2));
            Assert.True(firstCollider.IsInside(secondCollider));
        }

        [Fact]
        public void IsInside_ValidData_ReturnsFalse()
        {
            _firstObject.TryTakeComponent(out Transform transform);
            transform.Position = new Vector3(0, 0, 0);
            ColliderBase firstCollider = new BoxCollider2D(_collisionController, _firstObject, new System.Drawing.Size(2, 2));
            ColliderBase secondCollider = new BoxCollider2D(_collisionController, _secondObject, new System.Drawing.Size(2, 2));
            Assert.False(firstCollider.IsInside(secondCollider));
        }

        [Fact]
        public void TryCollide_ValidData()
        {
            ColliderBase firstCollider = new BoxCollider2D(_collisionController, _firstObject, new System.Drawing.Size(2, 2));
            ColliderBase secondCollider = new BoxCollider2D(_collisionController, _secondObject, new System.Drawing.Size(2, 2));
            firstCollider.TryCollide(secondCollider);
        }

        [Fact]
        public void TryCollide_ObjectWithCollidedLayers()
        {
            GameObject testObject1 = new GameObject(new List<IComponent> { new RigidBody2D(), new Transform()}, Layer.Player);
            GameObject testObject2 = new GameObject(new List<IComponent> { new RigidBody2D(), new Transform()}, Layer.Border);
            ColliderBase firstCollider = new BoxCollider2D(_collisionController, testObject1, new System.Drawing.Size(2, 2));
            ColliderBase secondCollider = new BoxCollider2D(_collisionController, testObject2, new System.Drawing.Size(2, 2));
            firstCollider.TryCollide(secondCollider);
        }
    }
}
