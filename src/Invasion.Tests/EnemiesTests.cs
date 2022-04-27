using Invasion.Engine;
using Invasion.Engine.Graphics;
using Invasion.Models;
using Invasion.Models.Collisions;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using Invasion.Models.Factories;
using Invasion.Models.Factories.EnemiesFactories;
using Invasion.Models.Systems;
using Xunit;

namespace Invasion.Tests
{
    public class EnemiesTests
    {
        private CollisionController _collisionController;
        private CollisionRecords _collisionRecords;
        private IModelFactory<EnemyBase> _enemyFactory;
        private IGraphicProvider _graphicProvider;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        private ModificatorSystem _modificatorSystem;

        public EnemiesTests()
        {
            _bulletSystem = new BulletSystem();
            _enemySystem = new EnemySystem();
            _collisionRecords = new CollisionRecords(_bulletSystem, _enemySystem, _modificatorSystem);
            _collisionController = new CollisionController(_collisionRecords.StartCollideValues);
            _graphicProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            Compose();
        }

        public void Compose()
        {
            _graphicProvider.LoadBitmap(@"Sources\background.bmp");
            _graphicProvider.LoadBitmap(@"Sources\character.png");
            _graphicProvider.LoadBitmap(@"Sources\topdownwall.png");
            _graphicProvider.LoadBitmap(@"Sources\pistol.png");
            _graphicProvider.LoadBitmap(@"Sources\knife.png");
            _graphicProvider.LoadBitmap(@"Sources\defaultBullet.png");
            _graphicProvider.LoadBitmap(@"Sources\shootingEnemy.png");
            _graphicProvider.LoadBitmap(@"Sources\speedBonus.png");
            _graphicProvider.LoadBitmap(@"Sources\slowTrap.png");
            _graphicProvider.LoadBitmap(@"Sources\kamikadzeEnemy.png");
            _graphicProvider.LoadBitmap(@"Sources\beatingEnemy.png");
        }

        [Fact]
        public void ShootingEnemy_CreateTest()
        {
            var configuration = new EnemyConfiguration(2, 1f, 2);
            _enemyFactory = new ShootingEnemyFactory(_graphicProvider, _collisionController, configuration);
            var shootingEnemy = _enemyFactory.Create() as ShootingEnemy;
            Assert.Equal(configuration.MaxHealth, shootingEnemy.CurrentHealthPoints);
            Assert.Equal(configuration.MaxHealth, shootingEnemy.MaxHealthPoint);
            Assert.Equal(configuration.Cost, shootingEnemy.Cost);
            Assert.Equal(configuration.Speed, shootingEnemy.Speed);
        }

        [Fact]
        public void BeatingEnemy_CreateTest()
        {
            var configuration = new EnemyConfiguration(health: 8, speed: 1.25f, cost: 1);
            _enemyFactory = new BeatingEnemyFactory(_graphicProvider, _collisionController, configuration);
            var beatingEnemy = _enemyFactory.Create() as BeatingEnemy;
            Assert.Equal(configuration.MaxHealth, beatingEnemy.CurrentHealthPoints);
            Assert.Equal(configuration.MaxHealth, beatingEnemy.MaxHealthPoint);
            Assert.Equal(configuration.Cost, beatingEnemy.Cost);
            Assert.Equal(configuration.Speed, beatingEnemy.Speed);
        }

        [Fact]
        public void KamikadzeEnemy_CreateTest()
        {
            var configuration = new EnemyConfiguration(health: 2, speed: 3f, cost: 1);
            _enemyFactory = new KamikadzeEnemyFactory(_graphicProvider, _collisionController, configuration);
            var kamikadzeEnemy = _enemyFactory.Create() as KamikadzeEnemy;
            Assert.Equal(configuration.MaxHealth, kamikadzeEnemy.CurrentHealthPoints);
            Assert.Equal(configuration.MaxHealth, kamikadzeEnemy.MaxHealthPoint);
            Assert.Equal(configuration.Cost, kamikadzeEnemy.Cost);
            Assert.Equal(configuration.Speed, kamikadzeEnemy.Speed);
        }

        [Fact]
        public void Enemy_TakeDamageTest()
        {
            var configuration = new EnemyConfiguration(2, 1f, 2);
            _enemyFactory = new ShootingEnemyFactory(_graphicProvider, _collisionController, configuration);
            var shootingEnemy = _enemyFactory.Create() as ShootingEnemy;
            shootingEnemy.TakeDamage(1);
            Assert.Equal(1, shootingEnemy.CurrentHealthPoints);
        }

        [Fact]
        public void Enemy_SetHealthTest()
        {
            bool isEventCalled = false;
            var configuration = new EnemyConfiguration(2, 1f, 2);
            _enemyFactory = new ShootingEnemyFactory(_graphicProvider, _collisionController, new EnemyConfiguration(2, 1f, 2));
            var shootingEnemy = _enemyFactory.Create() as ShootingEnemy;
            shootingEnemy.OnHealthChanged += _ => isEventCalled = true;
            shootingEnemy.SetHealth(1);
            Assert.Equal(1, shootingEnemy.CurrentHealthPoints);
            Assert.True(isEventCalled);
        }
    }
}
