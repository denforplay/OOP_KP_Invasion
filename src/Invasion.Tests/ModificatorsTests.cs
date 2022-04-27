using Invasion.Engine;
using Invasion.Engine.Graphics;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;
using Invasion.Models.Factories;
using Invasion.Models.Factories.ModificatorFactories;
using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Modificators;
using Invasion.Models.Modificators.Traps;
using Invasion.Models.Players.Decorator;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Firearms;
using Xunit;

namespace Invasion.Tests
{
    public class ModificatorsTests
    {
        private WeaponFactory _weaponFactory;
        private IModelFactory<ModificatorBase> _modificatorsFactory;
        private IGraphicProvider _graphicProvider;
        private BulletSystem _bulletSystem;

        public ModificatorsTests()
        {
            _graphicProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            _bulletSystem = new BulletSystem();
            _weaponFactory = new WeaponFactory(null, _bulletSystem, _graphicProvider);
            Compose();
        }

        [Fact]
        public void HighReloadWeapon_ApplyOnPlayer_Unapplied()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            _modificatorsFactory = new HigherReloadWeaponBonusFactory(_graphicProvider, null);
            var bonus = _modificatorsFactory.Create();
            bonus.Apply(player);
            Assert.True(player is not PlayerDecorator);
        }

        [Fact]
        public void HighReloadWeapon_ApplyOnWeapon_Applied()
        {
            var weaponModel = _weaponFactory.Create<Pistol>(null);
            var weaponDecorator = new WeaponBaseDecorator(weaponModel, weaponModel.Configuration, weaponModel.Components);
            _modificatorsFactory = new HigherReloadWeaponBonusFactory(_graphicProvider, null);
            var bonus = _modificatorsFactory.Create();
            bonus.Apply(weaponDecorator);
            Assert.True(weaponDecorator.Weapon is FasterWeaponBaseDecorator);
        }

        [Fact]
        public void LowerReloadWeapon_ApplyOnWeapon_Applied()
        {
            var weaponModel = _weaponFactory.Create<Pistol>(null);
            var weaponDecorator = new WeaponBaseDecorator(weaponModel, weaponModel.Configuration, weaponModel.Components);
            _modificatorsFactory = new LowerReloadWeaponBonusFactory(_graphicProvider, null);
            var bonus = _modificatorsFactory.Create();
            bonus.Apply(weaponDecorator);
            Assert.True(weaponDecorator.Weapon is LowerReloadWeaponDecorator);
        }

        [Fact]
        public void HigherFireRate_ApplyOnWeapon_Applied()
        {
            var weaponModel = _weaponFactory.Create<Pistol>(null);
            var weaponDecorator = new WeaponBaseDecorator(weaponModel, weaponModel.Configuration, weaponModel.Components);
            _modificatorsFactory = new HigherFireRateBonusFactory(_graphicProvider, null);
            var bonus = _modificatorsFactory.Create();
            bonus.Apply(weaponDecorator);
            Assert.True(weaponDecorator.Weapon is HigherFireRateWeponDecorator);
        }

        [Fact]
        public void SlowTrap_ApplyOnPlayer_Applied()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            var playerDecorator = new PlayerDecorator(player, player.Components, player.Configuration);
            float higherSpeed = playerDecorator.Speed;
            _modificatorsFactory = new SlowTrapFactory(_graphicProvider, null);
            var bonus = _modificatorsFactory.Create();
            bonus.Apply(playerDecorator);
            float lowerSpeed = playerDecorator.Speed;
            Assert.True(playerDecorator.Player is SlowedPlayer);
            Assert.True(lowerSpeed < higherSpeed);
        }

        [Fact]
        public void FreezeTrap_ApplyOnPlayer_Applied()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            var playerDecorator = new PlayerDecorator(player, player.Components, player.Configuration);
            _modificatorsFactory = new FreezeTrapFactory(_graphicProvider, null);
            var bonus = _modificatorsFactory.Create();
            bonus.Apply(playerDecorator);
            Assert.True(playerDecorator.Player is FreezedPlayer);
            Assert.Equal(0, playerDecorator.Speed);
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
            _graphicProvider.LoadBitmap(@"Sources\speedWeaponBonus.png");
            _graphicProvider.LoadBitmap(@"Sources\slowedWeaponBonus.png");
            _graphicProvider.LoadBitmap(@"Sources\removeHealthTrap.png");
            _graphicProvider.LoadBitmap(@"Sources\lowerReload.png");
            _graphicProvider.LoadBitmap(@"Sources\higherReload.png");
            _graphicProvider.LoadBitmap(@"Sources\moreDamageBonus.png");
            _graphicProvider.LoadBitmap(@"Sources\slowTrap.png");
            _graphicProvider.LoadBitmap(@"Sources\freezeTrap.png");
            _graphicProvider.LoadBitmap(@"Sources\kamikadzeEnemy.png");
            _graphicProvider.LoadBitmap(@"Sources\beatingEnemy.png");
        }
    }
}
