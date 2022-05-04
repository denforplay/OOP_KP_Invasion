using Invasion.Controller.Controllers;
using Invasion.Controller.Inputs;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;
using Invasion.View;
using Moq;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using Xunit;

namespace Invasion.Tests
{
    public class ComponentsTests
    {
        [Fact]
        public void CreateTransformTest()
        {
            Engine.Components.Transform transform = new Engine.Components.Transform
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

        [Fact]
        public void SceneTest()
        {
            Scene scene = new Scene();
            scene.Initialize();
            var gameobject = new GameObject(null);
            var gameObjectView = new GameObjectView(gameobject);
            scene.AddGameObject(gameobject);
            scene.AddView(gameObjectView);
            scene.Update();
            scene.FixedUpdate();
            scene.RemoveGameObject(gameobject);
            scene.RemoveView(gameObjectView);
            scene.Dispose();
            scene = new Scene(new List<GameObject> { gameobject }, new List<IController> { }, new List<IView> { gameObjectView });
        }

        [Fact]
        public void TestTimer()
        {
            Time.Start(60);
            float startTime = Time.DeltaTime;
            Time.Update();
            float endTime = Time.DeltaTime;
            Assert.NotEqual(startTime, endTime);
        }

        [Fact]
        public void TestScreen()
        {
            Assert.Equal(25, Screen.UnitsPerHeight);
        }
    }
}
