using Invasion.Engine.InputSystem.Interfaces;
using SharpDX.DirectInput;

namespace Invasion.Engine.InputSystem.InputComponents
{
    public class KeyButton : IInputComponent<bool>
    {
        private DInput _input;
        private Key _key;

        public KeyButton(DInput input, Key key)
        {
            _input = input;
            _key = key;
        }

        public bool ReadValue()
        {
            if (_input.KeyboardUpdated)
            {
                return _input.KeyboardState.IsPressed(_key);
            }

            return false;
        }
    }
}
