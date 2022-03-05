using Invasion.Controller.Inputs;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using SharpDX;

namespace Invasion.Controller.Controllers
{
    public class PlayerController : IController
    {
        private GameObject _player;
        private PlayerInput _input;
        private RigidBody2D _rigidBody;

        public PlayerController(GameObject player, PlayerInput input)
        {
            _player = player;
            _input = input;
            player.TryTakeComponent(out _rigidBody);
        }

        public void Update()
        {
            if (_player.TryTakeComponent<Transform>(out var transformable))
            {
                Vector2 inputVector = _input.ReadValue()/10;
                _rigidBody.Speed = inputVector;
            }
        }
    }
}
