using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using Invasion.Models.Enemies;
using Invasion.Models.Weapons;
using SharpDX;

namespace Invasion.Controller.Controllers;

public class EnemyController : IController
{
    private WeaponBase _weaponBase;
    private EnemyBase _enemy;
    private Transform _enemyTransform;
    private RigidBody2D _enemyPhysics;
    private float _shootTime;
    private List<Player> _players;

    public EnemyController(EnemyBase enemy, List<Player> players)
    {
        _enemy = enemy;
        _players = players;
        _enemy.TryTakeComponent(out _enemyPhysics);
        _enemy.TryTakeComponent(out _enemyTransform);
    }
    
    public void BindGun(WeaponBase weaponBase)
    {
        _weaponBase = weaponBase;
        _weaponBase.Update();
    }
    public void Update()
    {
        var closestPlayer = FindClosestPlayer();
        Transform playerTransform;
        _weaponBase.Update();
        closestPlayer.TryTakeComponent(out playerTransform);
        var direction = Vector3.Normalize(Vector3.Subtract(playerTransform.Position, _enemyTransform.Position))/15 * Time.TimeScale;
        _enemyPhysics.Speed = new Vector2(direction.X, direction.Y) * _enemy.Speed;
        if (_weaponBase is not null && _shootTime >= _weaponBase.ReloadTime)
        {
            _shootTime = 0;
            _weaponBase.Attack(new Vector2(direction.X, direction.Y) * 5);
        }
        Transform transform;
        _enemy.TryTakeComponent(out transform);
        var angle = Math.Atan(direction.Y / direction.X) + Math.PI;
        if (direction.X > 0)
        {
            angle += Math.PI;
        }

        transform.Rotation = new Vector3(-(float) angle, transform.Rotation.Y, transform.Rotation.Z);
        _shootTime += Time.FixedDeltaTime * Time.TimeScale;
    }

    private Player FindClosestPlayer()
    {
        Dictionary<Player, float> _distancesToPlayer = new Dictionary<Player, float>();
        if (_enemy.TryTakeComponent(out Transform transform))
        {
            foreach (var player in _players)
            {
                Transform playerTransform;
                player.TryTakeComponent(out playerTransform);
                float distance = Vector3.Distance(transform.Position, playerTransform.Position);
                _distancesToPlayer[player] = distance;
            }
        }

        return _distancesToPlayer.MinBy(x => x.Value).Key;
    }
}