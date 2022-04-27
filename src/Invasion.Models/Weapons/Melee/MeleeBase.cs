using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons.Melee;

public class MeleeBase : WeaponBase
{
    public override void GiveDamage(IHealthable healthable)
    {
        healthable.TakeDamage(Damage);
    }

    private Transform _transform;
    private GameObject _parent;
    private Transform _parentTransform;
    private bool _canAttack;

    public MeleeBase(GameObject parent, WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(configuration, components, layer)
    {
        _parent = parent;
        TryTakeComponent(out _transform);
        parent?.TryTakeComponent(out _parentTransform);
        _canAttack = true;
    }

    public bool IsAttack => !_canAttack;
    public override GameObject Parent => _parent;
    public override int Damage { get; set; }
    public override float Speed { get; set; }



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