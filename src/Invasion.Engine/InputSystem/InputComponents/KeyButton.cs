using Invasion.Engine.InputSystem.Interfaces;
using Invasion.Engine.Interfaces;
using System.Windows.Input;

namespace Invasion.Engine.InputSystem.InputComponents
{
    public class KeyButton : IInputComponent<bool>
    {
        private IInputProvider _inputProvider;
        private Key _key;

        public KeyButton(IInputProvider inputProvider, Key key)
        {
            _inputProvider = inputProvider;
            _key = key;
        }

        public bool ReadValue()
        {
            return _inputProvider.CheckKey(_key);
        }
    }
}
