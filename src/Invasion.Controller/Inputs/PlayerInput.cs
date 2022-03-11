using Invasion.Engine.InputSystem;
using Invasion.Engine.InputSystem.InputComponents;
using SharpDX;
using SharpDX.DirectInput;
using SharpDX.Windows;

namespace Invasion.Controller.Inputs
{
    public class PlayerInput
    {
        private DInput _dInput;
        private KeyButton _upButton;
        private KeyButton _downButton;
        private KeyButton _leftButton;
        private KeyButton _rightButton;
        private MouseButton _shootKey;

        public DInput DInput => _dInput;
        public PlayerInput(RenderForm renderForm, DInput input, Key upKey, Key downKey, Key rightKey, Key leftKey, MouseKey shootKey)
        {
            _dInput = input;
            _upButton = new KeyButton(input, upKey);
            _downButton = new KeyButton(input, downKey);
            _leftButton = new KeyButton(input, leftKey);
            _rightButton = new KeyButton(input, rightKey);
            _shootKey = new MouseButton(input, shootKey);
        }

        public bool ReadShoot() => _shootKey.ReadValue();

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
