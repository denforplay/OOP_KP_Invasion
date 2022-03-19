using Invasion.Controller.Controllers;
using Invasion.Controller.Inputs;
using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.InputSystem;
using Invasion.Engine.InputSystem.InputComponents;
using Invasion.Models;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Firearms;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.View;
using Invasion.View.Factories.Base;
using Invasion.View.Factories.BulletFactories;
using Invastion.CompositeRoot.Base;
using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;
using Size = System.Drawing.Size;

namespace Invastion.CompositeRoot.Implementations;

public class HeroCompositeRoot : ICompositeRoot
{
    private RenderForm _renderForm;
    private DInput _dInput;
    private DX2D _dx2D;
    private CollisionsCompositeRoot _collisionsRoot;
    private RectangleF _clientRect;
    private Scene _gameScene;
    private Dictionary<string, Player> _players = new Dictionary<string, Player>();
    private Dictionary<Player, (PlayerController, GameObjectView)> _playersData = new Dictionary<Player, (PlayerController, GameObjectView)>();
    private Dictionary<IWeapon, (GameObject, GameObjectView)> _weaponData = new Dictionary<IWeapon, (GameObject, GameObjectView)>();
    private GameObjectViewFactoryBase<BulletBase> _bulletFactory;
    private BulletSystem _bulletSystem;
    public HeroCompositeRoot(RenderForm renderForm, DInput dInput, DX2D dx2D, CollisionsCompositeRoot collisionsRoot, RectangleF clientRect, Scene gameScene)
    {
        _dInput = dInput;
        _dx2D = dx2D;
        _collisionsRoot = collisionsRoot;
        _clientRect = clientRect;
        _gameScene = gameScene;
        _bulletSystem = new BulletSystem();
        _bulletFactory = new DefaultBulletFactory(_dx2D);
        _bulletSystem.OnStart += SpawnBullet;
        _bulletSystem.OnEnd += DeleteBullet;
        _renderForm = renderForm;
    }

    public void Compose()
    {
       CreatePlayer("firstPlayer", "dash.bmp", new Vector3(15f, 15f, 0f), new Size(2, 2), new PlayerInput(_dInput, Key.W, Key.S, Key.D, Key.A));
       CreatePlayer("secondPlayer", "dash.bmp", new Vector3(5f, 5f, 0f), new Size(2, 2), new PlayerInput(_dInput, Key.NumberPad8, Key.NumberPad2, Key.NumberPad6, Key.NumberPad4));
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
        }, Layer.Player);
        player.AddComponent(new BoxCollider2D(_collisionsRoot.Controller, player, colliderSize));
        var playerView = new GameObjectView(player, _clientRect.Height / 25f, _clientRect.Height);
        var playerController = new PlayerController(player, inputs);
        _gameScene.Add(player, playerView, playerController);
        _players.Add(playerTag, player);
        _playersData.Add(player, (playerController, playerView));
    }
    
    private void InitializeWeapons()
    {
        CreateWeapon(_players["firstPlayer"], "pistol.png", new WeaponInput(_dInput, Key.Q, Key.E, Key.Space));
        CreateWeapon(_players["secondPlayer"], "pistol.png", new WeaponInput(_dInput, Key.NumberPad7, Key.NumberPad9, Key.NumberPadEnter));
    }
    
    private void CreateWeapon(Player owner, string sprite, WeaponInput weaponInput)
    {
        Transform ownerTransform;
        owner.TryTakeComponent(out ownerTransform);
        var weapon = new Pistol(new List<IComponent>
        {
            new Transform(),
            new SpriteRenderer(_dx2D, sprite),
        }, ownerTransform);
            
        var weaponView =
            new GameObjectView(weapon, _clientRect.Height / 25F, _clientRect.Height);
            
        _gameScene.AddGameObject(weapon as GameObject);
        _gameScene.AddGameObjectView(weaponView);
        _playersData[owner].Item1.BindGun(weapon, weaponInput);
        weapon.OnShotEvent += Shoot;
    }
    
    private void SpawnBullet(Entity<BulletBase> bullet)
    {
        _gameScene.AddGameObjectView(_bulletFactory.Create(bullet, _clientRect.Height / 25f, _clientRect.Height));
        _gameScene.AddGameObject(bullet.GetEntity);
    }
        
    private void DeleteBullet(Entity<BulletBase> bullet)
    {
        _bulletFactory.Destroy(bullet);
    }
        
    private void Shoot(BulletBase bullet)
    {
        _bulletSystem.Work(bullet);
    }
}