using System.Collections.Generic;
using System.Numerics;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Graphics;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Melee;
using Xunit;

namespace Invasion.Tests.Models
{
    public class WeaponTests
    {
        private WeaponFactory _weaponFactory;
        private IGraphicProvider _graphicProvider;
        private BulletSystem _bulletSystem;

        public WeaponTests()
        {
            _bulletSystem = new BulletSystem();
            _graphicProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            Compose();
            _weaponFactory = new WeaponFactory(null, _bulletSystem, _graphicProvider);
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

        [Fact]
        public async void TestFirearmAttack()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            bool isBulletCreated = false;
            Pistol weapon = _weaponFactory.Create<Pistol>(player) as Pistol;
            _bulletSystem.OnStart += _ => isBulletCreated = true;
            weapon.Attack(new Vector2(1, 1));
            Assert.True(isBulletCreated);
        }

        [Fact]
        public void UpdatePistolTest()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            bool isBulletCreated = false;
            Pistol weapon = _weaponFactory.Create<Pistol>(player) as Pistol;
            player.TryTakeComponent(out Transform transform);
            transform.Position = new Vector3(1, 2, 3);
            weapon.Update();
            weapon.TryTakeComponent(out Transform weaponTransform);
            Assert.Equal(transform.Position, weaponTransform.Position);
            Assert.Equal(player, weapon.Parent);
        }

        [Fact]
        public void GiveDamagePistolTest()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            Pistol weapon = _weaponFactory.Create<Pistol>(player) as Pistol;
            weapon.GiveDamage(player);
            Assert.Equal(8, player.CurrentHealthPoints);
        }

        [Fact]
        public void TestMelleeAttack()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            Knife weapon = _weaponFactory.Create<Knife>(player) as Knife;
            weapon.Attack(new Vector2(1, 1));
            weapon.TryTakeComponent(out Transform transform);
            Assert.Equal(new Vector3(10, 10, 0), transform.Position);
            Assert.Equal(weapon.Parent, player);
        }


        [Fact]
        public void TestMelleeUpdate()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            Knife weapon = _weaponFactory.Create<Knife>(player) as Knife;
            player.TryTakeComponent(out Transform playerTransform);
            weapon.TryTakeComponent(out Transform transform);
            playerTransform.Position = new Vector3(1, 2, 3);
            weapon.Update();
            Assert.Equal(playerTransform.Position, transform.Position);
        }

        [Fact]
        public void GiveDamageMelleeTest()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            Knife weapon = _weaponFactory.Create<Knife>(player) as Knife;
            weapon.GiveDamage(player);
            Assert.Equal(9, player.CurrentHealthPoints);
        }

        [Fact]
        public void NullObjectWeaponTest()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            EmptyWeapon weapon = _weaponFactory.Create<EmptyWeapon>(player) as EmptyWeapon;
            weapon.GiveDamage(player);
            Assert.Equal(10, player.CurrentHealthPoints);
            player.TryTakeComponent(out Transform playerTransform);
            weapon.TryTakeComponent(out Transform transform);
            playerTransform.Position = new Vector3(1, 2, 3);
            weapon.Update();
            Assert.Equal(new Vector3(0, 0, 0), transform.Position);
            weapon.Attack(new Vector2(1, 1));
            Assert.Equal(new Vector3(0, 0, 0), transform.Position);
            Assert.Equal(null, weapon.Parent);
        }

        [Fact]
        public void FasterWeaponDecorator_TenTimesLess_ReturnsTrue()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            Pistol weapon = _weaponFactory.Create<Pistol>(player) as Pistol;
            var previousReloadTime = weapon.ReloadTime;
            FasterWeaponBaseDecorator decorator = new FasterWeaponBaseDecorator(weapon, weapon.Configuration, weapon.Components);
            Assert.Equal(previousReloadTime / 10f, decorator.ReloadTime);
        }

        [Fact]
        public void WeaponDecorator_AttackTest()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            bool isBulletCreated = false;
            var weapon = _weaponFactory.Create<Pistol>(player);
            weapon = new FasterWeaponBaseDecorator(weapon, weapon.Configuration, weapon.Components);
            _bulletSystem.OnStart += _ => isBulletCreated = true;
            weapon.Attack(new Vector2(1, 1));
            Assert.True(isBulletCreated);
        }

        [Fact]
        public void WeaponDecorator_UpdateTest()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            bool isBulletCreated = false;
            var weapon = _weaponFactory.Create<Pistol>(player);
            weapon = new FasterWeaponBaseDecorator(weapon, weapon.Configuration, weapon.Components);
            player.TryTakeComponent(out Transform transform);
            transform.Position = new Vector3(1, 2, 3);
            weapon.Update();
            weapon.TryTakeComponent(out Transform weaponTransform);
            Assert.Equal(transform.Position, weaponTransform.Position);
            Assert.Equal(player, weapon.Parent);
        }

        [Fact]
        public void WeaponDecorator_DamageTest()
        {
            Player player = new Player(new List<IComponent>
            {
                new Transform()
            }, new PlayerConfiguration(1, 10));
            var weapon = _weaponFactory.Create<Pistol>(player);
            weapon = new FasterWeaponBaseDecorator(weapon, weapon.Configuration, weapon.Components);
            weapon.GiveDamage(player);
            Assert.Equal(8, player.CurrentHealthPoints);
        }
    }
}
