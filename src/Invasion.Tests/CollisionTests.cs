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
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using Invasion.Models.Modificators.Traps;
using Invasion.Models.Decorator;
using Invasion.Models.Weapons.Melee;
using Invasion.Models.Modificators.Bonuses;
using Invasion.Models.Weapons.Decorator;

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
            _collisionRecords = new CollisionRecords(new BulletSystem(), new EnemySystem(), new ModificatorSystem());
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

        [Fact] 
        public void CollisionsListTest()
        {
            GameObject testObject1 = new GameObject(new List<IComponent> { new RigidBody2D(), new Transform() }, Layer.Player);
            GameObject testObject2 = new GameObject(new List<IComponent> { new RigidBody2D(), new Transform() }, Layer.Border);
            Collisions collisions = new Collisions();
            collisions.TryBind(testObject1, testObject2);
            Assert.Single(collisions.CollisionPairs);
        }

        [Fact]
        public void TestPlayerEnemyCollision()
        {
            bool isPlayerDestroyed = false;
            Player player = new Player(null, new PlayerConfiguration(3, 3));
            player.OnDestroyed += () => isPlayerDestroyed = true;
            KamikadzeEnemy enemy = new KamikadzeEnemy(null, new EnemyConfiguration(3, 3, 3));
            _collisionController.TryCollide((player, enemy));
            Assert.True(isPlayerDestroyed);
        }


        [Fact]
        public void TestTrapPlayerCollision()
        {
            Player player = new Player(null, new PlayerConfiguration(3, 3));
            PlayerDecorator playerDecorator = new PlayerDecorator(player, null, new PlayerConfiguration(3, 3));
            FreezeTrap trap = new FreezeTrap(null, new ModificatorConfiguration(1));
            _collisionController.TryCollide((playerDecorator, trap));
            Assert.True(trap.IsApplied);
        }

        [Fact]
        public void TestBonusWeaponCollision()
        {
            Player player = new Player(null, new PlayerConfiguration(3, 3));
            Knife knife = new Knife(player, new WeaponConfiguration(3, 3, 3), null);
            WeaponBaseDecorator weaponBaseDecorator = new WeaponBaseDecorator(knife, new WeaponConfiguration(3, 3, 3), null);
            HigherFIreRateWeaponBonus bonus = new HigherFIreRateWeaponBonus(null, new ModificatorConfiguration(1));
            _collisionController.TryCollide((weaponBaseDecorator, bonus));
            Assert.True(bonus.IsApplied);
        }

        [Fact]
        public void TestKnifePlayerCollisionWithoutAttack()
        {
            bool isPlayerDestroyed = false;
            KamikadzeEnemy enemy = new KamikadzeEnemy(null, new EnemyConfiguration(3, 3, 3));
            Player player = new Player(null, new PlayerConfiguration(3, 3));
            PlayerDecorator playerDecorator = new PlayerDecorator(player, null, new PlayerConfiguration(3, 3));
            player.OnDestroyed += () => isPlayerDestroyed = true;
            Knife knife = new Knife(enemy, new WeaponConfiguration(3, 3, 3), null);
            WeaponBaseDecorator weaponBaseDecorator = new WeaponBaseDecorator(knife, new WeaponConfiguration(3, 3, 3), null);
            _collisionController.TryCollide((weaponBaseDecorator, playerDecorator));
            Assert.False(isPlayerDestroyed);
        }


        [Fact]
        public void TestKnifeEnemyCollisionWithoutAttack()
        {
            bool isEnemyDestroyed = false;
            KamikadzeEnemy enemy = new KamikadzeEnemy(null, new EnemyConfiguration(3, 3, 3));
            Player player = new Player(null, new PlayerConfiguration(3, 3));
            PlayerDecorator playerDecorator = new PlayerDecorator(player, null, new PlayerConfiguration(3, 3));
            enemy.OnDestroyed += () => isEnemyDestroyed = true;
            Knife knife = new Knife(playerDecorator, new WeaponConfiguration(3, 3, 3), null);
            WeaponBaseDecorator weaponBaseDecorator = new WeaponBaseDecorator(knife, new WeaponConfiguration(3, 3, 3), null);
            _collisionController.TryCollide((weaponBaseDecorator, enemy));
            Assert.False(isEnemyDestroyed);
        }
    }
}
