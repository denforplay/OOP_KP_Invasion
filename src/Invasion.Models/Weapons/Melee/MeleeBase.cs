using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons.Melee;

public class MeleeBase : WeaponBase
{
    public override void GiveDamage(IHealthable healthable)
    {
        healthable.TakeDamage(_damage);
    }

    private Transform _transform;
    private GameObject _parent;
    private Transform _parentTransform;
    private bool _canAttack;
    private int _damage;
    public override float ReloadTime => 0.5f;
    public bool IsAttack => !_canAttack;
    public override GameObject Parent => _parent;
    public override int Damage => _damage;
    public override float Speed { get; set; }

    public MeleeBase(GameObject parent, List<IComponent> components = null) : base(components, Layer.Weapon)
    {
        _parent = parent;
        _damage = 1;
        Speed = 1f;
        TryTakeComponent(out _transform);
        parent?.TryTakeComponent(out _parentTransform);
        _canAttack = true;
    }


    public override async void Attack(Vector2 direction)
    {
        if (_canAttack)
        {
            direction = direction * 10;
            _canAttack = false;
            _transform.Position = new Vector3(_transform.Position.X + direction.X, _transform.Position.Y + direction.Y, _transform.Position.Z);
            await Task.Delay(150);
            _canAttack = true;
        }
    }

    public override void Update()
    {
        if (_canAttack)
        {
            _transform.Rotation = _parentTransform.Rotation;
            _transform.Position = _parentTransform.Position;
        }
    }
}