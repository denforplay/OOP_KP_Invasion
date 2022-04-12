﻿using Invasion.Controller.Controllers;
using Invasion.Controller.Inputs;
using Invasion.Core.EventBus;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.InputSystem;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Decorator;
using Invasion.Models.Events;
using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.View;
using Invasion.View.Factories.Base;
using Invastion.CompositeRoot.Base;
using SharpDX;
using SharpDX.DirectInput;
using System.Drawing;

namespace Invastion.CompositeRoot.Implementations;

public class HeroCompositeRoot : ICompositeRoot
{
    private DInput _dInput;
    private DX2D _dx2D;
    private CollisionsCompositeRoot _collisionsRoot;
    private Scene _gameScene;
    private Dictionary<string, Player> _players = new Dictionary<string, Player>();
    private Dictionary<Player, (PlayerController, GameObjectView)> _playersData = new Dictionary<Player, (PlayerController, GameObjectView)>();
    private Dictionary<WeaponBase, (GameObject, GameObjectView)> _weaponData = new Dictionary<WeaponBase, (GameObject, GameObjectView)>();
    private GameObjectViewFactoryBase<BulletBase> _bulletFactory;
    private BulletSystem _bulletSystem;
    private PlayerConfiguration _playerConfiguration;
    private WeaponFactory _weaponFactory;
    Dictionary<string, Type> _playerWeapons;

    public List<Player> Players => _players.Values.ToList();
    public HeroCompositeRoot(DInput dInput, DX2D dx2D, BulletSystem bulletSystem, CollisionsCompositeRoot collisionsRoot,Scene gameScene, Dictionary<string, Type> playerWeapons)
    {
        _playerWeapons = playerWeapons;
        _bulletSystem = bulletSystem;
        _dInput = dInput;
        _dx2D = dx2D;
        _collisionsRoot = collisionsRoot;
        _gameScene = gameScene;
        _bulletFactory = new BulletFactory();
        _playerConfiguration = new PlayerConfiguration(1, 5);
        _bulletSystem.OnStart += SpawnBullet;
        _bulletSystem.OnEnd += DeleteBullet;
        _weaponFactory = new WeaponFactory(_collisionsRoot.Controller, bulletSystem, dx2D);
    }

    public void Compose()
    {
       CreatePlayer("Player1", @"Sources\dash.bmp", new Vector3(15f, 15f, 0f), new Size(2, 2), new PlayerInput(_dInput, Key.W, Key.S, Key.D, Key.A));
       CreatePlayer("Player2", @"Sources\dash.bmp", new Vector3(5f, 5f, 0f), new Size(2, 2), new PlayerInput(_dInput, Key.NumberPad8, Key.NumberPad2, Key.NumberPad6, Key.NumberPad4));
       InitializeWeapons();
    }
    
    private void CreatePlayer(string playerTag, string sprite, Vector3 startPosition, Size colliderSize, PlayerInput inputs)
    {
        var player = new Player(new List<IComponent>
        {
            new Transform
            {
                Position = startPosition
            },
            new SpriteRenderer(_dx2D, sprite),
            new RigidBody2D()
        }, _playerConfiguration, Layer.Player);
        var playerDecorator = new PlayerDecorator(player, new List<IComponent>(player.Components), _playerConfiguration);
        playerDecorator.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, playerDecorator, colliderSize));
        var playerView = new GameObjectView(playerDecorator);
        var playerController = new PlayerController(playerDecorator, inputs);
        var healthView = new HealthView(playerDecorator, _dx2D.RenderTarget);
        playerDecorator.OnDestroyed += () =>
        {
            SingletonEventBus.GetInstance.Invoke(new GameLoseEvent());
        };
        _gameScene.Add(playerDecorator, playerView, playerController);
        _gameScene.AddGameObjectView(healthView);
        _players.Add(playerTag, playerDecorator);
        _playersData.Add(playerDecorator, (playerController, playerView));
    }
    
    private void InitializeWeapons()
    {
        CreateWeapon(_players["Player1"], new WeaponInput(_dInput, Key.Q, Key.E, Key.Space), _playerWeapons["Player1"]);
        CreateWeapon(_players["Player2"], new WeaponInput(_dInput, Key.NumberPad7, Key.NumberPad9, Key.NumberPadEnter), _playerWeapons["Player2"]);
    }
    
    private void CreateWeapon(Player owner, WeaponInput weaponInput, Type type)
    {
        var weaponModel = _weaponFactory.Create(owner, type);
        WeaponBase weaponBase = new WeaponBaseDecorator(weaponModel, new List<IComponent>(weaponModel.Components));
        var collider = new BoxCollider2D(_collisionsRoot.Controller, weaponBase, new Size(2, 2));
        weaponBase.AddComponent(collider);
        var weaponView =
            new GameObjectView(weaponBase);
            
        _gameScene.AddGameObject(weaponBase);
        _gameScene.AddGameObjectView(weaponView);
        _playersData[owner].Item1.BindGun(weaponBase, weaponInput);
    }
    
    private void SpawnBullet(Entity<BulletBase> bullet)
    {
        var bulletView = _bulletFactory.Create(bullet);
        _gameScene.AddGameObjectView(bulletView);
        _gameScene.AddGameObject(bullet.GetEntity);
        bullet.GetEntity.OnDestroyed += () =>
        {
            _gameScene.RemoveGameObject(bullet.GetEntity);
            _gameScene.RemoveGameObjectView(bulletView);
        };
    }
        
    private void DeleteBullet(Entity<BulletBase> bullet)
    {
        bullet.GetEntity.OnDestroy();
        _bulletFactory.Destroy(bullet);
    }

    public void Dispose()
    {
    }
}