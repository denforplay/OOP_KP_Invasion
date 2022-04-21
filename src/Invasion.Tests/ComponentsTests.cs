using Invasion.Engine.Components;
using System.Numerics;
using Xunit;

namespace Invasion.Tests
{
    public class ComponentsTests
    {
        [Fact]
        public void CreateTransformTest()
        {
            Transform transform = new Transform
            {
                Position = new Vector3(1, 1, 1),
                Rotation = new Vector3(45, 90, 45),
                Scale = new Vector3(1, 1, 1)
            };

            Assert.Equal(transform.Position, new Vector3(1, 1, 1));
            Assert.Equal(transform.Rotation, new Vector3(45, 90, 45));
            Assert.Equal(transform.Position, new Vector3(1, 1, 1));
        }

        [Fact]
        public void CreateRigidBodyTest()
        {
            RigidBody2D rigidBody = new RigidBody2D
            {
                Speed = new Vector2(5f, 5f)
            };

            Assert.Equal(rigidBody.Speed, new Vector2(5f, 5f));
        }

    }
}
