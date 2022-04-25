using Invasion.Engine;
using Invasion.Engine.Graphics;
using Invasion.Models;
using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Melee;
using Moq;
using Xunit;

namespace Invasion.Tests
{
    public class WeaponTests
    {
        private WeaponFactory _weaponFactory;
        private IGraphicProvider _graphicProvider;

        public WeaponTests()
        {
            _graphicProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            Compose();
            _weaponFactory = new WeaponFactory(null, null, _graphicProvider);
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
        public void TestCreateKnife()
        {
            var weapon = _weaponFactory.Create<Knife>(null);
            Assert.True(weapon is Knife);
        }


        [Fact]
        public void TestCreatePistol()
        {
            var weapon = _weaponFactory.Create<Pistol>(null);
            Assert.True(weapon is Pistol);
        }

        [Fact]
        public void TestCreateEmptyWeapon()
        {
            var weapon = _weaponFactory.Create<EmptyWeapon>(null);
            Assert.True(weapon is EmptyWeapon);
        }
    }
}
