using Invasion.Controller.Inputs;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Interfaces;
using Invasion.Models.Decorator;
using Invasion.Models.Weapons;
using SharpDX;

namespace Invasion.Controller.Controllers
{
    public class PlayerController : IController
    {
        private PlayerDecorator _player;
        private PlayerInput _input;
        private RigidBody2D _rigidBody;
        private WeaponBase _weaponBase;
        private WeaponInput _weaponInput;
        private float _shootTime;


        public PlayerController(PlayerDecorator player, PlayerInput input)
        {
            _player = player;
            _input = input;
            player.TryTakeComponent(out _rigidBody);
        }

        public void BindGun(WeaponBase weaponBase, WeaponInput weaponInput)
        {
            _weaponInput = weaponInput;
            _weaponBase = weaponBase;
            _weaponBase.Update();
        }
        
        public void Update()
        {
            _weaponBase?.Update();
            Vector2 inputVector = _input.ReadValue() / 10;
            _rigidBody.Speed = inputVector * _player.Speed;
            _shootTime += Time.FixedDeltaTime;
            Rotate();
            if (_weaponBase is not null && _weaponInput.ReadShoot() && _shootTime >= _weaponBase.ReloadTime)
            {
                _player.TryTakeComponent(out Transform transform);
                var direction = new Vector2((float)Math.Cos(transform.Rotation.X), -(float)Math.Sin(transform.Rotation.X));
                _shootTime = 0;
                _weaponBase?.Attack(Vector2.Normalize(direction)/10);
            }
        }

        private void Rotate()
        {
            if (_weaponBase is not null)
            {
                _player.TryTakeComponent(out Transform transform);
                var direction = _weaponInput.ReadValue();
                var angle = Math.Tan(45) * direction.X / 25;
                transform.Rotation = new Vector3(transform.Rotation.X + (float) angle, transform.Rotation.Y,
                    transform.Rotation.Z);
            }
        }
    }
}
