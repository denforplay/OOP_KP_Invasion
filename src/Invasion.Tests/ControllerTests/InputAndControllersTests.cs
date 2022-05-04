using Invasion.Controller.Controllers;
using Invasion.Controller.Inputs;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Graphics;
using Invasion.Engine.InputSystem;
using Invasion.Engine.InputSystem.InputComponents;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;
using Invasion.Models.Enemies;
using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Melee;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;
using Xunit;

namespace Invasion.Tests
{
    public class InputAndControllersTests
    {
        private IInputProvider _inputProvider;
        private IGraphicProvider _graphicProvider;
        private WeaponFactory _weaponFactory;

        public InputAndControllersTests()
        {
            _inputProvider = new DirectXInputProvider(new SharpDX.Windows.RenderForm());
            _graphicProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            Compose();
            _weaponFactory = new WeaponFactory(null, new BulletSystem(), _graphicProvider);
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
        public void TestDirectXInputs()
        {
            bool isPressed = _inputProvider.CheckKey(Key.Tab);
            Assert.False(isPressed);
            _inputProvider.Update();
            isPressed = _inputProvider.CheckKey(Key.Tab);
            Assert.False(isPressed);
            _inputProvider.Dispose();
            Assert.Throws<NullReferenceException>(() => _inputProvider.CheckKey(System.Windows.Input.Key.Tab));
        }

        [Fact]
        public void TestKeyButton()
        {
            KeyButton keyButton = new KeyButton(_inputProvider, System.Windows.Input.Key.Back);
            Assert.False(keyButton.ReadValue());
        }

        [Fact]
        public void TestPlayerInput()
        {
            PlayerInput playerInput = new PlayerInput(_inputProvider, Key.W, Key.S, Key.D, Key.A);
            var value = playerInput.ReadValue();
            Assert.Equal(Vector2.Zero, value);
        }

        [Fact]
        public void TestWeaponInput()
        {
            WeaponInput weaponInput = new WeaponInput(_inputProvider, Key.Q, Key.E, Key.Space);
            var value = weaponInput.ReadValue();
            var shootValue = weaponInput.ReadShoot();
            Assert.Equal(Vector2.Zero, value);
            Assert.False(shootValue);
        }


        [Fact]
        public void TestPlayerController()
        {
            PlayerController playerController = new PlayerController(new PlayerDecorator(new Player(null, new PlayerConfiguration(3, 3)), null, new PlayerConfiguration(3, 3)), new PlayerInput(_inputProvider, Key.W, Key.A, Key.S, Key.D));
            playerController.BindGun(new WeaponBaseDecorator(_weaponFactory.Create<Knife>(null), new WeaponConfiguration(3, 3, 3), null), new WeaponInput(_inputProvider, Key.Q, Key.E, Key.Space));
            playerController.Update();
            Assert.NotNull(playerController);
        }

        [Fact]
        public void TestEnemyController()
        {
            EnemyController enemyController = new EnemyController(new KamikadzeEnemy(new List<IComponent> { new Transform(), new RigidBody2D()}, new EnemyConfiguration(3, 3, 3)), new List<Player> { new Player(new List<IComponent> { new Transform()}, new PlayerConfiguration(2, 2))});
            enemyController.BindGun(new WeaponBaseDecorator(_weaponFactory.Create<Knife>(null), new WeaponConfiguration(3, 3, 3), null));
            enemyController.Update();
            Assert.NotNull(enemyController);
        }
    }
}
