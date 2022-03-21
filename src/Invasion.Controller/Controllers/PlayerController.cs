using Invasion.Controller.Inputs;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Weapons;
using SharpDX;

namespace Invasion.Controller.Controllers
{
    public class PlayerController : IController
    {
        private GameObject _player;
        private PlayerInput _input;
        private RigidBody2D _rigidBody;
        private IWeapon _weapon;
        private WeaponInput _weaponInput;
        private float _shootTime;
        public PlayerController(GameObject player, PlayerInput input)
        {
            _player = player;
            _input = input;
            player.TryTakeComponent(out _rigidBody);
        }

        public void BindGun(IWeapon weapon, WeaponInput weaponInput)
        {
            _weaponInput = weaponInput;
            _weapon = weapon;
            _weapon.Update();
        }
        
        public void Update()
        {
            _weapon?.Update();
            Vector2 inputVector = _input.ReadValue() / 10;
            _rigidBody.Speed = inputVector;
            _shootTime += Time.DeltaTime;
            Rotate();
            if (_weapon is not null && _weaponInput.ReadShoot() && _shootTime >= _weapon.ReloadTime)
            {
                _player.TryTakeComponent(out Transform transform);
                var direction = new Vector2((float)Math.Cos(transform.Rotation.X), -(float)Math.Sin(transform.Rotation.X));
                _shootTime = Time.DeltaTime;
                _weapon?.Attack(Vector2.Normalize(direction)/10);
            }
        }

        private void Rotate()
        {
            if (_weapon is not null)
            {
                _player.TryTakeComponent(out Transform transform);
                var direction = _weaponInput.ReadValue();
                var angle = Math.Tan(45) * direction.X / 10;
                transform.Rotation = new Vector3(transform.Rotation.X + (float) angle, transform.Rotation.Y,
                    transform.Rotation.Z);
            }
        }
    }
}
