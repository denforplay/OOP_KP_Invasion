using Invasion.Engine.InputSystem;
using Invasion.Engine.InputSystem.InputComponents;
using Invasion.Engine.InputSystem.Interfaces;
using SharpDX.DirectInput;
using System.Numerics;

namespace Invasion.Controller.Inputs
{
    public class PlayerInput : IInputComponent<Vector2>
    {
        private DInput _dInput;
        private KeyButton _upButton;
        private KeyButton _downButton;
        private KeyButton _leftButton;
        private KeyButton _rightButton;

        public DInput DInput => _dInput;
        public PlayerInput(DInput input, Key upKey, Key downKey, Key rightKey, Key leftKey)
        {
            _dInput = input;
            _upButton = new KeyButton(input, upKey);
            _downButton = new KeyButton(input, downKey);
            _leftButton = new KeyButton(input, leftKey);
            _rightButton = new KeyButton(input, rightKey);
        }

        public Vector2 ReadValue()
        {
            Vector2 value = Vector2.Zero;
            if (_upButton.ReadValue())
                value += new Vector2(0, 1);

            if (_downButton.ReadValue())
                value += new Vector2(0, -1);

            if (_leftButton.ReadValue())
                value += new Vector2(-1, 0);

            if (_rightButton.ReadValue())
                value += new Vector2(1, 0);

            return value;
        }
    }
}
