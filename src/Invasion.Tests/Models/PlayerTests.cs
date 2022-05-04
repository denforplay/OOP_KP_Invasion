using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;
using Xunit;

namespace Invasion.Tests.Models
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerCreateTest()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            Assert.Equal(player.CurrentHealthPoints, player.MaxHealthPoint);
            Assert.Equal(10, player.MaxHealthPoint);
            Assert.Equal(1, player.Speed);
        }

        [Fact]
        public void TakeDamage_LessThanMaxHealth_PlayerTest()
        {
            bool isHealthChanged = false;
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            player.OnHealthChanged += _ => isHealthChanged = true;
            player.TakeDamage(5);
            Assert.Equal(true, isHealthChanged);
            Assert.Equal(5, player.CurrentHealthPoints);
        }

        [Fact]
        public void TakeDamage_HigherOrEqualThanMaxHealth_PlayerTest()
        {
            bool isDestroyed = false;
            Player player = new Player(null, new PlayerConfiguration(1, 10));

            player.OnDestroyed += () => isDestroyed = true;
            player.TakeDamage(10);
            Assert.Equal(true, isDestroyed);
        }

        [Fact]
        public void SetHealth_PlayerTest()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            bool isHealthChanged = false;
            player.OnHealthChanged += _ => isHealthChanged = true;
            player.SetHealth(100);
            Assert.Equal(100, player.CurrentHealthPoints);
            Assert.Equal(true, isHealthChanged);
        }

        [Fact]
        public void SlowedPlayerDecoratorTest()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            player = new SlowedPlayer(player, player.Components, new PlayerConfiguration(1, 10));
            Assert.Equal(0.5, player.Speed);
        }
    }
}