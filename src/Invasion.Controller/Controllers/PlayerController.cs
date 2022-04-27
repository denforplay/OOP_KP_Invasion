using Invasion.Controller.Inputs;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.InputSystem.Interfaces;
using Invasion.Engine.Interfaces;
using Invasion.Models.Decorator;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;
using System.Numerics;

namespace Invasion.Controller.Controllers
{
    /// <summary>
    /// Player controller
    /// </summary>
    public class PlayerController : IController
    {
        private PlayerDecorator _player;
        private IInputComponent<Vector2> _playerInput;
        private RigidBody2D _rigidBody;
        private WeaponBaseDecorator _weaponBase;
        private WeaponInput _weaponInput;
        private float _shootTime;

        /// <summary>
        /// Player controller constructor
        /// </summary>
        /// <param name="player">Controlled player</param>
        /// <param name="playerInput">Player controls</param>
        public PlayerController(PlayerDecorator player, IInputComponent<Vector2> playerInput)
        {
            _player = player;
            _playerInput = playerInput;
            player.TryTakeComponent(out _rigidBody);
        }

        /// <summary>
        /// Give gun to a player
        /// </summary>
        /// <param name="weaponBase">Binded weapon</param>
        /// <param name="weaponInput">Weapon controls</param>
        public void BindGun(WeaponBaseDecorator weaponBase, WeaponInput weaponInput)
        {
            _weaponInput = weaponInput;
            _weaponBase = weaponBase;
            _weaponBase.Update();
        }
        
        public void Update()
        {
            _weaponBase?.Update();
            Vector2 inputVector = _playerInput.ReadValue() / 10;
            _rigidBody.Speed = inputVector * _player.Speed;
            _shootTime += Time.FixedDeltaTime;
            Rotate();
            if (_weaponBase is not null && _weaponInput.ReadShoot() && _shootTime >= _weaponBase.ReloadTime)
            {
                _player.TryTakeComponent(out Transform transform);
                var direction = new Vector2((float)Math.Cos(transform.Rotation.X), -(float)Math.Sin(transform.Rotation.X));
                _shootTime = 0;
                _weaponBase?.Attack(Vector2.Normalize(direction)/10 * _weaponBase.Speed);
            }
        }

        /// <summary>
        /// Rotate player method
        /// </summary>
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
