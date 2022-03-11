using System.Windows;
using Invasion.Controller.Inputs;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Weapons;
using SharpDX;
using Cursor = System.Windows.Forms.Cursor;

namespace Invasion.Controller.Controllers
{
    public class PlayerController : IController
    {
        private GameObject _player;
        private PlayerInput _input;
        private RigidBody2D _rigidBody;
        private IWeapon _weapon;
        private float _shootTime;
        public PlayerController(GameObject player, PlayerInput input)
        {
            _player = player;
            _input = input;
            player.TryTakeComponent(out _rigidBody);
        }

        public void BindGun(IWeapon weapon)
        {
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
            if (_input.ReadShoot() && _shootTime >= 1)
            {
                Transform transform;
                _player.TryTakeComponent(out transform);
                var cursorPosition = new Vector2(Cursor.Position.X, 900 - Cursor.Position.Y);
                var playerPosition = new Vector2(transform.Position.X / 45f * 1600, transform.Position.Y / 25f*900);
                var direction = new Vector2(cursorPosition.X - playerPosition.X, cursorPosition.Y - playerPosition.Y);
                _shootTime = Time.DeltaTime;
                _weapon?.Attack(Vector2.Normalize(direction)/10);
            }
        }

        private void Rotate()
        {
            Transform transform;
            _player.TryTakeComponent(out transform);
            var playerPosition = new Vector2(transform.Position.X / 45f * 1600, transform.Position.Y / 25f * 900);
            var cursorVector = new Vector2(playerPosition.X - Cursor.Position.X,
                playerPosition.Y - (900 - Cursor.Position.Y));
            var playerVector = new Vector2(cursorVector.X, 0);
            var angle = Math.Atan(cursorVector.Y / cursorVector.X);
            if (cursorVector.X > 0)
            {
                angle += Math.PI;
            }

            transform.Rotation = new Vector3(-(float) angle, transform.Rotation.Y, transform.Rotation.Z);
           
            Console.WriteLine(cursorVector.X + " " + cursorVector.Y + " " + angle/3.14f*180);
        }
    }
}
