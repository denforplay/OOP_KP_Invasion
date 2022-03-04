using Invasion.Engine.InputSystem;
using Invasion.Engine.InputSystem.InputComponents;
using SharpDX;
using SharpDX.DirectInput;

namespace Invasion.Controller.Inputs
{
    public class PlayerInput
    {
        private KeyButton _upButton;
        private KeyButton _downButton;
        private KeyButton _leftButton;
        private KeyButton _rightButton;

        public PlayerInput(DInput input, Key upKey, Key downKey, Key rightKey, Key leftKey)
        {
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
