using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using Invasion.Models.Modificators.Bonuses;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Bullets;
using System.Linq;
using Xunit;

namespace Invasion.Tests
{
    public class SystemsTests
    {
        [Fact]
        public void ScoreSystemTest()
        {
            bool isEventCalled = false;
            ScoreSystem scoreSystem = new ScoreSystem();
            scoreSystem.OnScoreChanged += _ => isEventCalled = true;
            scoreSystem.AddScores(1);
            Assert.True(isEventCalled);
            Assert.Equal(1, scoreSystem.CurrentScore);
        }

        [Fact]
        public void EnemySystemTest()
        {
            EnemySystem enemySystem = new EnemySystem();
            var enemy = new ShootingEnemy(null, new EnemyConfiguration(5, 2f, 1));
            enemySystem.Work(enemy);
            Assert.Single(enemySystem.Entities);
            enemySystem.Update(0.01f);
            enemySystem.StopWork(enemy);
            Assert.Empty(enemySystem.Entities);
            enemySystem.Work(enemy);
            enemySystem.Work(enemy);
            Assert.Equal(2, enemySystem.Entities.Count());
            enemySystem.StopAll();
            Assert.Empty(enemySystem.Entities);
        }

        [Fact]
        public void BulletSystemTest()
        {
            BulletSystem bulletSystem = new BulletSystem();
            var bullet = new DefaultBullet();
            bulletSystem.Work(bullet);
            Assert.Single(bulletSystem.Entities);
            bulletSystem.Update(0.01f);
            bulletSystem.StopWork(bullet);
            Assert.Empty(bulletSystem.Entities);
            bulletSystem.Work(bullet);
            bulletSystem.Work(bullet);
            bulletSystem.Work(bullet);
            Assert.Equal(3, bulletSystem.Entities.Count());
            bulletSystem.StopAll();
            Assert.Empty(bulletSystem.Entities);
        }

        [Fact]
        public void ModificatorSystemTest()
        {
            ModificatorSystem bulletSystem = new ModificatorSystem();
            var modificator = new HigherFIreRateWeaponBonus(null, new ModificatorConfiguration(1000));
            bulletSystem.Work(modificator);
            Assert.Single(bulletSystem.Entities);
            bulletSystem.Update(0.01f);
            bulletSystem.StopWork(modificator);
            Assert.Empty(bulletSystem.Entities);
            bulletSystem.Work(modificator);
            bulletSystem.Work(modificator);
            bulletSystem.Work(modificator);
            Assert.Equal(3, bulletSystem.Entities.Count());
            bulletSystem.StopAll();
            Assert.Empty(bulletSystem.Entities);
        }
    }
}
